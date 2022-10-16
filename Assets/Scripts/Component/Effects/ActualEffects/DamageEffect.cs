using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageEffect : StatusEffect
{

    [field: SerializeField] public override string EffectName { get; set; }
    [field: SerializeField] public override float Duration { get; set; }
    [field: SerializeField] public override int Strength { get; set; }
    [field: SerializeField] public override bool LoopEffect { get; set; }


    public override void Apply(int Strength, float Duration, bool LoopEffect)
    {
        this.Strength = Strength;
        this.Duration = Duration;
        this.LoopEffect = LoopEffect;
        Debug.Log("Effect Applied");
        if (this.gameObject.GetComponent<OffensiveStats>() != null)
        {
            OffensiveStats stats = this.gameObject.GetComponent<OffensiveStats>();
            stats._DamageIncrease += 10 * Strength;
        }
        if (LoopEffect == false)
        {
            StartCoroutine(UnapplicationCoroutine());
            
        }
    }

    public override void UnApply()
    {
        
        if (this.gameObject.GetComponent<OffensiveStats>() != null)
        {
            OffensiveStats stats = this.gameObject.GetComponent<OffensiveStats>();
            stats._DamageIncrease -= 10 * Strength;
        }
        this.gameObject.GetComponent<StatusEffectManager>().RemoveEffect(this);


        Destroy(this);
    }

    private IEnumerator UnapplicationCoroutine()
    {
        yield return new WaitForSeconds(Duration);
        Debug.Log("Effect UnApplied");
        UnApply();
    }

    public override void ReApply(int NewStrength, float NewDuration, bool NewLoopEffect)
    {
        Debug.Log("Effect ReApplied");
        if (this.gameObject.GetComponent<OffensiveStats>() != null)
        {
            OffensiveStats stats = this.gameObject.GetComponent<OffensiveStats>();
            stats._DamageIncrease -= 10 * Strength;
            StopCoroutine(UnapplicationCoroutine());
            Apply(NewStrength, NewDuration, NewLoopEffect);
        }
    }

    public override void InstantRemove()
    {
        Debug.Log("Effect InstantRemoved");
        Destroy(this);
    }
}
