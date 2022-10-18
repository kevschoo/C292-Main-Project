using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unique Part RTD", menuName = "ScriptableObjects/Unique Part RTD")]
public class UniquePartRTD : ScriptableObject
{
    [field: SerializeField] public List<GameObject> SpawnableObjects { get; set; }

    [field: SerializeField] public Sprite PartIcon { get; set; }
}
