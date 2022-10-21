using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserTowerShop : MonoBehaviour
{


    //listens for invoke(gameobject shipobject)
    //enables ui for ship shop or viewing
    [SerializeField] TextMeshProUGUI TowerText;
    [SerializeField] GameObject SelectedObject;

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
        if(SelectedObject == null)
        {
            TowerText.text = "Select A Tower";
        }
    }

    void ShowSelectedTowerShop(object sender, ObjectSelectEventArgs args)
    {
        
        if (args.SelectedObj == null)
        {
            Debug.Log("Event args gave null somehow ");
            return;
        }
        SelectedObject = args.SelectedObj;
        if(SelectedObject.gameObject.GetComponent<Tower>() != null)
        {
            this.gameObject.SetActive(true);
            this.TowerText.text = SelectedObject.name;
        }
        //this debug will cause null pointer since other event insta clears name lmao
        //Debug.Log("UserShipShop: Event args gave this object: " + args.SelectedObj.name);
    }

    void OnClearedSelect(object sender, EventArgs args)
    {
        this.gameObject.SetActive(false);
        this.SelectedObject = null;
        Debug.Log("UserShipShop: Event caused object clear ");
    }
}
