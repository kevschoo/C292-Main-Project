using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    [SerializeField]Button but;

    private void Start()
    {
        but = this.gameObject.GetComponent<Button>();
        Debug.Log("Button Object");
    }

    public void Debugger()
    {
        Debug.Log("Button clicked");
    }
}
