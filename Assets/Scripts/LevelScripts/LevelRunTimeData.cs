using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level Data", menuName = "ScriptableObjects/Level Data")]
public class LevelRunTimeData : ScriptableObject
{
    [SerializeField] string LevelName;
    [SerializeField] string LevelDifficulty;
    [SerializeField] string LevelDescription;

    [SerializeField] int CurrentWaveNumber;
    [SerializeField] int LastWaveNumber;
    [SerializeField] float SurvivalTimeLength;
    [SerializeField] bool isWaveActive = false;
}
