using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Player : MonoBehaviour
{
    [SerializeField] public abstract string PlayerName { get; set; }
    [SerializeField] public abstract PermanentAttributes PlayerPermAttributes { get; set; }

    [SerializeField] public abstract BaseBonus baseBonus { get; set; }

    [SerializeField] public abstract List<EntityAI> PlayerEntities { get; set; }

    //[SerializeField] public abstract List<Tower> PlayerAvaliableTowers { get; set; }

    [SerializeField] public abstract PlayerKnownEntities PKEntities { get; set; }
    [SerializeField] public abstract PlayerKnownUprades PKUpgrades { get; set; }

    [SerializeField] public abstract SpecialAbility SelectedAbility { get; set; }
    [SerializeField] public abstract bool CanActivateSelectedAbility { get; set; }
    [SerializeField] public abstract GameObject SelectedObject { get; set; }
    [SerializeField] public abstract GameObject PlayerBase { get; set; }

    [SerializeField] public abstract int Index { get; set; }

    [SerializeField] public abstract float Money { get; set; }
}
