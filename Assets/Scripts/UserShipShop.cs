using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UserShipShop : MonoBehaviour
{

    //listens for invoke(gameobject shipobject)
    //enables ui for ship shop or viewing
    [SerializeField] TextMeshProUGUI ShipText;
    [SerializeField]GameObject SelectedObject;

    // Start is called before the first frame update
    void Start()
    {
        ObjectSelectEvent.SelectionCleared += OnClearedSelect;
        ObjectSelectEvent.SelectionChanged += ShowSelectedTowerShop;
    }

    private void OnDestroy()
    {
        ObjectSelectEvent.SelectionCleared -= OnClearedSelect;
        ObjectSelectEvent.SelectionChanged -= ShowSelectedTowerShop;
    }
    // Update is called once per frame
    void Update()
    {
        if (SelectedObject == null)
        {
            ShipText.text = "Select A Space Ship";
        }
    }

    void ShowSelectedTowerShop(object sender, ObjectSelectEventArgs args)
    {
        if(args == null)
        {
            Debug.Log("Event args gave null somehow ");
            return;
        }
        SelectedObject = args.SelectedObj;
        if (SelectedObject.gameObject.GetComponent<Tower>() == null)
        {
            this.ShipText.text = SelectedObject.name;
        }
        //this debug will cause null pointer since other event insta clears name lmao
        //Debug.Log("UserShipShop: Event args gave this object: " + args.SelectedObj.name);
    }

    void OnClearedSelect(object sender, ObjectSelectEventArgs args)
    {
        SelectedObject = null;
        Debug.Log("UserShipShop: Event caused object clear " + args.SelectedObj);
    }
}
