using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelectionEvent : EventArgs
{
    public GameObject SelectedObj;

    public ObjectSelectionEvent(GameObject SelectedObject)
    {
        SelectedObj = SelectedObject;
    }

    //invoke(gameobject)
    //if x type > call event
    


}
