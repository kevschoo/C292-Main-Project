using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OffensiveStats : MonoBehaviour
{
    [field: SerializeField] public int _BaseDamage { get; set; }
    [field: SerializeField] public int _BasePenetration { get; set; }
    [field: SerializeField] public int _BaseDamageIncrease { get; set; }
    [field: SerializeField] public int _BaseAttackRange { get; set; }
    [field: SerializeField] public int _BaseAttackSpeed { get; set; }

    [field: SerializeField] public int _Damage { get; set; }
    [field: SerializeField] public int _Penetration { get; set; }
    [field: SerializeField] public int _DamageIncrease { get; set; }
    [field: SerializeField] public float _AttackRange { get; set; }
    [field: SerializeField] public float _AttackSpeed { get; set; }


}
