using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EntityEventArgs : EventArgs
{
    public GameObject SelectedEntity;
    public Player player;
}
public static class EntitySelectEvent
{
    public static event EventHandler<EntityEventArgs> SelectionChanged;
    public static event EventHandler SelectionCleared;

    public static void InvokeSelectionChanged(GameObject SelectedObject, Player SendingPlayer)
    {
        SelectionChanged(null, new EntityEventArgs { SelectedEntity = SelectedObject, player = SendingPlayer });

    }

    public static void InvokeSelectionChanged(GameObject SelectedObject)
    {
        SelectionChanged(null, new EntityEventArgs { SelectedEntity = SelectedObject });

    }

    public static void InvokeSelectionCleared()
    {
        SelectionCleared(null, EventArgs.Empty);

    }
}
