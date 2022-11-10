using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class EntityShip : Entity
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
    [field: SerializeField] public override float HealthSpawnModifier { get; set; } = 1.0f;
    [field: SerializeField] public override float HealthRegenerationModifier { get; set; } = 1.0f;
    [field: SerializeField] public override bool CanRegenerate { get; set; } = false;
    [field: SerializeField] public override bool IsRegenerating { get; set; } = false;
    [field: SerializeField] public override bool IsInvincible { get; set; } = false;
    [field: SerializeField] public override bool CanDieAtZero { get; set; } = true;
    [field: SerializeField] public override bool CanDie { get; set; } = true;
    [field: SerializeField] public override bool FriendlyFire { get; set; }
    [field: SerializeField] public override bool MinionsDieOnDeath { get; set; } = false;

    private void Awake()
    {
        //Debug.Log("Entity Ship: " + EntityName + " Spawned");
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
        
    }

    private void Start()
    {
        UpgradeReceived();
        
    }

    private void Update()
    {
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
                {entityAI.OnDamageTaken(EData.gameObject);}
            }

        }
        if (collision.gameObject.TryGetComponent<Bullet>( out Bullet bulletData))
        {

            //Do not allow bullets to hit players owned ships
            if (bulletData.Parent == this.gameObject)
            { return; }
            //If the object doesnt have health don't bother
            if (this.atr_Defense == null)
            {
               // Debug.Log("No HP BAR");
                bulletData.MaxTargetsToPenetrate -= 1;
                if(bulletData.MaxTargetsToPenetrate <= 0)
                {Destroy(collision.gameObject);}

                return;
            }

            if(bulletData.team != this.team || bulletData.team == null)
            {
                if (bulletData.Owner == this.Owner && bulletData.AllowFriendlyFire)
                {
                    //Debug.Log("Friendly Fire, From Ourselves");
                    DamageCalculator(bulletData, this.atr_Defense, .5f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }
                }
                else if(bulletData.Owner != this.Owner)
                {
                    //Debug.Log("Enemy Fire, Enemy Has No Team");
                    DamageCalculator(bulletData, this.atr_Defense, 1f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (entityAI != null)
                    {
                        entityAI.OnDamageTaken(bulletData.Parent.gameObject);
                    }
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }

                }
            }
            else if(bulletData.team == this.team)
            {
                if (bulletData.Owner == this.Owner && bulletData.AllowFriendlyFire)
                {
                    //Debug.Log("Friendly Fire, From Ourselves");
                    DamageCalculator(bulletData, this.atr_Defense, .5f);
                    bulletData.MaxTargetsToPenetrate -= 1;
                    if (bulletData.MaxTargetsToPenetrate <= 0)
                    { Destroy(collision.gameObject); }
                }
                else if (bulletData.Owner != this.Owner)
                {
                    //Debug.Log("Enemy Fire, From a Teammate");
                    DamageCalculator(bulletData, this.atr_Defense, .75f);
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
        if(CanRegenerate == false)
        { return; }
        if(IsRegenerating == true)
        { return; }
        if (this.atr_Defense != null)
        {
            if (this.atr_Defense.atr_Regeneration != 0)
            {StartCoroutine(RegenerateHealth()); }
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
        if (!this.atr_Minion)
        { return; }
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
            if(Parent.GetComponent<Entity>())
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
        {atr_Defense.atr_CurrentHealth = Mathf.RoundToInt(atr_Defense.atr_MaxHealth * this.HealthSpawnModifier);}
    }
    IEnumerator RegenerateHealth()
    {
        this.IsRegenerating = true;
        yield return new WaitForSeconds(1f * this.HealthRegenerationModifier);
        UpdateHealth(this.atr_Defense.atr_Regeneration);
        IsRegenerating = false;
    }

    void UpdateHealth(float Change)
    {
        if (this.atr_Defense != null)
        {
            if (Change > 0)
            {
                atr_Defense.atr_CurrentHealth += Change;
                if (atr_Defense.atr_CurrentHealth > atr_Defense.atr_MaxHealth)
                {atr_Defense.atr_CurrentHealth = atr_Defense.atr_MaxHealth;}
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
        InformParentOfDeath();
        if(MinionsDieOnDeath == true)
        {KillMinions();}
    }
    public override void CalculateAttributes()
    {
        if(atr_Upgrade == null)
        {
            if(atr_Perma == null)
            {
                if(atr_Offense)
                {ATRCalculator.Recalculate(atr_Offense);}
                if(atr_Defense)
                {ATRCalculator.Recalculate(atr_Defense);}
                if(atr_Minion)
                {ATRCalculator.Recalculate(atr_Minion);}
                if(atr_Mobility)
                {ATRCalculator.Recalculate(atr_Mobility);}
                if(atr_Cost)
                {ATRCalculator.Recalculate(atr_Cost);}
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
                { ATRCalculator.Recalculate(atr_Minion,atr_Upgrade); }
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
