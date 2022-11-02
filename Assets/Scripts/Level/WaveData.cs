using UnityEngine;
[System.Serializable]
public struct WaveData 
{
    [field: SerializeField] public int WaveNumber { get; set; }
    [field: SerializeField] public int NumberToSpawn { get; set; }
    [field: SerializeField] public float SpawnDelay { get; set; }
    [field: SerializeField] public GameObject Entity { get; set; }
}
