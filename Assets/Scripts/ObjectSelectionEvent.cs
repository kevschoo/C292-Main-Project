using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelectEventArgs : EventArgs
{
    public GameObject SelectedObj;

    //UnityEvent<GameObject> SelectedEvent;

    //invoke(gameobject)
    //if x type > call event

}

public static class ObjectSelectEvent 
{
    public static event EventHandler<ObjectSelectEventArgs> SelectionChanged;
    public static event EventHandler<ObjectSelectEventArgs> SelectionCleared;

    public static void InvokeSelectionChanged(GameObject SelectedObject)
    {
        SelectionChanged(null, new ObjectSelectEventArgs { SelectedObj = SelectedObject });
        
    }
    public static void InvokeSelectionCleared(GameObject SelectedObject)
    {
        SelectionChanged(null, new ObjectSelectEventArgs { SelectedObj = null });

    }
}
