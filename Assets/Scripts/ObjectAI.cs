using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectAI : MonoBehaviour
{
    [field: SerializeField] public abstract GameObject ObjectiveTarget { get; set; }

    [field: SerializeField] public abstract GameObject Target { get; set; }

    [field: SerializeField] public abstract bool AiIsEnabled { get; set; }

    public abstract void DamageTaken(GameObject Damager);
}
