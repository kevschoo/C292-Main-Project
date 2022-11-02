using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PKUpgrade List", menuName = "ScriptableObjects/PKUpgrade List")]
public class PlayerKnownUprades : ScriptableObject
{
    [field: SerializeField] public List<EntityPart> EntityParts { get; set; }

}
