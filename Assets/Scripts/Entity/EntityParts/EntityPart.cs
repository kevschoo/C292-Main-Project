using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Part", menuName = "ScriptableObjects/Entity Part")]
public class EntityPart : ScriptableObject
{
    [field: SerializeField] public Sprite PartIcon { get; set; }
    [field: SerializeField] public string PartName { get; set; }
    [field: SerializeField] public int PartSize { get; set; }
    [field: SerializeField] public bool AllowMultiple { get; set; }

    [field: SerializeField] public float PartCost { get; set; }
    //Defensive
    [field: SerializeField] public float atr_MaxHealthBonus { get; set; }
    [field: SerializeField] public float atr_DefenseBonus { get; set; }
    [field: SerializeField] public float atr_DamageReductionBonus { get; set; }
    [field: SerializeField] public float atr_RegenerationBonus { get; set; }
    //Offensive
    [field: SerializeField] public float atr_DamageBonus { get; set; }
    [field: SerializeField] public float atr_PenetrationBonus { get; set; }
    [field: SerializeField] public float atr_DamageIncreaseBonus { get; set; }
    [field: SerializeField] public float atr_AttackRangeBonus { get; set; }
    [field: SerializeField] public float atr_AttackSpeedBonus { get; set; }

    //Utility
    [field: SerializeField] public float atr_SpeedBonus { get; set; }
    [field: SerializeField] public float atr_TravelRangeBonus { get; set; }
    [field: SerializeField] public float atr_CostModifierBonus { get; set; }
    [field: SerializeField] public float atr_UpkeepModifierBonus { get; set; }
    [field: SerializeField] public int atr_MaxMinionAmountBonus { get; set; }
    [field: SerializeField] public float atr_MinionCooldownModifierBonus { get; set; }


    //EntitySystems as strings, added to entity via converting string to potential class if found and addign as component
    [field: SerializeField] public List<string> EntitySystems { get; set; }
}
