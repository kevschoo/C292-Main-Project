using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public abstract class EntityAI : MonoBehaviour
{

    [SerializeField] public abstract string AITypeName { get; set; }
    [SerializeField] public abstract GameObject ObjectiveTarget { get; set; }

    [SerializeField] public abstract GameObject Target { get; set; }

    [SerializeField] public abstract GameObject BaseLocation { get; set; }

    [SerializeField] public abstract bool AIEnabled { get; set; }

    [SerializeField] public abstract EntitySystemData OffensiveSystemData { get; set; }
    [SerializeField] public abstract EntitySystemData DefensiveSystemData { get; set; }
    [SerializeField] public abstract EntitySystemData AltSystemData { get; set; }
    public abstract void OnDamageTaken(GameObject Damager);
    public abstract void ReloadAI();
    public abstract void FindAllEntitySystems();

    public abstract void UpdateMinionUpgrades(EntityPart Part);

    public abstract void RemoveMinionUpgrades(EntityPart Part);
}
