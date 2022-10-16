using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : MonoBehaviour
{
    [field: SerializeField] public abstract string EffectName { get; set; }
    [field: SerializeField] public abstract float Duration { get; set; }
    [field: SerializeField] public abstract int Strength { get; set; }
    [field: SerializeField] public abstract bool LoopEffect { get; set; }
    public abstract void Apply(int Strength , float Duration, bool LoopEffect);
    public abstract void UnApply();
    public abstract void InstantRemove();
    public abstract void ReApply(int NewStrength, float NewDuration, bool NewLoopEffect);
}
