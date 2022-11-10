using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
public class AI_Default : EntityAI
{
    //General Abstract Variables
    [field: SerializeField] public override string AITypeName { get; set; } = "Default";
    [field: SerializeField] public override GameObject ObjectiveTarget { get; set; }
    [field: SerializeField] public override GameObject Target { get; set; }
    [field: SerializeField] public override GameObject BaseLocation { get; set; }
    [field: SerializeField] public override bool AIEnabled { get ; set; }
    [field: SerializeField] public override EntitySystemData OffensiveSystemData { get; set; }
    [field: SerializeField] public override EntitySystemData DefensiveSystemData { get; set; }
    [field: SerializeField] public override EntitySystemData AltSystemData { get; set; }

    //Default AI Variables


    [SerializeField] NavMeshAgent Agent;
    [SerializeField] Entity thisEntity;

    [SerializeField] bool AllowTargetChange;
    [SerializeField] bool GetTargetFromParent;

    [SerializeField] List<EntitySystem> OffenseSystems = new List<EntitySystem>();
    [SerializeField] List<EntitySystem> DefenseSystems = new List<EntitySystem>();
    [SerializeField] List<EntitySystem> AltSystems = new List<EntitySystem>();
    [SerializeField] bool OffenseSystemsActive;
    [SerializeField] bool DefenseSystemsActive;
    [SerializeField] bool CreatingMinion;

    [SerializeField] float OffenseStartDelay = .15f;
    [SerializeField] float DefenseStartDelay = .15f;

    [SerializeField] float BaseStopDistance = 1f;
    [SerializeField] float EnemyStopDistance = 1f;
    [SerializeField] float DistanceFromBase;
    [SerializeField] float DistanceFromTarget;
    [SerializeField] float DistanceFromObjectiveTarget;


    public override void OnDamageTaken(GameObject Damager)
    {
        //Debug.Log("Damaged");
        //if we allow changing target
        if(AllowTargetChange)
        {
            if(Damager.TryGetComponent<EntityShip>(out EntityShip DamagerInfo))
            {
                //ignore them if it is one of our own
                if(DamagerInfo.Owner == this.thisEntity.Owner)
                {return;}
                //don't target if the enemy cant even die
                if(DamagerInfo.IsInvincible || !DamagerInfo.CanDie || !DamagerInfo.CanDieAtZero)
                { return;}
                //same reason
                if(!DamagerInfo.atr_Defense)
                { return; }
                Target = Damager;
            }
        }
    }

    public override void FindAllEntitySystems()
    {
        AltSystems.Clear();
        OffenseSystems.Clear();
        DefenseSystems.Clear();
        foreach (Component x in this.gameObject.GetComponents<Component>())
        {
            if(x is EntitySystem)
            {AltSystems.Add((EntitySystem)x);}
        }
        foreach (EntitySystem ES in this.AltSystems)
        {
            ES.DeActivate();
            if (ES.Category == "Offense")
            { OffenseSystems.Add(ES); }
            if (ES.Category == "Defense")
            { OffenseSystems.Add(ES); }
        }
    }

    private void Awake()
    {
        WaveEvent.ChangePlayerObjective += ChangeObjectiveTarget;
        if (gameObject.TryGetComponent<NavMeshAgent>(out NavMeshAgent NVA))
        { 
          Agent = NVA;
          Agent.updateRotation = false;
          Agent.updateUpAxis = false;
        }
        else
        { 
            Agent = this.gameObject.AddComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            Agent.baseOffset = 0.35f;
            Agent.radius = 0.35f;
            Agent.height = 0.7f;
        }
        if (gameObject.TryGetComponent<Entity>(out Entity ent))
        { thisEntity = ent; }
        if(BaseLocation == null)
        { BaseLocation = new GameObject("Base" + this.gameObject.name); BaseLocation.transform.position = this.transform.position; }
    }

    private void OnDestroy()
    {
        GameObject.Destroy(BaseLocation);
        WaveEvent.ChangePlayerObjective -= ChangeObjectiveTarget;

    }

    void Start()
    {

    }

