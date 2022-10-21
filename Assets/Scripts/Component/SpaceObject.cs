using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class SpaceObject : MonoBehaviour
{

    [field: SerializeField] public PermUpgrades permUpgrades { get; set; }

    [field: SerializeField] public OffensiveStats offensiveStats { get; set; }

    [field: SerializeField] public DefensiveStats defensiveStats { get; set; }

    [field: SerializeField] public MinionStat minionStats { get; set; }

    [field: SerializeField] public SpeedStat speedStats { get; set; }

    [field: SerializeField] public CostStat costStats { get; set; }

    [field: SerializeField] public UpgradeSlots upgradeSlots { get; set; }

    [field: SerializeField] public StatusEffectManager statusEffectManager { get; set; }

    [field: SerializeField] public ObjectAI objectAI { get; set; }

    //[SerializeField] List<string> AvaliableComponents = new List<string>();

    [field: SerializeField] public string Team { get; set; }

    [field: SerializeField] public Player Owner { get; set; }

    [SerializeField] float HealthSpawnMod = 1.0f;

    [SerializeField] float HealthRegenMod = 1.0f;

    [SerializeField] bool isRegenerating = false;

    [SerializeField] bool isInvincible = false;

    [SerializeField] bool canDie = true;

    [SerializeField] bool canDieAtZero = true;
    [SerializeField] public bool AllowFriendlyFire { get; set; }

    private void Awake()
    {
        Debug.Log("Ship Spawned");
        if (this.gameObject.GetComponent<UpgradeSlots>())
        {
            upgradeSlots = this.gameObject.GetComponent<UpgradeSlots>();
            // upgradeSlots.CalculateBonuses();
        }

        if (this.gameObject.GetComponent<OffensiveStats>())
        {
            offensiveStats = this.gameObject.GetComponent<OffensiveStats>();
            //AvaliableComponents.Add(offensiveStats.name);
        }

        if (this.gameObject.GetComponent<DefensiveStats>())
        {
            defensiveStats = this.gameObject.GetComponent<DefensiveStats>();
            //AvaliableComponents.Add(defensiveStats.name);
        }
        if (this.gameObject.GetComponent<MinionStat>())
        {
            minionStats = this.gameObject.GetComponent<MinionStat>();
            //AvaliableComponents.Add(minionStats.name);
        }
        if (this.gameObject.GetComponent<SpeedStat>())
        {
            speedStats = this.gameObject.GetComponent<SpeedStat>();
            //AvaliableComponents.Add(speedStats.name);
        }
        if (this.gameObject.GetComponent<CostStat>())
        {
            costStats = this.gameObject.GetComponent<CostStat>();
            //AvaliableComponents.Add(costStats.name);
        }
        if (this.gameObject.GetComponent<StatusEffectManager>())
        {
            statusEffectManager = this.gameObject.GetComponent<StatusEffectManager>();
            //AvaliableComponents.Add(StatusEffectManager.name);
        }
        if (this.gameObject.GetComponent<ObjectAI>())
        {
            objectAI = this.gameObject.GetComponent<ObjectAI>();
        }

    }



    public void SetOwner(Player Owner)
    {
        this.Owner = Owner;
    }
    public void SetTeam(string Team)
    {
        this.Team = Team;
    }

    private void Start()
    {
        UpgradeReceived();

    }

    private void UpgradeReceived()
    {
        if (this.statusEffectManager != null)
        { this.statusEffectManager.ClearAllEffects(); }

        CalculateStatus();
        if (this.defensiveStats != null)
        {
            defensiveStats._CurrentHealth = Mathf.RoundToInt(defensiveStats._MaxHealth * HealthSpawnMod);
        }
    }
    private void Update()
    {
        Death();
        if(defensiveStats != null)
        {
            if (this.defensiveStats._HealthRegen != 0)
            {
                if(isRegenerating == false)
                {
                    StartCoroutine(RegenerateHealth());
                }
               
            }
        }
    }
    void Death()
    {
        if (this.defensiveStats == null)
        { return; }

        if (canDie == false)
        { return; }

        if (canDieAtZero == false)
        { return; }

        if (defensiveStats._CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void ForceDeath()
    {
        Destroy(this.gameObject);
    }

    void UpdateHealth(int Change)
    {
        if (defensiveStats != null)
        {
            if (Change >= 0)
            {
                defensiveStats._CurrentHealth += Change;
                if (defensiveStats._CurrentHealth > defensiveStats._MaxHealth)
                {
                    defensiveStats._CurrentHealth = defensiveStats._MaxHealth;

                }
            }
            else if (Change < 0 && this.isInvincible == false)
            {
                defensiveStats._CurrentHealth += Change;
                if (canDie == true && defensiveStats._CurrentHealth <= 0)
                {
                    ForceDeath();
                }
                else if(canDieAtZero == false && defensiveStats._CurrentHealth <= 0)
                {
                    Debug.Log("Health at zero");
                    //maybe future repair mechanic or something for healthatzero and disable ai
                    if(this.gameObject.GetComponent<ShipNavMeshAI>() != null)
                    {
                        this.gameObject.GetComponent<ShipNavMeshAI>().ChangeAI(false);
                    }
                }
            }
        }

    }

    /*
    bruh moment super if moment! could be switchstatement if wasnt so bad!
     * foreach(string compName in this.AvaliableComponents)
            {
                GetComponent(Type.GetType(compName));
                ObjectStatusHandler.Recalculate(GetComponent(Type.GetType(compName)));
            }
     */
    void CalculateStatus()
    {
        if (permUpgrades == null && upgradeSlots == null)
        {
            if (offensiveStats != null)
            { ObjectStatusHandler.Recalculate(offensiveStats); }
            if (defensiveStats != null)
            { ObjectStatusHandler.Recalculate(defensiveStats); }
            if (minionStats != null)
            { ObjectStatusHandler.Recalculate(minionStats); }
            if (speedStats != null)
            { ObjectStatusHandler.Recalculate(speedStats); }
            if (costStats != null)
            { ObjectStatusHandler.Recalculate(costStats); }

        }
        else if (permUpgrades != null && upgradeSlots == null)
        {
            if (offensiveStats != null)
            { ObjectStatusHandler.Recalculate(offensiveStats, permUpgrades); }
            if (defensiveStats != null)
            { ObjectStatusHandler.Recalculate(defensiveStats, permUpgrades); }
            if (minionStats != null)
            { ObjectStatusHandler.Recalculate(minionStats, permUpgrades); }
            if (speedStats != null)
            { ObjectStatusHandler.Recalculate(speedStats, permUpgrades); }
            if (costStats != null)
            { ObjectStatusHandler.Recalculate(costStats, permUpgrades); }
        }
        else if (permUpgrades == null && upgradeSlots != null)
        {
            if (offensiveStats != null)
            { ObjectStatusHandler.Recalculate(offensiveStats, upgradeSlots); }
            if (defensiveStats != null)
            { ObjectStatusHandler.Recalculate(defensiveStats, upgradeSlots); }
            if (minionStats != null)
            { ObjectStatusHandler.Recalculate(minionStats, upgradeSlots); }
            if (speedStats != null)
            { ObjectStatusHandler.Recalculate(speedStats, upgradeSlots); }
            if (costStats != null)
            { ObjectStatusHandler.Recalculate(costStats, upgradeSlots); }
        }
        else
        {
            if (offensiveStats != null)
            { ObjectStatusHandler.Recalculate(offensiveStats, permUpgrades, upgradeSlots); }
            if (defensiveStats != null)
            { ObjectStatusHandler.Recalculate(defensiveStats, permUpgrades, upgradeSlots); }
            if (minionStats != null)
            { ObjectStatusHandler.Recalculate(minionStats, permUpgrades, upgradeSlots); }
            if (speedStats != null)
            { ObjectStatusHandler.Recalculate(speedStats, permUpgrades, upgradeSlots); }
            if (costStats != null)
            { ObjectStatusHandler.Recalculate(costStats, permUpgrades, upgradeSlots); }
        }

    }

    //should be recoded
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
        if (collision.gameObject.GetComponent<SpaceObject>())
        {

        }
        if (collision.gameObject.GetComponent<Bullet>())
        {
            Bullet bulletData = (Bullet)collision.gameObject.GetComponent<Bullet>();
            
            //Do not allow bullets to hit players owned ships
            if (bulletData.Spawner == this.gameObject)
            { return; }
            //If the object doesnt have health don't bother
            if (this.defensiveStats == null)
            {
                Destroy(collision.gameObject);
                return;
            }
            //Are they same team and friendlyfire
            if (bulletData.Team == this.Team && bulletData.AllowFriendlyFire)
            {
                //is the bullet from us or friend
                if(bulletData.Owner == this.Owner)
                {
                    DamageCalculator(bulletData, this.defensiveStats, .5f);
                    Destroy(collision.gameObject);
                }
                //from friend
                if (bulletData.Owner != this.Owner)
                {
                    DamageCalculator(bulletData, this.defensiveStats, .5f);
                    Destroy(collision.gameObject);
                }
            }
            //if they are not our team
            if (bulletData.Team != this.Team)
            {
                DamageCalculator(bulletData, this.defensiveStats, 1f);
                if(objectAI != null)
                {
                    Debug.Log("Ayo This guy hiting us:" + bulletData.Spawner.name);
                    objectAI.DamageTaken(bulletData.Spawner.gameObject);
                }
                Destroy(collision.gameObject);

            }
        }
    }

   
    //bruh float casting moment
    void DamageCalculator(Bullet bull, DefensiveStats stats, float extraMod)
        {
        //Debug.Log("Damage:" + bull.ParentDamage + ", Team:" + bull.Team + ", Player:" + bull.Owner + ", Penetration:" + bull.ParentPenetration + ", DMGI:" + bull.ParentDamageIncrease);

        int penetratedDefense = stats._Defense - bull.ParentPenetration;
            if (penetratedDefense <= 0)
            { penetratedDefense = 0; }
            int damageMinusDefense = bull.ParentDamage - penetratedDefense;
            //Debug.Log("DamageInTotalBeforeBonus:" + damageMinusDefense);
            float DamageMod = (((float)bull.ParentDamageIncrease - (float)stats._DamageReduction + 100) / 100);
            float totalDamage = DamageMod * (float)damageMinusDefense * extraMod;
            int DamageToTake = Mathf.RoundToInt(totalDamage);
            UpdateHealth(-DamageToTake);
           // Debug.Log("DamageInTotalAfterBonus:" + DamageToTake);
            
        }
    IEnumerator RegenerateHealth()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(1f * HealthRegenMod);
        UpdateHealth(this.defensiveStats._HealthRegen);
        isRegenerating = false;
    }
}

