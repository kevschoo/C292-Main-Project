using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Bullet : MonoBehaviour
{
    [field: SerializeField] public Player Owner { get; set; }
    [field: SerializeField] public GameObject Parent { get; set; }
    [field: SerializeField] public Team team { get; set; }

    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float Lifetime { get; set; }
    [field: SerializeField] public float ParentDamage { get; set; }
    [field: SerializeField] public float ParentPenetration { get; set; }
    [field: SerializeField] public float ParentDamageIncrease { get; set; }

    [field: SerializeField] public bool AllowFriendlyFire { get; set; }
    [field: SerializeField] public int MaxTargetsToPenetrate { get; set; }

}
