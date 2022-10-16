using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectStatusHandler 
{

    //Defensive Handler-------------------------------------------------------------------------------------------------------------------
    public static void Recalculate(DefensiveStats stat)
    {
        stat._MaxHealth = stat._BaseMaxHealth;
        stat._Defense = stat._BaseDefense;
        stat._DamageReduction = stat._BaseDamageReduction;
        stat._HealthRegen = stat._BaseHealthRegen;
    }
    public static void Recalculate(DefensiveStats stat, PermUpgrades permUpgrades)
    {
        stat._MaxHealth = stat._BaseMaxHealth + permUpgrades.HealthBonus;
        stat._Defense = stat._BaseDefense + permUpgrades.DefenseBonus;
        stat._DamageReduction = stat._BaseDamageReduction + permUpgrades.DamageReductionBonus;
        stat._HealthRegen = stat._BaseHealthRegen + permUpgrades.HealthRegenBonus;
    }
    public static void Recalculate(DefensiveStats stat, PermUpgrades permUpgrades, UpgradeSlots upgradeSlots)
    {
        stat._MaxHealth = stat._BaseMaxHealth + permUpgrades.HealthBonus + upgradeSlots.HealthBonus;
        stat._Defense = stat._BaseDefense + permUpgrades.DefenseBonus + upgradeSlots.DefenseBonus;
        stat._DamageReduction = stat._BaseDamageReduction + permUpgrades.DamageReductionBonus + upgradeSlots.DamageReductionBonus;
        stat._HealthRegen = stat._BaseHealthRegen + permUpgrades.HealthRegenBonus + upgradeSlots.HealthRegenBonus;
    }
    public static void Recalculate(DefensiveStats stat, UpgradeSlots upgradeSlots)
    {
        stat._MaxHealth = stat._BaseMaxHealth + upgradeSlots.HealthBonus;
        stat._Defense = stat._BaseDefense + upgradeSlots.DefenseBonus;
        stat._DamageReduction = stat._BaseDamageReduction + upgradeSlots.DamageReductionBonus;
        stat._HealthRegen = stat._BaseHealthRegen + upgradeSlots.HealthRegenBonus;
    }

    //Offensive Handler-------------------------------------------------------------------------------------------------------------------

    public static void Recalculate(OffensiveStats stat)
    {
        stat._Damage = stat._BaseDamage;
        stat._Penetration = stat._BasePenetration;
        stat._DamageIncrease = stat._BaseDamageIncrease;
        stat._AttackRange = stat._BaseAttackRange;
        stat._AttackSpeed = stat._BaseAttackSpeed;
    }
    public static void Recalculate(OffensiveStats stat, PermUpgrades permUpgrades)
    {
        stat._Damage = stat._BaseDamage + permUpgrades.DamageBonus;
        stat._Penetration = stat._BasePenetration + permUpgrades.PenetrationBonus;
        stat._DamageIncrease = stat._BaseDamageIncrease + permUpgrades.DamageIncreaseBonus;
        stat._AttackRange = stat._BaseAttackRange + permUpgrades.AttackRangeBonus;
        stat._AttackSpeed = stat._BaseAttackSpeed * permUpgrades.AttackSpeedBonus;
    }
    public static void Recalculate(OffensiveStats stat, PermUpgrades permUpgrades, UpgradeSlots upgradeSlots)
    {
        stat._Damage = stat._BaseDamage + permUpgrades.DamageBonus + upgradeSlots.DamageBonus;
        stat._Penetration = stat._BasePenetration + permUpgrades.PenetrationBonus + upgradeSlots.PenetrationBonus;
        stat._DamageIncrease = stat._BaseDamageIncrease + permUpgrades.DamageIncreaseBonus + upgradeSlots.DamageIncreaseBonus;
        stat._AttackRange = stat._BaseAttackRange + permUpgrades.AttackRangeBonus + upgradeSlots.AttackRangeBonus;
        stat._AttackSpeed = stat._BaseAttackSpeed * permUpgrades.AttackSpeedBonus * upgradeSlots.AttackSpeedBonus;
    }
    public static void Recalculate(OffensiveStats stat, UpgradeSlots upgradeSlots)
    {
        stat._Damage = stat._BaseDamage + upgradeSlots.DamageBonus;
        stat._Penetration = stat._BasePenetration + upgradeSlots.PenetrationBonus;
        stat._DamageIncrease = stat._BaseDamageIncrease +  upgradeSlots.DamageIncreaseBonus;
        stat._AttackRange = stat._BaseAttackRange + upgradeSlots.AttackRangeBonus;
        stat._AttackSpeed = stat._BaseAttackSpeed * upgradeSlots.AttackSpeedBonus;
    }

    //Minion Handler-------------------------------------------------------------------------------------------------------------------
    public static void Recalculate(MinionStat stat)
    {
        stat._MaxMinionAmount = stat._BaseMaxMinionAmount;
        stat._MinionCooldown = stat._BaseMinionCooldown;
    }

    public static void Recalculate(MinionStat stat, PermUpgrades permUpgrades)
    {
        stat._MaxMinionAmount = stat._BaseMaxMinionAmount + permUpgrades.MinionAmountBonus;
        stat._MinionCooldown = stat._BaseMinionCooldown *(permUpgrades.MinionCooldownBonus);
    }
    public static void Recalculate(MinionStat stat, PermUpgrades permUpgrades, UpgradeSlots upgradeSlots)
    {
        stat._MaxMinionAmount = stat._BaseMaxMinionAmount + permUpgrades.MinionAmountBonus + upgradeSlots.MinionAmountBonus;
        stat._MinionCooldown = stat._BaseMinionCooldown * permUpgrades.MinionCooldownBonus * upgradeSlots.MinionCooldownBonus;
    }
    public static void Recalculate(MinionStat stat, UpgradeSlots upgradeSlots)
    {
        stat._MaxMinionAmount = stat._BaseMaxMinionAmount + upgradeSlots.MinionAmountBonus;
        stat._MinionCooldown = stat._BaseMinionCooldown * upgradeSlots.MinionCooldownBonus;
    }

    //Speed Handler-------------------------------------------------------------------------------------------------------------------
    public static void Recalculate(SpeedStat stat)
    {
        stat._Speed = stat._BaseSpeed;
    }
    public static void Recalculate(SpeedStat stat, PermUpgrades permUpgrades)
    {
        stat._Speed = stat._BaseSpeed + permUpgrades.SpeedBonus;
    }
    public static void Recalculate(SpeedStat stat, PermUpgrades permUpgrades, UpgradeSlots upgradeSlots)
    {
        stat._Speed = stat._BaseSpeed + permUpgrades.SpeedBonus + upgradeSlots.SpeedBonus;
    }
    public static void Recalculate(SpeedStat stat, UpgradeSlots upgradeSlots)
    {
        stat._Speed = stat._BaseSpeed + upgradeSlots.SpeedBonus;
    }

    //Cost Handler-------------------------------------------------------------------------------------------------------------------
    public static void Recalculate(CostStat stat)
    {
        stat._Cost = stat._BaseCost;
        stat._Upkeep = stat._BaseUpkeep;
    }
    public static void Recalculate(CostStat stat, PermUpgrades permUpgrades)
    {
        stat._Cost = stat._BaseCost * permUpgrades.CostBonus;
        stat._Upkeep = stat._BaseUpkeep * permUpgrades.CostBonus;
    }
    public static void Recalculate(CostStat stat, PermUpgrades permUpgrades, UpgradeSlots upgradeSlots)
    {
        stat._Cost = stat._BaseCost * permUpgrades.CostBonus * upgradeSlots.CostBonus;
        stat._Upkeep = stat._BaseUpkeep * permUpgrades.CostBonus * upgradeSlots.CostBonus;
    }
    public static void Recalculate(CostStat stat, UpgradeSlots upgradeSlots)
    {
        stat._Cost = stat._BaseCost * upgradeSlots.CostBonus;
        stat._Upkeep = stat._BaseUpkeep * upgradeSlots.CostBonus;
    }
}
