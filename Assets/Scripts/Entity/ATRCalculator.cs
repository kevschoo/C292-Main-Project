using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public static class ATRCalculator 
{


    //OFF
    public static void Recalculate(AttributeOffense atr)
    {
        atr.atr_Damage = atr.atr_BaseDamage;
        atr.atr_Penetration = atr.atr_BasePenetration;
        atr.atr_AttackSpeed = atr.atr_BaseAttackSpeed;
        atr.atr_AttackRange = atr.atr_BaseAttackRange;
        atr.atr_DamageIncrease = atr.atr_BaseDamageIncrease;
    }

    public static void Recalculate(AttributeOffense atr, PermanentAttributes per)
    {
        atr.atr_Damage = atr.atr_BaseDamage + per.atr_DamageBonus ;
        atr.atr_Penetration = atr.atr_BasePenetration + per.atr_PenetrationBonus;
        atr.atr_AttackSpeed = atr.atr_BaseAttackSpeed + per.atr_AttackSpeedBonus ;
        atr.atr_AttackRange = atr.atr_BaseAttackRange + per.atr_AttackRangeBonus ;
        atr.atr_DamageIncrease = atr.atr_BaseDamageIncrease + per.atr_DamageIncreaseBonus;
    }

    public static void Recalculate(AttributeOffense atr, PermanentAttributes per, AttributeUpgradeSystem ups)
    {
        atr.atr_Damage = atr.atr_BaseDamage + per.atr_DamageBonus + ups.atr_DamageBonus;
        atr.atr_Penetration = atr.atr_BasePenetration + per.atr_PenetrationBonus + ups.atr_PenetrationBonus;
        atr.atr_AttackSpeed = atr.atr_BaseAttackSpeed + per.atr_AttackSpeedBonus + ups.atr_AttackSpeedBonus;
        atr.atr_AttackRange = atr.atr_BaseAttackRange + per.atr_AttackRangeBonus + ups.atr_AttackRangeBonus;
        atr.atr_DamageIncrease = atr.atr_BaseDamageIncrease + per.atr_DamageIncreaseBonus + ups.atr_DamageIncreaseBonus;
    }

    public static void Recalculate(AttributeOffense atr, AttributeUpgradeSystem ups)
    {
        atr.atr_Damage = atr.atr_BaseDamage + ups.atr_DamageBonus;
        atr.atr_Penetration = atr.atr_BasePenetration + ups.atr_PenetrationBonus;
        atr.atr_AttackSpeed = atr.atr_BaseAttackSpeed + ups.atr_AttackSpeedBonus;
        atr.atr_AttackRange = atr.atr_BaseAttackRange  + ups.atr_AttackRangeBonus;
        atr.atr_DamageIncrease = atr.atr_BaseDamageIncrease + ups.atr_DamageIncreaseBonus;
    }


    //DEF
    public static void Recalculate(AttributeDefense atr)
    {
        atr.atr_Defense = atr.atr_BaseDefense;
        atr.atr_MaxHealth = atr.atr_BaseMaxHealth;
        atr.atr_Regeneration = atr.atr_BaseRegeneration;
        atr.atr_DamageReduction = atr.atr_BaseDamageReduction;
    }

    public static void Recalculate(AttributeDefense atr, PermanentAttributes per)
    {
        atr.atr_Defense = atr.atr_BaseDefense + per.atr_DefenseBonus;
        atr.atr_MaxHealth = atr.atr_BaseMaxHealth + per.atr_MaxHealthBonus;
        atr.atr_Regeneration = atr.atr_BaseRegeneration + per.atr_RegenerationBonus;
        atr.atr_DamageReduction = atr.atr_BaseDamageReduction + per.atr_DamageReductionBonus;
    }

    public static void Recalculate(AttributeDefense atr, PermanentAttributes per, AttributeUpgradeSystem ups)
    {
        atr.atr_Defense = atr.atr_BaseDefense + per.atr_DefenseBonus + ups.atr_DefenseBonus;
        atr.atr_MaxHealth = atr.atr_BaseMaxHealth + per.atr_MaxHealthBonus + ups.atr_MaxHealthBonus;
        atr.atr_Regeneration = atr.atr_BaseRegeneration + per.atr_RegenerationBonus + ups.atr_RegenerationBonus;
        atr.atr_DamageReduction = atr.atr_BaseDamageReduction + per.atr_DamageReductionBonus + ups.atr_DamageReductionBonus;
    }

    public static void Recalculate(AttributeDefense atr, AttributeUpgradeSystem ups)
    {
        atr.atr_Defense = atr.atr_BaseDefense  + ups.atr_DefenseBonus;
        atr.atr_MaxHealth = atr.atr_BaseMaxHealth  + ups.atr_MaxHealthBonus;
        atr.atr_Regeneration = atr.atr_BaseRegeneration  + ups.atr_RegenerationBonus;
        atr.atr_DamageReduction = atr.atr_BaseDamageReduction + ups.atr_DamageReductionBonus;
    }

    //MIN
    public static void Recalculate(AttributeMinions atr)
    {
        atr.atr_MaxMinionAmount = atr.atr_BaseMaxMinionAmount;
        atr.atr_MinionCooldown = atr.atr_BaseMinionCooldown;
    }

    public static void Recalculate(AttributeMinions atr, PermanentAttributes per)
    {
        atr.atr_MaxMinionAmount = atr.atr_BaseMaxMinionAmount + per.atr_MaxMinionAmountBonus;
        atr.atr_MinionCooldown = atr.atr_BaseMinionCooldown * per.atr_MinionCooldownModifierBonus;
    }

    public static void Recalculate(AttributeMinions atr, PermanentAttributes per, AttributeUpgradeSystem ups)
    {
        atr.atr_MaxMinionAmount = atr.atr_BaseMaxMinionAmount + per.atr_MaxMinionAmountBonus + ups.atr_MaxMinionAmountBonus;
        atr.atr_MinionCooldown = atr.atr_BaseMinionCooldown * per.atr_MinionCooldownModifierBonus * ups.atr_MinionCooldownModifierBonus;
    }

    public static void Recalculate(AttributeMinions atr, AttributeUpgradeSystem ups)
    {
        atr.atr_MaxMinionAmount = atr.atr_BaseMaxMinionAmount + ups.atr_MaxMinionAmountBonus;
        atr.atr_MinionCooldown = atr.atr_BaseMinionCooldown  * ups.atr_MinionCooldownModifierBonus;
    }

    //MOB
    public static void Recalculate(AttributeMobility atr)
    {
        atr.atr_Speed = atr.atr_BaseSpeed;
        atr.atr_TravelRange = atr.atr_BaseTravelRange;
    }

    public static void Recalculate(AttributeMobility atr, PermanentAttributes per)
    {
        atr.atr_Speed = atr.atr_BaseSpeed + per.atr_SpeedBonus;
        atr.atr_TravelRange = atr.atr_BaseTravelRange + per.atr_TravelRangeBonus;
    }

    public static void Recalculate(AttributeMobility atr, PermanentAttributes per, AttributeUpgradeSystem ups)
    {
        atr.atr_Speed = atr.atr_BaseSpeed + per.atr_SpeedBonus + ups.atr_SpeedBonus;
        atr.atr_TravelRange = atr.atr_BaseTravelRange + per.atr_TravelRangeBonus + ups.atr_TravelRangeBonus;
    }

    public static void Recalculate(AttributeMobility atr, AttributeUpgradeSystem ups)
    {
        atr.atr_Speed = atr.atr_BaseSpeed + ups.atr_SpeedBonus;
        atr.atr_TravelRange = atr.atr_BaseTravelRange + ups.atr_TravelRangeBonus;
    }

    //COST
    public static void Recalculate(AttributeCost atr)
    {
        atr.atr_CostModifier = atr.atr_BaseCostModifier;
        atr.atr_UpkeepModifier = atr.atr_BaseUpkeepModifier;
    }

    public static void Recalculate(AttributeCost atr, PermanentAttributes per)
    {
        atr.atr_CostModifier = atr.atr_BaseCostModifier + per.atr_CostModifierBonus;
        atr.atr_UpkeepModifier = atr.atr_BaseUpkeepModifier + per.atr_UpkeepModifierBonus;
    }

    public static void Recalculate(AttributeCost atr, PermanentAttributes per, AttributeUpgradeSystem ups)
    {
        atr.atr_CostModifier = atr.atr_BaseCostModifier + per.atr_CostModifierBonus + ups.atr_CostModifierBonus;
        atr.atr_UpkeepModifier = atr.atr_BaseUpkeepModifier + per.atr_UpkeepModifierBonus + ups.atr_UpkeepModifierBonus;
    }

    public static void Recalculate(AttributeCost atr, AttributeUpgradeSystem ups)
    {
        atr.atr_CostModifier = atr.atr_BaseCostModifier+ ups.atr_CostModifierBonus;
        atr.atr_UpkeepModifier = atr.atr_BaseUpkeepModifier + ups.atr_UpkeepModifierBonus;
    }




}
