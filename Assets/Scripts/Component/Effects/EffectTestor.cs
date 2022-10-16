using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTestor : MonoBehaviour
{
    [SerializeField] DamageEffect CurrentEffectType = new DamageEffect();
    public string Name = "DamageUp";
    public float Duration = 5;
    public int Strength = 5;
    public bool LoopEffect = false;


    private void Start()
    {
       

        StartCoroutine(waittimer(5f));

    }

    public IEnumerator waittimer(float timer)
    {
   
        Debug.Log(Time.deltaTime);
        yield return new WaitForSeconds(timer);
        CurrentEffectType = this.gameObject.AddComponent<DamageEffect>();
        CurrentEffectType.EffectName = Name;
        CurrentEffectType.Duration = Duration;
        CurrentEffectType.Strength = Strength;
        CurrentEffectType.LoopEffect = false;
        this.gameObject.GetComponent<StatusEffectManager>().AddEffect(CurrentEffectType);

        Debug.Log(Time.deltaTime);
        yield return new WaitForSeconds(timer + 5);
        CurrentEffectType = this.gameObject.AddComponent<DamageEffect>();
        CurrentEffectType.EffectName = Name;
        CurrentEffectType.Duration = Duration;
        CurrentEffectType.Strength = Strength + 1;
        CurrentEffectType.LoopEffect = true;
        this.gameObject.GetComponent<StatusEffectManager>().AddEffect(CurrentEffectType);
    }
}
