using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeOffense : MonoBehaviour
{
    [field: SerializeField] public float atr_BaseDamage { get; set; }
    [field: SerializeField] public float atr_BasePenetration { get; set; }
    [field: SerializeField] public float atr_BaseDamageIncrease { get; set; }
    [field: SerializeField] public float atr_BaseAttackRange { get; set; }
    [field: SerializeField] public float atr_BaseAttackSpeed { get; set; }

    [field: SerializeField] public float atr_Damage { get; set; }
    [field: SerializeField] public float atr_Penetration { get; set; }
    [field: SerializeField] public float atr_DamageIncrease { get; set; }
    [field: SerializeField] public float atr_AttackRange { get; set; }
    [field: SerializeField] public float atr_AttackSpeed { get; set; }

}
