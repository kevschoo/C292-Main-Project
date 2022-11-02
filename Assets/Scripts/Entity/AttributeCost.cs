using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeCost : MonoBehaviour
{
    [field: SerializeField] public float atr_BaseCostModifier { get; set; }
    [field: SerializeField] public float atr_BaseUpkeepModifier { get; set; }
    [field: SerializeField] public float atr_CostModifier { get; set; }
    [field: SerializeField] public float atr_UpkeepModifier { get; set; }

    [field: SerializeField] public bool atr_HasUpkeepCosts { get; set; }
    [field: SerializeField] public float atr_UpKeepCooldown { get; set; }
}
