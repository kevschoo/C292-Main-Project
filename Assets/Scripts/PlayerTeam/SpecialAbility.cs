using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAbility : MonoBehaviour
{

    [SerializeField] public abstract string AbilityName { get; set; }
    [SerializeField] public abstract string Category { get; set; }
    [SerializeField] public abstract bool isActive { get; set; }
    public abstract void Activate(Player AbilityUser, Vector3 Location);
    public abstract void DeActivate();
}
