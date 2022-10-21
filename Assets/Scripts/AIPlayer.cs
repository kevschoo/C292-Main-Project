using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    [field: SerializeField] public override PermUpgrades PlayersPermUpgrade { get ; set ; }
    [field: SerializeField] public override List<SpaceObject> PlayerOwnedObjects { get ; set; }
    [field: SerializeField] public override GameObject SelectedObject { get; set; }
    [field: SerializeField] public override int index { get; set; }

    private void Start()
    {
        if(PlayerOwnedObjects == null)
        {
            PlayerOwnedObjects = new List<SpaceObject>();
        }
    }
}