    void ChangeObjectiveTarget(object sender, WaveEventArgs args)
    {
        Debug.Log("Changing objective target attempt");
        if (this.thisEntity.Owner != null)
        {
            if(args.player != null)
            {
                if(args.player == this.thisEntity.Owner)
                {
                    Debug.Log("Changing objective target" + args.Target.gameObject);
                    this.ObjectiveTarget = args.Target.gameObject;
                    this.Target = args.Target.gameObject;
                }
            }
        }
    }

    public override void ReloadAI()
    {
        //MUST HAVE MOBILITY AND OFFENSIVE TO BE ACTIVE
        
        FindAllEntitySystems();
    }

    void SpawnMinion()
    {
        if(!thisEntity.atr_Minion && !thisEntity.IsTower)
        { return; }
        if(thisEntity.atr_Minion.atr_CanSpawnMinions && thisEntity.atr_Minion.MinionType!= null)
        {
            if(thisEntity.atr_Minion.atr_CurMinionAmount < thisEntity.atr_Minion.atr_MaxMinionAmount)
            {
                if(CreatingMinion == false)
                {
                    Debug.Log("Spawning Minion");
                    StartCoroutine(CreateMinion(thisEntity.atr_Minion.atr_MinionCooldown));
                }
                
            }
        }
    }
    public override void UpdateMinionUpgrades(EntityPart Part)
    {
        if (!thisEntity.atr_Minion)
        { return; }
        foreach(GameObject Minion in thisEntity.atr_Minion.Minions)
        {
            if(Minion.TryGetComponent<AttributeUpgradeSystem>(out AttributeUpgradeSystem minUP))
            {
                minUP.AddPart(Part);
            }
        }  
    }
    public override void RemoveMinionUpgrades(EntityPart Part)
    {
        if (!thisEntity.atr_Minion)
        { return; }
        foreach (GameObject Minion in thisEntity.atr_Minion.Minions)
        {
            if (Minion.TryGetComponent<AttributeUpgradeSystem>(out AttributeUpgradeSystem minUP))
            {
                minUP.RemovePart(Part);
            }
        }
    }

