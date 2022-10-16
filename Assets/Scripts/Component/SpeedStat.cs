using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SpeedStat : MonoBehaviour
{
    [field: SerializeField] public int _BaseSpeed { get; set; }
    [field: SerializeField] public int _BaseTravelRange { get; set; }

    [field: SerializeField] public int _Speed { get; set; }
    [field: SerializeField] public int _TravelRange { get; set; }

    [field: SerializeField] public bool _CanMove { get; set; }

}
