using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSelectionEvent : MonoBehaviour
{
    public static event Action OnObjectSelected;

    void Start()
    {
        //Add a listener to the new Event. Calls MyAction method when invoked
        //ObjectSelected.AddListener(MyAction);
    }

    private void OnMouseDown()
    {
        Debug.Log("Event Clicked");
        OnObjectSelected?.Invoke();
    }

    void Update()
    {
        
    }

    void MyAction()
    {
        //Output message to the console
        Debug.Log("Do Stuff");
    }
}
