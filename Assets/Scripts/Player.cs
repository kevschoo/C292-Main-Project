using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[DisallowMultipleComponent]
public abstract class Player : MonoBehaviour
{
    [SerializeField] public abstract PermUpgrades PlayersPermUpgrade { get; set; }

    [SerializeField] public abstract List<SpaceObject> PlayerOwnedObjects { get; set; }

    [SerializeField] public abstract List<Tower> PlayerAvaliableTowers { get; set; }

    [SerializeField] public abstract List<SpaceObject> PlayerAvaliableShips { get; set; }

    [SerializeField] public abstract GameObject SelectedObject { get; set; }

    [SerializeField] public abstract int Index { get; set; }

    [SerializeField] public abstract int Money { get; set; }

}
