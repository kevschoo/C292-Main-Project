using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MinionStat : MonoBehaviour
{
    [field: SerializeField] public int _BaseMaxMinionAmount { get; set; }
    [field: SerializeField] public int _BaseMinionCooldown { get; set; }

    [field: SerializeField] public int _MaxMinionAmount { get; set; }
    [field: SerializeField] public int _CurMinionAmount { get; set; }
    [field: SerializeField] public float _MinionCooldown { get; set; }

    [field: SerializeField] public bool _CanSpawnMinions { get; set; }

    [field: SerializeField] public List<GameObject> Minions { get; set; }
}
