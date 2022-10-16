using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CostStat : MonoBehaviour
{
    [field: SerializeField] public float _BaseCost { get; set; }
    [field: SerializeField] public float _Cost { get; set; }
    [field: SerializeField] public float _BaseUpkeep { get; set; }
    [field: SerializeField] public float _Upkeep { get; set; }
    [field: SerializeField] public bool _HasUpkeepCosts { get; set; }
    [field: SerializeField] public float _UpKeepCooldown { get; set; }


}
