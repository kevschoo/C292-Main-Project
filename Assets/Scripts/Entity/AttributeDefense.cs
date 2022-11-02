using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDefense : MonoBehaviour
{
    [field: SerializeField] public float atr_BaseMaxHealth { get; set; }
    [field: SerializeField] public float atr_BaseDefense { get; set; }
    [field: SerializeField] public float atr_BaseDamageReduction { get; set; }
    [field: SerializeField] public float atr_BaseRegeneration { get; set; }

    [field: SerializeField] public float atr_CurrentHealth { get; set; }
    [field: SerializeField] public float atr_MaxHealth { get; set; }
    [field: SerializeField] public float atr_Defense { get; set; }
    [field: SerializeField] public float atr_DamageReduction { get; set; }
    [field: SerializeField] public float atr_Regeneration { get; set; }
}
