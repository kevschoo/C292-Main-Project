using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : Entity
{
    //Entity Manages all interactable units, towers, and objects that can deal with any of the Attribute stats
    //Alerts AI component of changes 
    //knows calculates all stats from components to set base values
    //middleman between interactions of health, damage, status effects, upgrades added, minion death, 

    //Outside Stats
    [field: SerializeField] public override SpriteRenderer SpriteRender { get; set; }
    [field: SerializeField] public override Sprite MySprite { get; set; }
    [field: SerializeField] public override GameObject Parent { get; set; }
    [field: SerializeField] public override Team team { get; set; }
    [field: SerializeField] public override Player Owner { get; set; }
    [field: SerializeField] public override PermanentAttributes atr_Perma { get; set; }
    [field: SerializeField] public  BaseBonus atr_Base { get; set; }

    [field: SerializeField] public LevelHandler levelHandler { get; set; }
    [field: SerializeField] public GameObject EnemyBase { get; set; }

    [field: SerializeField] public GameObject UnitSpawnLocation { get; set; }

    //Inside Stats to be obtained
    [field: SerializeField] public override string EntityName { get; set; }
    [field: SerializeField] public override AttributeOffense atr_Offense { get; set; }
    [field: SerializeField] public override AttributeDefense atr_Defense { get; set; }
    [field: SerializeField] public override AttributeMinions atr_Minion { get; set; }
    [field: SerializeField] public override AttributeMobility atr_Mobility { get; set; }
    [field: SerializeField] public override AttributeCost atr_Cost { get; set; }
    [field: SerializeField] public override AttributeUpgradeSystem atr_Upgrade { get; set; }
    [field: SerializeField] public override AttributeStatusEffect atr_Status { get; set; }
    [field: SerializeField] public override EntityAI entityAI { get; set; }

    //Baseline Bools and conditions for entities
    [field: SerializeField] public override bool IsTower { get; set; } = true;
    [field: SerializeField] public override float Price { get; set; } = 100f;
    [field: SerializeField] public float BaseIncomeGeneration { get; set; } = 100f;
    [field: SerializeField] public float IncomeGeneration { get; set; }
    [field: SerializeField] public float BaseIncomePerWave { get; set; } = 100f;
    [field: SerializeField] public float IncomePerWave { get; set; }
    [field: SerializeField] public override float HealthSpawnModifier { get; set; } = 1.0f;
    [field: SerializeField] public override float HealthRegenerationModifier { get; set; } = 1.0f;
    [field: SerializeField] public override bool CanRegenerate { get; set; } = false;
    [field: SerializeField] public override bool IsRegenerating { get; set; } = false;
    [field: SerializeField] public override bool IsInvincible { get; set; } = false;
    [field: SerializeField] public override bool CanDieAtZero { get; set; } = true;
    [field: SerializeField] public override bool CanDie { get; set; } = true;
    [field: SerializeField] public override bool FriendlyFire { get; set; }
    [field: SerializeField] public override bool MinionsDieOnDeath { get; set; } = false;
    [field: SerializeField] public bool IsDefending { get; set; }
    [field: SerializeField] public bool IsGeneratingIncome { get; set; }
    [field: SerializeField] public bool UnitCurrentlySpawning { get; set; }
    [field: SerializeField] public List<GameObject> CurrentWaveEnemies { get; set; }

    [field: SerializeField] public float WaveTimerMinCooldown { get; set; } = 15;
    private void Awake()
    {
        Debug.Log("Entity Ship: " + EntityName + " Spawned");
        if (this.Owner != null)
        {
            if (this.atr_Perma == null)
            {
                if (Owner.PlayerPermAttributes)
                { atr_Perma = Owner.PlayerPermAttributes; }
            }
        }
        if (this.gameObject.TryGetComponent<AttributeOffense>(out AttributeOffense atr_off))
        { atr_Offense = atr_off; }
        if (this.gameObject.TryGetComponent<AttributeDefense>(out AttributeDefense atr_def))
        { atr_Defense = atr_def; }
        if (this.gameObject.TryGetComponent<AttributeMinions>(out AttributeMinions atr_min))
        { atr_Minion = atr_min; }
        if (this.gameObject.TryGetComponent<AttributeMobility>(out AttributeMobility atr_mob))
        { atr_Mobility = atr_mob; }
        if (this.gameObject.TryGetComponent<AttributeCost>(out AttributeCost atr_cost))
        { atr_Cost = atr_cost; }
        if (this.gameObject.TryGetComponent<AttributeUpgradeSystem>(out AttributeUpgradeSystem atr_up))
        { atr_Upgrade = atr_up; }
        if (this.gameObject.TryGetComponent<AttributeStatusEffect>(out AttributeStatusEffect atr_SE))
        { atr_Status = atr_SE; }
        if (this.gameObject.TryGetComponent<EntityAI>(out EntityAI ent_Ai))
        { entityAI = ent_Ai; }
        if(Owner)
        {
            if(Owner.baseBonus)
            { this.atr_Base = Owner.baseBonus;}
        }


    }

    private void Start()
    {
        levelHandler = LevelHandler.Level;
        if (levelHandler == null)
        {
            Debug.Log("No Level Handler?");
        }
        UpgradeReceived();
        WaveEvent.WaveStart += OnWaveStart;
        WaveEvent.WaveEnd += OnWaveEnd;
        WaveEvent.ReverseModes += OnReversal;
    }
    void OnWaveStart(object sender, WaveEventArgs args)
    {

        if (!IsDefending)
        {
            Debug.Log("Wave Sending" + this.name);
            SpawnWave();
        }
        else
        {
            Debug.Log("Wave Coming" + this.name);
        }

    }
    void OnWaveEnd(object sender, WaveEventArgs args)
    {
        Debug.Log("Wave Ended");
        if(args.WaveNumber == levelHandler.CurrentWaveNumber)
        {
            Owner.Money += IncomePerWave;
        }

    }
    void OnReversal(object sender, WaveEventArgs args)
    {
        if (IsDefending)
        {
            Debug.Log("Now Attacking" + this.name);
            IsDefending = false;
            WaveEvent.InvokeChangePlayerObjective(this.Owner, EnemyBase.gameObject);
        }
        else if(!IsDefending)
        {
            //Debug.Log("Now Defending" + this.name);
            IsDefending = true;
        }
    }

    void SpawnWave()
    {
        Debug.Log("Should be spawning a wave");
        if (levelHandler.EnemyBaseWavesData != null)
        {
            foreach(WaveData waveData in levelHandler.EnemyBaseWavesData.WaveList)
            {
                if(waveData.WaveNumber == levelHandler.CurrentWaveNumber)
                {
                    Debug.Log("Entity Attempt" + waveData.Entity.name);
                    if (waveData.Entity)
                    {
                        GenerateUnits(waveData.NumberToSpawn, waveData.Entity, waveData.SpawnDelay);
                        StartCoroutine(WaveWaitCooldown(WaveTimerMinCooldown + (waveData.SpawnDelay * waveData.NumberToSpawn)));
                    }
                    
                }
            }
        }
    }
    IEnumerator WaveWaitCooldown(float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if(levelHandler.IsWaveLevel && levelHandler.CurrentWaveNumber <= levelHandler.LastWaveNumber)
        {
            levelHandler.StartNextWave();
        }
    }
    void GenerateUnits(int Amount, GameObject Unit, float Delay)
    {

        if(Amount > 0)
        {
            if(!UnitCurrentlySpawning)
            {
                StartCoroutine(CreateWave(Amount, Unit, Delay));
            }
        }
    }

    IEnumerator CreateWave(int Amount, GameObject Unit, float Delay)
    {
        UnitCurrentlySpawning = true;
        
        GameObject NewUnit = Instantiate(Unit, new Vector3(UnitSpawnLocation.transform.position.x, UnitSpawnLocation.transform.position.y, UnitSpawnLocation.transform.position.z), Quaternion.identity);
        
        if (NewUnit.TryGetComponent<Entity>(out Entity UnitInfo))
        {
            UnitInfo.Parent = this.gameObject;
            UnitInfo.team = this.team;
            UnitInfo.Owner = this.Owner;
            UnitInfo.atr_Perma = this.atr_Perma;
        }
        if (NewUnit.TryGetComponent<EntityAI>(out EntityAI UnitAI))
        {
            if (EnemyBase)
            { UnitAI.ObjectiveTarget = EnemyBase; }
            UnitAI.ReloadAI();
        }
        CurrentWaveEnemies.Add(NewUnit);
        yield return new WaitForSeconds(Delay);
        UnitCurrentlySpawning = false;
        GenerateUnits(Amount - 1, Unit, Delay);
    }


    private void Update()
    {
        if (levelHandler.isWaveActive == true)
        {
            if(!IsGeneratingIncome)
            {
                StartCoroutine(GenerateMoney());
            }
            if(!this.IsDefending && !UnitCurrentlySpawning)
            {
                if (CurrentWaveEnemies.Count == 0)
                {
                    Debug.Log("All this waves enemies have died");

                    levelHandler.StartNextWave();
                }
            }
        }
        
        CheckForDeath();
        Regenerate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
        //Maybe move this to AI type only
        if (collision.gameObject.TryGetComponent<Entity>(out Entity EData))
        {

            if (EData.team != this.team && EData.Owner != this.Owner)
            {
                if (this.entityAI != null)
                { entityAI.OnDamageTaken(EData.gameObject); }
            }

        }
        if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bulletData))
        {

            //Do not allow bullets to hit players owned ships
            if (bulletData.Parent == this.gameObject)
            { return; }
            //If the object doesnt have health don't bother
            if (this.atr_Defense == null)
            {
                // Debug.Log("No HP BAR");
                bulletData.MaxTargetsToPenetrate -= 1;
                if (bulletData.MaxTargetsToPenetrate <= 0)
                { Destroy(collision.gameObject); }

                return;
            }

            if (bulletData.team != this.team || bulletData.team == null)
            {
                if (bulletData.Owner == this.Owner && bulletData.AllowFriendlyFire)
                {
                    //Debug.Log("Friendly Fire, From Ourselves");
                    DamageCalculator(bulletData, this.atr_Defense, 1f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }
                }
                else if (bulletData.Owner != this.Owner)
                {
                    //Debug.Log("Enemy Fire, Enemy Has No Team");
                    DamageCalculator(bulletData, this.atr_Defense, 1.5f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (entityAI != null)
                    {
                        Debug.Log("Ayo This guy hiting us:" + bulletData.Parent.name);
                        entityAI.OnDamageTaken(bulletData.Parent.gameObject);
                    }
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }

                }
            }
            else if (bulletData.team == this.team)
            {
                if (bulletData.Owner == this.Owner && bulletData.AllowFriendlyFire)
                {
                    //Debug.Log("Friendly Fire, From Ourselves");
                    DamageCalculator(bulletData, this.atr_Defense, 1f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }
                }
                else if (bulletData.Owner != this.Owner)
                {
                    //Debug.Log("Enemy Fire, From a Teammate");
                    DamageCalculator(bulletData, this.atr_Defense, 1f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }
                }
            }
            else
            {
                Debug.Log("Missed one of 4 options");
            }

        }
    }
    void DamageCalculator(Bullet bull, AttributeDefense stats, float ExtraDamageModifier)
    {
        //Debug.Log("Damage:" + bull.ParentDamage + ", Team:" + bull.Team + ", Player:" + bull.Owner + ", Penetration:" + bull.ParentPenetration + ", DMGI:" + bull.ParentDamageIncrease);

        float remainingDefense = stats.atr_Defense - bull.ParentPenetration;
        if (remainingDefense <= 0)
        { remainingDefense = 0; }

        float DamageToTake = bull.ParentDamage - remainingDefense;


        //Debug.Log("DamageInTotalBeforeBonus:" + damageMinusDefense);
        float DamageModifier = (((float)bull.ParentDamageIncrease - (float)stats.atr_BaseDamageReduction + 100) / 100);
        float TotalDamage = DamageModifier * DamageToTake * ExtraDamageModifier;
        float FinalDamage = TotalDamage;
        UpdateHealth(-DamageToTake);
        // Debug.Log("DamageInTotalAfterBonus:" + DamageToTake);

    }

    void Regenerate()
    {
        if (CanRegenerate == false)
        { return; }
        if (IsRegenerating == true)
        { return; }
        if (this.atr_Defense != null)
        {
            if (this.atr_Defense.atr_Regeneration != 0)
            { StartCoroutine(RegenerateHealth()); }
        }
    }

    void CheckForDeath()
    {
        if (this.atr_Defense == null)
        { return; }

        if (CanDie == false)
        { return; }

        if (CanDieAtZero == false)
        { return; }

        if (this.atr_Defense.atr_CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public override void KillMinions()
    {
        if (!this.atr_Minion)
        { return; }
        if (this.atr_Minion.Minions != null)
        {
            if (this.atr_Minion.Minions.Count != 0)
            {
                foreach (GameObject Minion in this.atr_Minion.Minions)
                { Destroy(Minion); }
            }
        }
    }
    public override void MinionDeath(GameObject Minion)
    {
        if (CurrentWaveEnemies.Contains(Minion))
        {
            CurrentWaveEnemies.Remove(Minion);
        }

        if (!this.atr_Minion)
        {return;}
        if (this.atr_Minion.Minions != null)
        {
            if (this.atr_Minion.Minions.Count != 0)
            {
                this.atr_Minion.Minions.Remove(Minion);
                this.atr_Minion.atr_CurMinionAmount = this.atr_Minion.Minions.Count;
            }
        }
    }
    public override void InformParentOfDeath()
    {
        if (this.Parent != null)
        {
            if (Parent.GetComponent<Entity>())
            {
                Parent.GetComponent<Entity>().MinionDeath(this.gameObject);
            }
            if (Parent.GetComponent<EntityTower>())
            {
                Parent.GetComponent<EntityTower>().StationedEntityDeath();
            }

        }
    }

    private void UpgradeReceived()
    {
        if (this.atr_Status != null)
        { this.atr_Status.ClearAllEffects(); }

        CalculateAttributes();
        if (this.atr_Defense != null)
        { atr_Defense.atr_CurrentHealth = Mathf.RoundToInt(atr_Defense.atr_MaxHealth * this.HealthSpawnModifier); }
    }
    IEnumerator RegenerateHealth()
    {
        this.IsRegenerating = true;
        yield return new WaitForSeconds(1f * this.HealthRegenerationModifier);
        UpdateHealth(this.atr_Defense.atr_Regeneration);
        IsRegenerating = false;
    }

    IEnumerator GenerateMoney()
    {
        IsGeneratingIncome = true;
        yield return new WaitForSeconds(10f);
        Owner.Money += this.IncomeGeneration;
        IsGeneratingIncome = false;
    }

    void UpdateHealth(float Change)
    {
        if (this.atr_Defense != null)
        {
            if (Change > 0)
            {
                atr_Defense.atr_CurrentHealth += Change;
                if (atr_Defense.atr_CurrentHealth > atr_Defense.atr_MaxHealth)
                { atr_Defense.atr_CurrentHealth = atr_Defense.atr_MaxHealth; }
            }
            else if (Change < 0 && this.IsInvincible == false)
            {
                atr_Defense.atr_CurrentHealth += Change;
                if (this.CanDie == true && atr_Defense.atr_CurrentHealth <= 0)
                {
                    Destroy(this.gameObject);
                }
                else if (this.CanDieAtZero == false && atr_Defense.atr_CurrentHealth <= 0)
                {
                    Debug.Log("Health at zero");
                    //maybe future repair mechanic or something for healthatzero and disable ai
                    if (this.gameObject.GetComponent<EntityAI>() != null)
                    {
                        this.gameObject.GetComponent<EntityAI>().AIEnabled = false;
                    }
                }
            }
        }

    }

    private void OnDestroy()
    {
        Debug.Log("Base DESTROYED!" + this.EntityName);
        WaveEvent.WaveStart -= OnWaveStart;
        WaveEvent.WaveEnd -= OnWaveEnd;
        WaveEvent.ReverseModes -= OnReversal;

        InformParentOfDeath();
        if (MinionsDieOnDeath == true)
        { KillMinions(); }
    }
    public override void CalculateAttributes()
    {
        if(atr_Base)
        {
            this.IncomeGeneration = BaseIncomeGeneration + atr_Base.atr_IncomeGeneration;
            this.IncomePerWave = BaseIncomePerWave + atr_Base.atr_IncomePerWave;
            this.HealthRegenerationModifier = atr_Base.atr_HealthRegenerationModifier;
        }
        else
        {
            this.IncomeGeneration = BaseIncomeGeneration;
            this.IncomePerWave = BaseIncomePerWave;
        }


        if (atr_Upgrade == null)
        {
            if (atr_Perma == null)
            {
                if (atr_Offense)
                { ATRCalculator.Recalculate(atr_Offense); }
                if (atr_Defense)
                { ATRCalculator.Recalculate(atr_Defense); }
                if (atr_Minion)
                { ATRCalculator.Recalculate(atr_Minion); }
                if (atr_Mobility)
                { ATRCalculator.Recalculate(atr_Mobility); }
                if (atr_Cost)
                { ATRCalculator.Recalculate(atr_Cost); }
            }
            if (atr_Perma != null)
            {
                if (atr_Offense)
                { ATRCalculator.Recalculate(atr_Offense, atr_Perma); }
                if (atr_Defense)
                { ATRCalculator.Recalculate(atr_Defense, atr_Perma); }
                if (atr_Minion)
                { ATRCalculator.Recalculate(atr_Minion, atr_Perma); }
                if (atr_Mobility)
                { ATRCalculator.Recalculate(atr_Mobility, atr_Perma); }
                if (atr_Cost)
                { ATRCalculator.Recalculate(atr_Cost, atr_Perma); }
            }
        }
        if (atr_Upgrade != null)
        {
            if (atr_Perma == null)
            {
                if (atr_Offense)
                { ATRCalculator.Recalculate(atr_Offense, atr_Upgrade); }
                if (atr_Defense)
                { ATRCalculator.Recalculate(atr_Defense, atr_Upgrade); }
                if (atr_Minion)
                { ATRCalculator.Recalculate(atr_Minion, atr_Upgrade); }
                if (atr_Mobility)
                { ATRCalculator.Recalculate(atr_Mobility, atr_Upgrade); }
                if (atr_Cost)
                { ATRCalculator.Recalculate(atr_Cost, atr_Upgrade); }
            }
            if (atr_Perma != null)
            {
                if (atr_Offense)
                { ATRCalculator.Recalculate(atr_Offense, atr_Perma, atr_Upgrade); }
                if (atr_Defense)
                { ATRCalculator.Recalculate(atr_Defense, atr_Perma, atr_Upgrade); }
                if (atr_Minion)
                { ATRCalculator.Recalculate(atr_Minion, atr_Perma, atr_Upgrade); }
                if (atr_Mobility)
                { ATRCalculator.Recalculate(atr_Mobility, atr_Perma, atr_Upgrade); }
                if (atr_Cost)
                { ATRCalculator.Recalculate(atr_Cost, atr_Perma, atr_Upgrade); }
            }
        }
    }

}