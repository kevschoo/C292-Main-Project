using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;

public class PauseTimeEvent : MonoBehaviour
{
    UnityEvent GamePaused = new UnityEvent();

    void Start()
    {
        //Add a listener to the new Event. Calls MyAction method when invoked
        GamePaused.AddListener(MyAction);
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
