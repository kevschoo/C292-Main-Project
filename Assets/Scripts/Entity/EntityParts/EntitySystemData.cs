using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EntitySystemData", menuName = "ScriptableObjects/EntitySystemData")]
public class EntitySystemData : ScriptableObject
{
    [field: SerializeField] public List<GameObject> SpawnableObjects { get; set; }
}
