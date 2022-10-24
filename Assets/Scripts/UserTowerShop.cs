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
    [SerializeField] TextMeshProUGUI TowerInfoText;
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

    public void CallSpawnMinion()
    {
        
        if (SelectedObject.GetComponent<Tower>() != null)
        {
            Tower tower = SelectedObject.GetComponent<Tower>();
            tower.Spawn();
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
            SetInfoText();
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
        if (SelectedObject.GetComponent<SpaceObject>())
        {
            SpaceObject spaceObj = SelectedObject.GetComponent<SpaceObject>();
            Debug.Log(spaceObj.name);
            this.TowerInfoText.text = "";
            if (spaceObj.Owner != null)
            {
                this.TowerInfoText.text += "Owner: " + spaceObj.Owner;
            }
            { this.TowerInfoText.text += "No Owner<br>"; }
            if (spaceObj.Team != "")
            {
                this.TowerInfoText.text += " | Team:" + spaceObj.Team + "<br>";
            }
            else
            { this.TowerInfoText.text += "No Team<br>"; }
            if (spaceObj.costStats != null)
            {
                this.TowerInfoText.text += "Cost: " + spaceObj.costStats._Cost + " | Upkeep: " + spaceObj.costStats._Upkeep + " Per " + spaceObj.costStats._UpKeepCooldown + " Seconds" + "<br>";
            }
            if (spaceObj.defensiveStats != null)
            {
                this.TowerInfoText.text += "Hp: " + spaceObj.defensiveStats._CurrentHealth + "/" + spaceObj.defensiveStats._MaxHealth + " | Regen: " + spaceObj.defensiveStats._HealthRegen + " Per " + (spaceObj.HealthRegenMod * 1) + " Seconds" + "<br>";
                this.TowerInfoText.text += "Def: " + spaceObj.defensiveStats._Defense + " | DmgDwn: " + spaceObj.defensiveStats._DamageReduction + "<br>";
            }
            if (spaceObj.offensiveStats != null)
            {
                this.TowerInfoText.text += "Dmg: " + spaceObj.offensiveStats._Damage + " | Range:" + spaceObj.offensiveStats._AttackRange + " | ASpd:" + spaceObj.offensiveStats._AttackSpeed + "<br>";
                this.TowerInfoText.text += "Pen: " + spaceObj.offensiveStats._Penetration + " | DmgUp: " + spaceObj.offensiveStats._DamageIncrease + "<br>";
            }
            if (spaceObj.speedStats != null)
            {
                this.TowerInfoText.text += "Spd: " + spaceObj.speedStats._Speed + " | Travel Range:" + spaceObj.speedStats._TravelRange + "<br>";
            }
            if (spaceObj.upgradeSlots != null)
            {
                //this.TowerInfoText.text += spaceObj.upgradeSlots.CurShipParts +"/" +spaceObj.upgradeSlots.MaxShipParts;
                this.TowerInfoText.text += spaceObj.upgradeSlots.CurShipParts + "/" + spaceObj.upgradeSlots.MaxShipParts +" Upgrades: ";
                foreach (ShipPart part in spaceObj.upgradeSlots.ChosenParts)
                {
                    this.TowerInfoText.text += part.name + ",";
                }
                this.TowerInfoText.text += "<br>";
                foreach (UniquePart part in spaceObj.upgradeSlots.UniqueParts)
                {
                    this.TowerInfoText.text += part.PartName + ",";
                }
                this.TowerInfoText.text += "<br>";
            }
            if (spaceObj.minionStats != null)
            {
                this.TowerInfoText.text += "Units: " + spaceObj.minionStats._CurMinionAmount + "/" + spaceObj.minionStats._MaxMinionAmount + "| Cooldown:" + spaceObj.minionStats._MinionCooldown + "<br>";
                if (SelectedObject.GetComponent<Tower>())
                {
                    Tower tower = SelectedObject.GetComponent<Tower>();
                    if (tower.MinionTemplate != null)
                    {
                        this.TowerInfoText.text += "Unit Type: " + tower.MinionTemplate.name + "<br>";
                    }
                    else
                    { this.TowerInfoText.text += "No Minion Type<br>"; }
                    if (tower.MinionsSpawned.Count != 0)
                    {
                        this.TowerInfoText.text += "Units: ";
                        foreach (GameObject minion in tower.MinionsSpawned)
                        {
                            this.TowerInfoText.text += minion.name + ", ";
                        }
                        this.TowerInfoText.text += "<br>";
                    }
                    else
                    { this.TowerInfoText.text += "No Active Minions<br>"; }
                }
            }
            if (spaceObj.statusEffectManager != null)
            {
                this.TowerInfoText.text += "Immunity: ";
                if (spaceObj.statusEffectManager.ImmuneToEffects.Count == 0)
                {
                    this.TowerInfoText.text += "None";
                }
                foreach (string effectName in spaceObj.statusEffectManager.ImmuneToEffects)
                {

                    this.TowerInfoText.text += effectName + ",";
                }
                this.TowerInfoText.text += "<br>";
                this.TowerInfoText.text += "Effects: ";
                if (spaceObj.statusEffectManager.CurrentEffects.Count == 0)
                {
                    this.TowerInfoText.text += "None";
                }
                foreach (string effectName in spaceObj.statusEffectManager.CurrentEffects)
                {
                    this.TowerInfoText.text += effectName + ",";
                }
                this.TowerInfoText.text += "<br>";
            }
        }
        else
        {
            this.TowerInfoText.text = "";
            if (SelectedObject.GetComponent<Tower>())
            {
                Tower tower = SelectedObject.GetComponent<Tower>();
                if (tower.Owner != null)
                {
                    this.TowerInfoText.text += "Owner: " + tower.Owner;
                }
                else
                { this.TowerInfoText.text += "No Owner<br>"; }
                if (tower.Team != "")
                {
                    this.TowerInfoText.text += " | Team:" + tower.Team + "<br>";
                }
                else
                { this.TowerInfoText.text += "No Team<br>"; }
                if (tower.MinionTemplate != null)
                {
                    this.TowerInfoText.text += "Unit Type: " + tower.MinionTemplate.name + "<br>";
                }
                else
                { this.TowerInfoText.text += "No Minion Type<br>"; }
                if (tower.MinionsSpawned.Count != 0)
                {
                    this.TowerInfoText.text += "Units: ";
                    foreach (GameObject minion in tower.MinionsSpawned)
                    {
                        this.TowerInfoText.text += minion.name + ", ";
                    }
                    this.TowerInfoText.text += "<br>";
                }
                else
                { this.TowerInfoText.text += "No Active Minions<br>"; }

            }
        }
    }
}
