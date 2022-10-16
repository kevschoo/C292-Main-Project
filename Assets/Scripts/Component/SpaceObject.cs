using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class SpaceObject : MonoBehaviour
{
    [SerializeField] PermUpgrades permUpgrades;

    [SerializeField] OffensiveStats offensiveStats;

    [SerializeField] DefensiveStats defensiveStats;

    [SerializeField] MinionStat minionStats;

    [SerializeField] SpeedStat speedStats;

    [SerializeField] CostStat costStats;

    [SerializeField] UpgradeSlots upgradeSlots;

    [SerializeField] StatusEffectManager statusEffectManager;

    //[SerializeField] List<string> AvaliableComponents = new List<string>();

    [SerializeField] string Team;

    [SerializeField] Player Owner;

    [SerializeField] float HealthSpawnMod = 1.0f;

    [SerializeField] bool isInvincible = false;

    [SerializeField] bool canDie = true;

    [SerializeField] bool canDieAtZero = true;

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

    }


    private void Start()
    {
        UpgradeRecieved();
    }

    private void UpgradeRecieved()
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
    }
    void Death()
    {
        if(this.defensiveStats == null)
        { return; }

        if(canDie == false)
        { return; }

        if(canDieAtZero == false)
        { return; }

        if(defensiveStats._CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void ForceDeath()
    {
        Destroy(this.gameObject);
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
            if(offensiveStats != null)
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

}
