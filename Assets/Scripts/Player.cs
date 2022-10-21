using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[DisallowMultipleComponent]
public abstract class Player : MonoBehaviour
{
    [field: SerializeField] public abstract PermUpgrades PlayersPermUpgrade { get; set; }

    [field: SerializeField] public abstract List<SpaceObject> PlayerOwnedObjects { get; set; }

    [field: SerializeField] public abstract GameObject SelectedObject { get; set; }

    [field: SerializeField] public abstract int index { get; set; }

}
