using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Base Bonus", menuName = "ScriptableObjects/Base Bonus")]
public class BaseBonus : ScriptableObject
{

    //Utility
    [field: SerializeField] public float atr_IncomeGeneration { get; set; }
    [field: SerializeField] public float atr_IncomePerWave { get; set; }
    [field: SerializeField] public float atr_HealthRegenerationModifier { get; set; }
}