    IEnumerator CreateMinion(float Cooldown)
    {
        CreatingMinion = true;
        yield return new WaitForSeconds(Cooldown);
        GameObject Minion = Instantiate(thisEntity.atr_Minion.MinionType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        if (Minion.TryGetComponent<Entity>(out Entity MinionEnt))
        {
            MinionEnt.Owner = thisEntity.Owner;
            MinionEnt.team = thisEntity.team;
            MinionEnt.Parent = this.gameObject;
            MinionEnt.atr_Perma = thisEntity.atr_Perma;
            foreach(EntityPart part in thisEntity.atr_Upgrade.EquippedEntityParts)
            {
                MinionEnt.atr_Upgrade.AddPart(part);
            }
            MinionEnt.IsTower = false;

        }
        thisEntity.atr_Minion.atr_CurMinionAmount++;
        thisEntity.atr_Minion.Minions.Add(Minion);
        CreatingMinion = false;
    }

    private void LateUpdate()
    {
        if (thisEntity)
        {
            if (thisEntity.atr_Mobility == null || thisEntity.atr_Offense == null)
            { this.AIEnabled = false; }
            Agent.speed = thisEntity.atr_Mobility.atr_Speed;
            EnemyStopDistance = thisEntity.atr_Offense.atr_AttackRange / 2;
            AIEnabled = true;
            CheckRangeForTargets();
        }
        else { this.AIEnabled = false; }
        SpawnMinion();

    }

    void CheckRangeForTargets()
    {
        if(Target)
        { return; }
        if (ObjectiveTarget)
        { return; }
        //Debug.Log("finding Target from circle check!");
        Collider2D[] NearbyEntities = Physics2D.OverlapCircleAll(this.transform.position, thisEntity.atr_Offense.atr_AttackRange);
        foreach(Collider2D n in NearbyEntities)
        {
           // Debug.Log("Targets:" + NearbyEntities.Length) ;
            if (n.gameObject.TryGetComponent<Entity>(out Entity ent))
            {
                if(ent.Owner != thisEntity.Owner && ent.team != thisEntity.team && !ent.IsInvincible && ent.CanDie && ent.CanDieAtZero && ent.atr_Defense)
                {
                    Target = n.gameObject;
                    return; 
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetDistances();
        ActivateAI();
        GetParentTarget();
    }
    void GetDistances()
    {
        if(Target)
        { DistanceFromTarget = Vector2.Distance(this.gameObject.transform.position, Target.transform.position);}
        if (BaseLocation)
        { DistanceFromBase = Vector2.Distance(this.gameObject.transform.position, BaseLocation.transform.position); }
        if (ObjectiveTarget)
        { DistanceFromObjectiveTarget = Vector2.Distance(this.gameObject.transform.position, ObjectiveTarget.transform.position); }
    }

    public void FaceTarget(Vector3 TargetPosition)
    {
        Vector2 DirectionToLook = TargetPosition - this.transform.position;

        float Angle = Mathf.Atan2(DirectionToLook.y, DirectionToLook.x) * Mathf.Rad2Deg;

        Quaternion RotationAmount = Quaternion.AngleAxis(Angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, RotationAmount, Agent.angularSpeed * Time.deltaTime);
    }
    int RotationDirection = 1;

    public void OrbitTarget(Vector3 TargetPosition)
    {
        //Debug.Log("Attempting Orbit");
        Vector3 axis = new Vector3(0, 0, 1);
        
        transform.RotateAround(TargetPosition, axis, Time.deltaTime * Agent.speed * 2 * RotationDirection);
    }

    void GetParentTarget()
    {
        if(!GetTargetFromParent)
        { return; }
        if (thisEntity.Parent == null)
        { return; }
        if(Target)
        { return; }
        if (thisEntity.Parent.TryGetComponent<EntityAI>(out EntityAI ParentAi))
        {
            if (ParentAi.Target)
            { this.Target = ParentAi.Target; }
        }
    }

    void ActivateAI()
    {
        if(!AIEnabled)
        {return;}
        //whenever there is an objective it probably means that this is going down the path to a base or in some weird mode
        if(ObjectiveTarget)
        {
            thisEntity.atr_Mobility.atr_TravelRange = Mathf.Infinity;
            thisEntity.atr_Mobility.atr_CanMove = true;
            if (Target == null)
            {Target = ObjectiveTarget;}
        }

        if(Target == null)
        {
            ToggleOffense(false);
        }
        if (Target)
        {
            FaceTarget(Target.transform.position);
            if (DistanceFromTarget >= thisEntity.atr_Offense.atr_AttackRange)
            {
                ToggleOffense(false);
            }
            else if (DistanceFromTarget < thisEntity.atr_Offense.atr_AttackRange)
            {
                if (!OffenseSystemsActive)
                { ToggleOffense(true); }
                if(DistanceFromTarget > thisEntity.atr_Mobility.atr_TravelRange)
                {
                    Target = null; return;
                }
            }

            if(thisEntity.atr_Mobility.atr_CanMove)
            {
                if(DistanceFromBase > thisEntity.atr_Mobility.atr_TravelRange)
                {
                    //Debug.Log("1");
                    Agent.SetDestination(this.BaseLocation.transform.position);
                    Target = null;
                }
                else if ( DistanceFromTarget > thisEntity.atr_Offense.atr_AttackRange * .9)
                {
                    //Debug.Log("2");
                    Agent.SetDestination(Target.transform.position);
                }
                else if (DistanceFromTarget <= thisEntity.atr_Offense.atr_AttackRange * .9)
                {
                    //Debug.Log("3");
                    Agent.SetDestination(this.Agent.transform.position);
                    
                }
            }
        }
    }
    IEnumerator StartWithDelay(EntitySystem ES, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (Target != null)
        {ES.Activate(Target);}
        else
        {ES.Activate();}
    }

    void ToggleOffense(bool Value)
    {
        foreach(EntitySystem ES in this.OffenseSystems)
        {
            if (Value == false)
            { ES.DeActivate(); OffenseSystemsActive = false; }
            else if(Value == true && !OffenseSystemsActive)
            {StartCoroutine( StartWithDelay(ES, OffenseStartDelay)); OffenseSystemsActive = true; }

        }
    }
    void ToggleDefense(bool Value)
    {
        foreach (EntitySystem ES in this.DefenseSystems)
        {
            if (Value == false)
            { ES.DeActivate(); DefenseSystemsActive = false; }
            else if (Value == true && !DefenseSystemsActive)
            { StartCoroutine(StartWithDelay(ES, DefenseStartDelay)); DefenseSystemsActive = true; }

        }
    }
}
