using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class UserShipShop : MonoBehaviour
{

    //listens for invoke(gameobject shipobject)
    //enables ui for ship shop or viewing
    [SerializeField] TextMeshProUGUI ShipText;
    [SerializeField] TextMeshProUGUI ShipInfoText;
    [SerializeField]GameObject SelectedObject;

    // Start is called before the first frame update
    void Start()
    {
        ObjectSelectEvent.SelectionCleared += OnClearedSelect;
        ObjectSelectEvent.SelectionChanged += ShowSelectedShipShop;
    }

    private void OnDestroy()
    {
        ObjectSelectEvent.SelectionCleared -= OnClearedSelect;
        ObjectSelectEvent.SelectionChanged -= ShowSelectedShipShop;
    }
    // Update is called once per frame
    void Update()
    {
        if (SelectedObject == null)
        {
            ShipText.text = "Select A Space Ship";
        }
    }

    void ShowSelectedShipShop(object sender, ObjectSelectEventArgs args)
    {
        
        if(args.SelectedObj == null)
        {
            Debug.Log("Event args gave null somehow ");
            return;
        }
        SelectedObject = args.SelectedObj;
        if(SelectedObject != null)
        {
            if (SelectedObject.gameObject.GetComponent<Tower>() == null && SelectedObject.gameObject.GetComponent<SpaceObject>())
            {
                this.gameObject.SetActive(true);
                this.ShipText.text = SelectedObject.name;
                SetInfoText();
            }
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

    void SetInfoText()
    {
        if(SelectedObject.GetComponent<SpaceObject>())
        {
            SpaceObject spaceObj = SelectedObject.GetComponent<SpaceObject>();
            Debug.Log(spaceObj.name);
            this.ShipInfoText.text = "";
            if (spaceObj.Owner != null)
            {
                this.ShipInfoText.text += "Owner: " + spaceObj.Owner ;
            }
            if (spaceObj.Team != "")
            {
                this.ShipInfoText.text += " | Team:" + spaceObj.Team + "<br>";
            }
            else
            {this.ShipInfoText.text += "<br>";}
            if (spaceObj.costStats != null)
            {
                this.ShipInfoText.text += "Cost: " + spaceObj.costStats._Cost + "| Upkeep: " + spaceObj.costStats._Upkeep+ " Per " + spaceObj.costStats._UpKeepCooldown + " Seconds" + "<br>";
            }
            if (spaceObj.defensiveStats != null)
            {
                this.ShipInfoText.text += "Hp: " + spaceObj.defensiveStats._CurrentHealth + "/" + spaceObj.defensiveStats._MaxHealth  +" | Regen: " + spaceObj.defensiveStats._HealthRegen + " Per " + (spaceObj.HealthRegenMod * 1) + " Seconds" +"<br>";
                this.ShipInfoText.text += "Def: " + spaceObj.defensiveStats._Defense + "| DmgDwn: " + spaceObj.defensiveStats._DamageReduction + "<br>";
            }
            if (spaceObj.offensiveStats != null)
            {
                this.ShipInfoText.text += "Dmg: " + spaceObj.offensiveStats._Damage + "| Range:" + spaceObj.offensiveStats._AttackRange + "| ASpd:" + spaceObj.offensiveStats._AttackSpeed + "<br>";
                this.ShipInfoText.text += "Pen: " + spaceObj.offensiveStats._Penetration + "| DmgUp: " + spaceObj.offensiveStats._DamageIncrease + "<br>";
            }
            if (spaceObj.speedStats != null)
            {
                this.ShipInfoText.text += "Spd: " + spaceObj.speedStats._Speed + " | Travel Range:" + spaceObj.speedStats._TravelRange +"<br>";
            }
            if (spaceObj.upgradeSlots != null)
            {
                Debug.Log("UpSlots: " + spaceObj.upgradeSlots.ChosenParts.Count);
                this.ShipInfoText.text += "Upgrades: ";
                foreach(ShipPart part in spaceObj.upgradeSlots.ChosenParts)
                {
                    
                    this.ShipInfoText.text += part.name + ",";
                }
                this.ShipInfoText.text += "<br>";
                Debug.Log("UParts: " + spaceObj.upgradeSlots.UniqueParts.Count);
                foreach (UniquePart part in spaceObj.upgradeSlots.UniqueParts)
                {
                 
                    this.ShipInfoText.text += part.PartName + ",";
                }
                this.ShipInfoText.text += "<br>";
            }
            if (spaceObj.statusEffectManager != null)
            {
                this.ShipInfoText.text += "Immunity: ";
                if(spaceObj.statusEffectManager.ImmuneToEffects.Count == 0)
                {
                    this.ShipInfoText.text += "None";
                }
                foreach (string effectName in spaceObj.statusEffectManager.ImmuneToEffects)
                {
                    
                    this.ShipInfoText.text += effectName + ",";
                }
                this.ShipInfoText.text += "<br>";
                this.ShipInfoText.text += "Effects: ";
                if (spaceObj.statusEffectManager.CurrentEffects.Count == 0)
                {
                    this.ShipInfoText.text += "None";
                }
                foreach (string effectName in spaceObj.statusEffectManager.CurrentEffects)
                {
                    this.ShipInfoText.text += effectName + ",";
                }
                this.ShipInfoText.text += "<br>";
            }
        }
        else
        {
            Debug.Log("Error handling this object");
        }
    }
}
