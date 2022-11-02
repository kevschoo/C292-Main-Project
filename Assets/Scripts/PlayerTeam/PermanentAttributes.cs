using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PermUpgrades", menuName = "ScriptableObjects/PermUpgrades")]
public class PermanentAttributes : ScriptableObject
{
    //Defensive
    [field: SerializeField] public int atr_MaxHealthBonus { get; set; }
    [field: SerializeField] public int atr_DefenseBonus { get; set; }
    [field: SerializeField] public int atr_DamageReductionBonus { get; set; }
    [field: SerializeField] public int atr_RegenerationBonus { get; set; }
    //Offensive
    [field: SerializeField] public int atr_DamageBonus { get; set; }
    [field: SerializeField] public int atr_PenetrationBonus { get; set; }
    [field: SerializeField] public int atr_DamageIncreaseBonus { get; set; }
    [field: SerializeField] public float atr_AttackRangeBonus { get; set; }
    [field: SerializeField] public float atr_AttackSpeedBonus { get; set; }

    //Utility
    [field: SerializeField] public int atr_SpeedBonus { get; set; }
    [field: SerializeField] public int atr_TravelRangeBonus { get; set; }
    [field: SerializeField] public float atr_CostModifierBonus { get; set; }
    [field: SerializeField] public float atr_UpkeepModifierBonus { get; set; }
    [field: SerializeField] public int atr_MaxMinionAmountBonus { get; set; }
    [field: SerializeField] public float atr_MinionCooldownModifierBonus { get; set; }
}
