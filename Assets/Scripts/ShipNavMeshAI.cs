using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ShipNavMeshAI : MonoBehaviour
{
    NavMeshAgent Agent;
    [SerializeField] GameObject Target;
    [SerializeField] GameObject HomeBase;
    [SerializeField] bool IsAtHome;
    [SerializeField] float MaxDistanceFromHome = 0.1f;
    [SerializeField] float AttackAtPercentOfRange = 0.9f;
    [SerializeField] float AccelMod = 0.33f;
    [SerializeField] float SpeedMod = 0.66f;

    [SerializeField] bool InCombat;
    [SerializeField] bool AiIsEnabled = true;

    [SerializeField] List<UniquePart> WeaponParts = new List<UniquePart>();
    [SerializeField] List<UniquePart> DefenseParts = new List<UniquePart>();
    [SerializeField] bool WeaponsActive;
    [SerializeField] bool DefensesActive;
    [SerializeField] float rotateDir = 1;
    [SerializeField] bool rotateCRRunning = false;
    SpaceObject spaceObject;

    [field: SerializeField] public GameObject BulletType { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        if (this.gameObject.GetComponent<SpaceObject>() != null)
        {
            this.spaceObject = gameObject.GetComponent<SpaceObject>();
        }
        if(HomeBase == null)
        {

            HomeBase = this.gameObject;
        }
        AiIsEnabled = true;
        this.RecalculateNVA();
        this.RecalculateSystems();
    }

    public void SetTarget(GameObject target)
    {
        this.Target = target;
    }

    public void SetHome(GameObject HomeBase)
    {
        this.HomeBase = HomeBase;
    }

    // Update is called once per frame
    void Update()
    {
        if(AiIsEnabled)
        {
            BaseAI();
        }
        
    }

    public void ChangeAI(bool value)
    {
        AiIsEnabled = value;
    }

    //Horrible AI lmao
    void BaseAI()
    {
        float distfromhome = Vector2.Distance(this.gameObject.transform.position, HomeBase.transform.position);
        if (distfromhome < MaxDistanceFromHome)
        {
            IsAtHome = true;
        }
        else
        {
            IsAtHome = false;
        }
        //If the objects stats are not null
        if (spaceObject != null)
        {
            //if we are not in combat but weapons active, deactivate
            if (InCombat == false && WeaponsActive)
            {
                this.DeactivateWeapons();
            }
            if (InCombat == false && DefensesActive)
            {
                this.DeactivateDefenses();
            }
            //if we do not have a target and not at home, go home
            if (Target == null && IsAtHome == false)
            {
                Debug.Log("heading home");
                Agent.SetDestination(HomeBase.transform.position);
                FaceTarget(HomeBase.transform.position);
                InCombat = false;
            }
            //if we have a target, attack, if not in range move
            if (Target != null)
            {

                InCombat = true;

                FaceTarget(Agent.path.corners[0]);
                float dist =  Vector2.Distance(this.gameObject.transform.position, Target.transform.position);
                if (distfromhome > this.spaceObject.speedStats._TravelRange)
                {
                    Debug.Log("target outside travel range");
                }
                float x = Agent.remainingDistance;

                //if the object is within travel range, go to the attack range
                if (dist >= this.spaceObject.offensiveStats._AttackRange * AttackAtPercentOfRange)
                {
                    //Debug.Log("enemy out of range");
                    Agent.SetDestination(Target.transform.position);

                }
                //if they are within attack range, attack
                else if (dist < this.spaceObject.offensiveStats._AttackRange * AttackAtPercentOfRange)
                {
                    //Debug.Log("enemy in range");
                    Agent.destination = this.transform.position;
                    OrbitTarget(Target.transform.position);
                    if(this.rotateCRRunning == false)
                    {
                        StartCoroutine(CheckIfRotateStuck());
                    }
                }
                //activate weapons if we can see the enemy
                if (WeaponsActive == false && dist < this.spaceObject.offensiveStats._AttackRange)
                {
                    ActivateWeapons(Target);
                }
                //if we have defenses and they are not active, activate them if we take damage
                if (spaceObject.defensiveStats != null && InCombat && this.DefenseParts.Count != 0)
                {
                    if (spaceObject.defensiveStats._CurrentHealth < spaceObject.defensiveStats._MaxHealth)
                    {
                        ActivateDefenses(Target, this.gameObject);
                    }
                    else
                    {
                        DefensesActive = false;
                        DeactivateDefenses();
                    }
                }
            }
        }
    }

    public void OrbitTarget(Vector3 targ)
    {
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(targ,axis, Time.deltaTime * Agent.speed * 10f * rotateDir);
    }

    public void FaceTarget(Vector3 targ)
    {
        Vector2 direction = targ - this.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion Rot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rot, Agent.acceleration * 7 * Time.deltaTime);


    }
    public void RecalculateNVA()
    {
        this.Agent.acceleration = spaceObject.speedStats._Speed * AccelMod;
        this.Agent.speed = spaceObject.speedStats._Speed * SpeedMod;
    }
    public void RecalculateSystems()
    {
        this.WeaponParts.Clear();
        this.DefenseParts.Clear();
        foreach (UniquePart Upart in this.gameObject.GetComponents<UniquePart>())
        {
            if (Upart.Category == "Weapon")
            {
                this.WeaponParts.Add(Upart);
            }
            if (Upart.Category == "Defense")
            {
                this.DefenseParts.Add(Upart);
            }
        }
    }

    void ActivateWeapons(GameObject target)
    {
        Debug.Log("WeaponsActivated");
        WeaponsActive = true;
        if(this.WeaponParts.Count > 0)
        {
            foreach(UniquePart Upart in this.WeaponParts)
            {
                Upart.Activate(target);
            }
        }
    }

    void DeactivateWeapons()
    {
        Debug.Log("WeaponsDeactivated");
        WeaponsActive = false;
        if (this.WeaponParts.Count > 0)
        {
            foreach (UniquePart Upart in this.WeaponParts)
            {
                Upart.DeActivate();
            }
        }
    }

    void ActivateDefenses(GameObject target, GameObject ThisShip)
    {
        Debug.Log("DefensesActivated");
        DefensesActive = true;
        if (this.DefenseParts.Count > 0)
        {
            foreach (UniquePart Upart in this.DefenseParts)
            {
                Upart.Activate(target, ThisShip);
            }
        }
    }

    void DeactivateDefenses()
    {
        Debug.Log("DefensesDeactivated");
        DefensesActive = false;
        if (this.DefenseParts.Count > 0)
        {
            foreach (UniquePart Upart in this.DefenseParts)
            {
                Upart.DeActivate();
            }
        }
    }

    //change to be more efficient 
    IEnumerator CheckIfRotateStuck()
    {
        rotateCRRunning = true;
        Vector3 prevPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        yield return new WaitForSeconds(.5f);
        //Debug.Log("start corotuine");
        Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        if(Vector3.Distance(prevPos,newPos) < .1f)
        {
            rotateDir *= -1;
        }
        rotateCRRunning = false;

    }
}
