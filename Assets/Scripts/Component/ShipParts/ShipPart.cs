using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Part", menuName = "ScriptableObjects/Ship Part")]
public class ShipPart : ScriptableObject
{
    [field: SerializeField] public int PartSize { get; set; }
    [field: SerializeField] public bool AllowMultiple { get; set; }
    //Defensive
    [field: SerializeField] public int HealthBonus { get; set; }
    [field: SerializeField] public int DefenseBonus { get; set; }
    [field: SerializeField] public int DamageReductionBonus { get; set; }
    [field: SerializeField] public int HealthRegenBonus { get; set; }


    //Offensive
    [field: SerializeField] public int DamageBonus { get; set; }
    [field: SerializeField] public int PenetrationBonus { get; set; }
    [field: SerializeField] public int DamageIncreaseBonus { get; set; }
    [field: SerializeField] public float AttackRangeBonus { get; set; }
    [field: SerializeField] public float AttackSpeedBonus { get; set; }

    //Utility
    [field: SerializeField] public int SpeedBonus { get; set; }
    [field: SerializeField] public int TravelRangeBonus { get; set; }
    [field: SerializeField] public float CostBonus { get; set; }
    [field: SerializeField] public int MinionAmountBonus { get; set; }
    [field: SerializeField] public float MinionCooldownBonus { get; set; }



    [field: SerializeField] public List<string> UniqueShipParts { get; set; }

}
