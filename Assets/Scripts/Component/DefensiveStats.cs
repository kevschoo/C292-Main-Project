using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DefensiveStats : MonoBehaviour
{
    [field: SerializeField] public int _BaseMaxHealth { get; set; }
    [field: SerializeField] public int _BaseDefense { get; set; }
    [field: SerializeField] public int _BaseDamageReduction { get; set; }
    [field: SerializeField] public int _BaseHealthRegen { get; set; }

    [field: SerializeField] public int _MaxHealth { get; set; }
    [field: SerializeField] public int _CurrentHealth { get; set; }
    [field: SerializeField] public int _Defense { get; set; }
    [field: SerializeField] public int _DamageReduction { get; set; }
    [field: SerializeField] public int _HealthRegen { get; set; }

}
