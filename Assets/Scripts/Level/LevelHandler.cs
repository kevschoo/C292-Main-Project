using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelHandler : MonoBehaviour
{
    [SerializeField] LevelData CurrentLevelData;

    public string LevelName;
    public string LevelDifficulty;
    public string LevelDescription;


    public bool IsWaveLevel = true;

    public int CurrentWaveNumber = 0;
    public int LastWaveNumber;
    public bool isWaveActive = false;


    public float SurvivalTimeLength;

    public bool HasLevelReversed = false;


    [field: SerializeField] public BaseEntityData EnemyBaseWavesData { get; private set; }
    public static LevelHandler Level { get; private set; }


    void Awake()
    {
        if (Level != null && Level != this)
        { Destroy(this); }
        else
        { Level = this; }

        if(CurrentLevelData!= null)
        {
            LoadLevelData(CurrentLevelData);
        }
    }

    void LoadLevelData(LevelData LRTData)
    {
        this.LevelName = LRTData.LevelName;
        this.LevelDifficulty = LRTData.LevelDifficulty;
        this.LevelDescription = LRTData.LevelDescription;
        this.CurrentWaveNumber = LRTData.CurrentWaveNumber;
        this.LastWaveNumber = LRTData.LastWaveNumber;
        this.SurvivalTimeLength = LRTData.SurvivalTimeLength;
        this.isWaveActive = LRTData.isWaveActive;
        this.EnemyBaseWavesData = LRTData.EnemyBaseWavesData;
    }
    public void StartNextWave()
    {
        if(CurrentWaveNumber+1 > LastWaveNumber)
        {
            WaveEvent.InvokeReversal(false);
        }
        else
        {
            CurrentWaveNumber++;
            WaveEvent.InvokeWaveStart(CurrentWaveNumber);
        }
    }
    public void StartWave()
    {
        if(IsWaveLevel && !isWaveActive)
        {
            if(CurrentWaveNumber < LastWaveNumber)
            {
                Debug.Log("wanting to start wave");
                CurrentWaveNumber++;
                isWaveActive = true;
                WaveEvent.InvokeWaveStart(CurrentWaveNumber);
            }
            else if(CurrentWaveNumber >= LastWaveNumber)
            {
                if(HasLevelReversed == false)
                {
                    HasLevelReversed = true;
                    WaveEvent.InvokeReversal(false);
                }

            }

        }

    }

}
