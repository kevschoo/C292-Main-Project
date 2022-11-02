using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeMinions : MonoBehaviour
{
    [field: SerializeField] public int atr_BaseMaxMinionAmount { get; set; }
    [field: SerializeField] public float  atr_BaseMinionCooldown { get; set; }


    [field: SerializeField] public int atr_CurMinionAmount { get; set; }
    [field: SerializeField] public int atr_MaxMinionAmount { get; set; }
    [field: SerializeField] public float atr_MinionCooldown { get; set; }


    [field: SerializeField] public bool atr_CanSpawnMinions { get; set; }
    [field: SerializeField] public bool atr_KillMinionsOnDeath { get; set; }

    [field: SerializeField] public GameObject MinionType { get; set; }
    [field: SerializeField] public List<GameObject> Minions { get; set; }
}
