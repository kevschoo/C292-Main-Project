using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
* Singleton
* This class is for keeping NavMesh of the current level, level wave, level timer, spawning locations, and targets
* This class will contain the following methods and variables
* 
* 
*/
public class LevelMap : MonoBehaviour
{
    public string LevelName;
    public string LevelDifficulty;
    public string LevelDescription;

    public int CurrentWaveNumber;
    public int LastWaveNumber;
    public float SurvivalTimeLength;
    public bool isWaveActive = false;


    public static LevelMap Level { get; private set; }


    void Awake()
    {
        if(Level != null && Level != this)
        {Destroy(this);}
        else
        { Level = this;}
    }

    void LoadLevelData(LevelRunTimeData LRTData)
    {
        this.LevelName = LRTData.LevelName;
        this.LevelDifficulty = LRTData.LevelDifficulty;
        this.LevelDescription = LRTData.LevelDescription;
        this.CurrentWaveNumber = LRTData.CurrentWaveNumber;
        this.LastWaveNumber = LRTData.LastWaveNumber;
        this.SurvivalTimeLength = LRTData.SurvivalTimeLength;
        this.isWaveActive = LRTData.isWaveActive;
    }
    
}
