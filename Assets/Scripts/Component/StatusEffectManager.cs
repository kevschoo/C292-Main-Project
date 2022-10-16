using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]

public class StatusEffectManager : MonoBehaviour
{
    [field: SerializeField] public bool IsImmuneToEffects { get; set; }
    [SerializeField] List<string> CurrentEffects = new List<string>();
    [SerializeField] List<string> ImmuneToEffects = new List<string>();
    [SerializeField] List<StatusEffect> CurrentEffectType = new List<StatusEffect>();
    public bool HasStatus(StatusEffect EffectType)
    {
        if(CurrentEffects.Contains(EffectType.name))
        { return true; }
        return false; 
    }
    public void AddEffect(StatusEffect EffectType)
    {
        if (this.IsImmuneToEffects)
        {
            Debug.Log("Is Immune To New Effects");
            EffectType.InstantRemove();
            return;
        }
        if (this.ImmuneToEffects.Contains(EffectType.name))
        {
            Debug.Log("Is Immune To That Effects");
            EffectType.InstantRemove();
            return;
        }
        if(HasStatus(EffectType) == false)
        {
            CurrentEffects.Add(EffectType.name);
            CurrentEffectType.Add(EffectType);
            CurrentEffectType[CurrentEffectType.IndexOf(EffectType)].Apply(EffectType.Strength, EffectType.Duration, EffectType.LoopEffect);
        }
        else
        {
            if(CurrentEffectType[CurrentEffectType.IndexOf(EffectType)].Strength <= EffectType.Strength)
            {
                CurrentEffectType[CurrentEffectType.IndexOf(EffectType)].ReApply(EffectType.Strength, EffectType.Duration, EffectType.LoopEffect);
            }
        }
    }
    public void RemoveEffect(StatusEffect EffectType)
    {
        this.CurrentEffects.Remove(EffectType.name);
        this.CurrentEffectType.Remove(EffectType);

    }

    public void ClearAllEffects()
    {
        if(this.CurrentEffects.Count == 0)
        { return; }

        Debug.Log("All Effects Cleared");
        for (int i = 0; i < CurrentEffectType.Count; i++)
        {
            CurrentEffectType[i].UnApply();
        }
    }
}
