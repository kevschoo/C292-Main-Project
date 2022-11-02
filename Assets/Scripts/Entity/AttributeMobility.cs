using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeMobility : MonoBehaviour
{
    [field: SerializeField] public float atr_BaseSpeed { get; set; }
    [field: SerializeField] public float atr_BaseTravelRange { get; set; }

    [field: SerializeField] public float atr_Speed { get; set; }
    [field: SerializeField] public float atr_TravelRange { get; set; }

    [field: SerializeField] public bool atr_CanMove { get; set; }
}
