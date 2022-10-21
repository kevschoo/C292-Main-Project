using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Lifetime { get; set; }
    [field: SerializeField] public int ParentDamage { get; set; }
    [field: SerializeField] public int ParentPenetration { get; set; }
    [field: SerializeField] public int ParentDamageIncrease { get; set; }
    [field: SerializeField] public string Team { get; set; }
    [field: SerializeField] public Player Owner { get; set; }
    [field: SerializeField] public GameObject Spawner { get; set; }
    [field: SerializeField] public bool AllowFriendlyFire { get; set; }
    [field: SerializeField] public int MaxTargetsToPenetrate { get; set; }

}
