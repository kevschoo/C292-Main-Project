using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BaseEntityData", menuName = "ScriptableObjects/BaseEntityData")]
public class BaseEntityData : ScriptableObject
{

    [field: SerializeField] public List<WaveData> WaveList { get; set; }

}
