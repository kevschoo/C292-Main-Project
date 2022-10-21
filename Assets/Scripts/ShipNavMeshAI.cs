using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class ShipNavMeshAI : ObjectAI
{

    [field: SerializeField] public override GameObject ObjectiveTarget { get; set; }
    [field: SerializeField] public override GameObject Target { get; set; }
    [field: SerializeField] public override bool AiIsEnabled { get; set; }

    NavMeshAgent Agent;
    [SerializeField] bool AllowTargetChange;
    [SerializeField] GameObject HomeBase;
    [SerializeField] bool InCombat;
    [SerializeField] int BaseAiType = 0;
    [SerializeField] int AiType = 0;
    [SerializeField] bool IsAtHome;
    [SerializeField] float MaxDistanceFromHome = 0.1f;
    [SerializeField] float AttackAtPercentOfRange = 0.9f;
    [SerializeField] float AccelMod = 0.33f;
    [SerializeField] float SpeedMod = 0.66f;



    [SerializeField] List<UniquePart> WeaponParts = new List<UniquePart>();
    [SerializeField] List<UniquePart> DefenseParts = new List<UniquePart>();
    [SerializeField] bool WeaponsActive;
    [SerializeField] bool DefensesActive;
    [SerializeField] bool HasCheckedParts = false;

    [SerializeField] float WeaponPartDelay = .15f;
    [SerializeField] float DefensePartDelay = .15f;

    [SerializeField] bool rotateCRRunning = false;
    [SerializeField] float rotateDir = 1;
    [SerializeField] bool allowOrbit = false;

    [SerializeField] SpaceObject spaceObject;

    [field: SerializeField] public GameObject BulletType { get; set; }

    public override void DamageTaken(GameObject Damager)
    {
        Debug.Log("Damager is:" + Damager.name);
       
        SetTarget(Damager);

    }
    // Start is called before the first frame update
    void Start()
    {
        this.RecalculateNVA();
        this.RecalculateSystems();

    }
    private void LateUpdate()
    {
        //this is a waste of processing power but the parts dont always get checked
        if(HasCheckedParts == false)
        {
            this.RecalculateNVA();
            this.RecalculateSystems();
            HasCheckedParts = true;
        }
    }
    private void Awake()
    {
        AiIsEnabled = true;
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        if (this.gameObject.GetComponent<SpaceObject>() != null)
        {
            this.spaceObject = gameObject.GetComponent<SpaceObject>();
        }
        if (HomeBase == null)
        {

            HomeBase = this.gameObject;
        }
        AiIsEnabled = true;
    }
    public void SetTarget(GameObject target)
    {
        if(AllowTargetChange == true)
        { this.Target = target; }
    }

    public void SetHome(GameObject HomeBase)
    {
        this.HomeBase = HomeBase;
    }

    public void SetAiType(int val)
    {
        this.AiType = val;
    }
    // Update is called once per frame
    void Update()
    {
        DistanceThisToHome = Vector2.Distance(this.gameObject.transform.position, HomeBase.transform.position);
        if(Target != null)
        {
            DistanceThisToTarget = Vector2.Distance(this.gameObject.transform.position, Target.transform.position);
            DistanceHomeToTarget = Vector2.Distance(Target.transform.position, HomeBase.transform.position);
        }

        if (AiIsEnabled)
        {
            switch (AiType)
            {
                case 0:
                    DefaultAI(); 
                    break;
                case 1:
                    BaseAI();
                    break;
                case 2:
                    FighterAI();
                    break;
                case 3:
                    ObjectiveAI();
                    break;
                default:
                    Debug.Log("AI ERROR");
                    AiType = 0;
                    break;
            }

        }
        
    }
    float DistanceThisToHome;
    float DistanceThisToTarget;
    float DistanceHomeToTarget;

    public void ChangeAI(bool value)
    {
        this.AiIsEnabled = value;
        this.DeactivateDefenses();
        this.DeactivateWeapons();
    }
    public void ChangeAIStatus(int value)
    {
        this.AiType = value;
        MaxDistanceFromHome = .1f;
        this.DeactivateDefenses();
        this.DeactivateWeapons();
    }
    //This AI will stay at home and orbit
    void DefaultAI()
    {
        RecalculateNVA();
        if (DistanceThisToHome < MaxDistanceFromHome)
        { IsAtHome = true; }
        else
        { IsAtHome = false; }

        //If the objects stats are null, we cant do everything
        if (spaceObject == null)
        { return; }

        //Deactivate Weapons and defense when not in combat
        if (!InCombat && WeaponsActive)
        { DeactivateWeapons(); }
        if (!InCombat && DefensesActive)
        { DeactivateDefenses(); }

        MaxDistanceFromHome = 1f;
        InCombat = false;
        //if we have no target and at home, afk
        if (IsAtHome)
        {
            Agent.SetDestination(this.transform.position);
            OrbitTarget(HomeBase.transform.position);
            FaceTarget(Vector3.Lerp(this.transform.position, this.HomeBase.transform.position, .05f));
            return;
        }
        //else go home, lerp around.
        else
        {
            Agent.SetDestination(HomeBase.transform.position);
            Vector3 PosToLook = new Vector3(Agent.nextPosition[0], Agent.nextPosition[0], 0);
            FaceTarget(Vector3.Lerp(this.transform.position, PosToLook, .05f));
        }
    }
    //This AI will move closer until in range then fire, then go home
    void BaseAI()
    {
        RecalculateNVA();
        if (DistanceThisToHome < MaxDistanceFromHome)
        {IsAtHome = true;}
        else
        {IsAtHome = false;}

        //If the objects stats are null, we cant do everything
        if (spaceObject == null)
        {return; }

        //Deactivate Weapons and defense when not in combat
        if(!InCombat && WeaponsActive)
        {DeactivateWeapons();}
        if(!InCombat && DefensesActive)
        {DeactivateDefenses();}

        if(Target == null)
        {
            InCombat = false;
            //if we have no target and at home, afk
            if(IsAtHome)
            {
                return;
            }
            //else go home, lerp around.
            else
            {
                Agent.SetDestination(HomeBase.transform.position);
                Vector3 PosToLook = new Vector3(Agent.nextPosition[0], Agent.nextPosition[0], 0);
                FaceTarget(Vector3.Lerp(this.transform.position, PosToLook, .05f));
            }
        }

        if(Target != null && Target != this.gameObject)
        {
            InCombat = true;
            float AttackRange = this.spaceObject.offensiveStats._AttackRange * AttackAtPercentOfRange;
            float TravelRange = this.spaceObject.speedStats._TravelRange;

            //if the target is not within travel range from our home and not in our attack range, attempt to move closer
            if(DistanceThisToTarget > AttackRange)
            {
                if(DistanceThisToHome < TravelRange)
                {
                    Vector3 PosToLook = new Vector3(Agent.nextPosition[0], Agent.nextPosition[0], 0);
                    FaceTarget(Vector3.Lerp(this.transform.position, PosToLook, .05f));
                    Agent.SetDestination(Target.transform.position);
                }
                else
                {
                    this.Target = null;
                }

            }
            else if (DistanceThisToTarget < AttackRange)
            {
                if(!WeaponsActive)
                { ActivateWeapons(Target); }
                FaceTarget(Vector3.Lerp(this.transform.position, Target.transform.position, .05f));
                Agent.SetDestination(this.transform.position);

            }

            //if we have defenses and they are not active, activate them if we take damage
            if (spaceObject.defensiveStats != null && this.DefenseParts.Count != 0)
            {
                ActivateDefenses(Target, this.gameObject);
            }
        }
    }
    //This AI will move closer until in range then fire,and will continue going closer to orbit, then go home
    void FighterAI()
    {
        RecalculateNVA();
        if (DistanceThisToHome < MaxDistanceFromHome)
        { IsAtHome = true; }
        else
        { IsAtHome = false; }

        //If the objects stats are null, we cant do everything
        if (spaceObject == null)
        { return; }

        //Deactivate Weapons and defense when not in combat
        if (!InCombat && WeaponsActive)
        { DeactivateWeapons(); }
        if (!InCombat && DefensesActive)
        { DeactivateDefenses(); }

        if (Target == null)
        {
            InCombat = false;
            //if we have no target and at home, afk
            if (IsAtHome)
            {
                return;
            }
            //else go home, lerp around.
            else
            {
                Agent.SetDestination(HomeBase.transform.position);
                Vector3 PosToLook = new Vector3(Agent.nextPosition[0], Agent.nextPosition[0], 0);
                FaceTarget(Vector3.Lerp(this.transform.position, PosToLook, .1f));
            }
        }

        if (Target != null && Target != this.gameObject)
        {
            InCombat = true;
            float AttackRange = this.spaceObject.offensiveStats._AttackRange * AttackAtPercentOfRange;
            float TravelRange = this.spaceObject.speedStats._TravelRange;

            //if the target is not within travel range from our home and not in our attack range, attempt to move closer
            if (DistanceThisToTarget > AttackRange)
            {
                if (DistanceThisToHome < TravelRange)
                {
                    Vector3 PosToLook = new Vector3(Agent.nextPosition[0], Agent.nextPosition[0], 0);
                    FaceTarget(Vector3.Lerp(this.transform.position, PosToLook, .1f));
                    Agent.SetDestination(Target.transform.position);
                }
                else
                {
                    this.Target = null;
                }

            }
            else if (DistanceThisToTarget < AttackRange)
            {
                if (!WeaponsActive)
                { ActivateWeapons(Target); }
                FaceTarget(Vector3.Lerp(this.transform.position, Target.transform.position, .05f));
                if(DistanceThisToTarget < 2.5f)
                {
                    Agent.SetDestination(this.transform.position);
                    OrbitTarget(Target.transform.position);
                    StartCoroutine(CheckIfRotateStuck());
                }
                else
                {
                    Agent.SetDestination(Target.transform.position);
                }
                

            }

            //if we have defenses and they are not active, activate them if we take damage
            if (spaceObject.defensiveStats != null && this.DefenseParts.Count != 0)
            {
                ActivateDefenses(Target, this.gameObject);
            }
        }
    }

    //This AI will move closer to  the objective without care, it will shoot other targets along the way
    void ObjectiveAI()
    {
        RecalculateNVA();
        //If the objects stats are null, we cant do everything
        if (spaceObject == null)
        { return; }

        //Deactivate Weapons and defense when not in combat
        if (!InCombat && WeaponsActive)
        { DeactivateWeapons(); }
        if (!InCombat && DefensesActive)
        { DeactivateDefenses(); }

        if (Target == null && this.ObjectiveTarget == null)
        {
            InCombat = false;
            Agent.SetDestination(this.transform.position);
        }
        float AttackRange = this.spaceObject.offensiveStats._AttackRange * AttackAtPercentOfRange;
        float TravelRange = this.spaceObject.speedStats._TravelRange;
        if (Target != null && Target != this.gameObject)
        {
            InCombat = true;

            if (DistanceThisToTarget < AttackRange)
            {
                //Debug.Log("1");
                if (!WeaponsActive)
                { ActivateWeapons(Target); }
                FaceTarget(Vector3.Lerp(this.transform.position, Target.transform.position, .05f));
                if (ObjectiveTarget == Target)
                { Agent.SetDestination(this.transform.position); }
                else if(ObjectiveTarget != null)
                {Agent.SetDestination(ObjectiveTarget.transform.position);}
                else
                { Agent.SetDestination(Target.transform.position); }
            }
            //if the target is not within travel range from our home and not in our attack range, attempt to move closer
            else if (DistanceThisToTarget > AttackRange)
            {
                //Debug.Log("2");
                if(ObjectiveTarget != null)
                {
                    FaceTarget(Vector3.Lerp(this.transform.position, ObjectiveTarget.transform.position, .05f));
                    Agent.SetDestination(ObjectiveTarget.transform.position); 
                }
                else
                { Agent.SetDestination(Target.transform.position); }
            }
            //if we have defenses and they are not active, activate them if we take damage
            if (spaceObject.defensiveStats != null && this.DefenseParts.Count != 0)
            {
                ActivateDefenses(Target, this.gameObject);
            }
        }
        else if (Target == null && this.ObjectiveTarget != null)
        {
            //Debug.Log("3");
            if (Vector2.Distance(ObjectiveTarget.transform.position, this.gameObject.transform.position) < AttackRange)
            {
                Agent.SetDestination(this.transform.position);
                Target = ObjectiveTarget;
            }
            else
            {
                FaceTarget(Vector3.Lerp(this.transform.position, ObjectiveTarget.transform.position, .05f));
                Agent.SetDestination(ObjectiveTarget.transform.position);
            }
        
        }
    }

    public void OrbitTarget(Vector3 targ)
    {
        Vector3 axis = new Vector3(0, 0, 1);
        transform.RotateAround(targ,axis, Time.deltaTime * this.spaceObject.speedStats._Speed * rotateDir);
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
        Debug.Log("getting systems:" + this.gameObject.name);
        this.WeaponParts.Clear();
        this.DefenseParts.Clear();
        foreach (UniquePart Upart in this.gameObject.GetComponents<UniquePart>())
        {
            Debug.Log("Part Found" + Upart.name +", Part Cat:" + Upart.Category);
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
            int i = 1;
            foreach(UniquePart Upart in this.WeaponParts)
            {
                i++;
                StartCoroutine(StartWithDelay(Upart, i *this.WeaponPartDelay));
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
                StartWithDelay(Upart, this.DefensePartDelay);
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

    IEnumerator StartWithDelay(UniquePart part, float delay)
    {
        Debug.Log("Started part" + part.name);
        yield return new WaitForSeconds(delay);
        part.Activate();
    }



}
