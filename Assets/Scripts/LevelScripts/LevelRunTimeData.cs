using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data")]
public class LevelRunTimeData : ScriptableObject
{
    [field: SerializeField] public string LevelName { get; set; }
    [field: SerializeField] public string LevelDifficulty { get; set; }
    [field: SerializeField] public string LevelDescription { get; set; }

    [field: SerializeField] public int CurrentWaveNumber { get; set; }
    [field: SerializeField] public int LastWaveNumber { get; set; }
    [field: SerializeField] public float SurvivalTimeLength { get; set; }
    [field: SerializeField] public bool isWaveActive { get; set; } = false;
}
