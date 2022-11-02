using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PKEntity List", menuName = "ScriptableObjects/PKEntity List")]
public class PlayerKnownEntities : ScriptableObject
{
    [field: SerializeField] public List<GameObject> Entities { get; set; }
}
