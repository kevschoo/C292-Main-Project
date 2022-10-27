using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UserTowerShop : MonoBehaviour
{


    //listens for invoke(gameobject shipobject)
    //enables ui for ship shop or viewing
    [SerializeField] TextMeshProUGUI TowerText;
    [SerializeField] TextMeshProUGUI TowerInfoText;
    [SerializeField] GameObject SelectedObject;

    [SerializeField] TMP_Dropdown DropDownList;
    [SerializeField] TextMeshProUGUI TowerCostInfo;

    List<string> PlayerTowers = new List<string>();

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

    public void CallSpawnMinion()
    {
        
        if (SelectedObject.GetComponent<Tower>() != null)
        {
            Tower tower = SelectedObject.GetComponent<Tower>();
            tower.Spawn();
        }
    }

    public void SetDropdownTowers(Player Player)
    {
        Debug.Log("Setting List");
        DropDownList.ClearOptions();
        PlayerTowers.Clear(); 
       
        for(int i = 0; i < Player.PlayerAvaliableShips.Count; i++)
        {
            Debug.Log("Loaded Ship:" + Player.PlayerAvaliableShips[i].gameObject.name);
            PlayerTowers.Add(Player.PlayerAvaliableShips[i].gameObject.name);
        }
        DropDownList.AddOptions(PlayerTowers);
        
    }


    GameObject GetPlayerShip(Player Player, int ShipIndex)
    {
        Debug.Log("Load Ships");
        if (Player.PlayerAvaliableShips.Count == 0)
        {return null;}
        if (ShipIndex > Player.PlayerAvaliableShips.Count)
        {return null;}
        if (ShipIndex < 0)
        {return null;}
        GameObject SelectedShip;
        SelectedShip = Player.PlayerAvaliableShips[ShipIndex].gameObject;
        return SelectedShip;
    }

    public void LoadTower()
    {
        Debug.Log("Tower Load");
        if (SelectedObject.GetComponent<Tower>() != null)
        {
            Tower tower = SelectedObject.GetComponent<Tower>();
            Player Buyer = tower.Owner;
            Debug.Log("TowerOwner: " + tower.Owner.name);
            SetDropdownTowers(Buyer);
        }
    }
    public void BuyTower()
    {
        Debug.Log("Tower Buy");
        if (SelectedObject.GetComponent<Tower>() != null)
        {
            Tower tower = SelectedObject.GetComponent<Tower>();
            Player Buyer = tower.Owner;
            GameObject Minion = GetPlayerShip(Buyer, DropDownList.value);
            Debug.Log("Tower New minion");
            if (Minion != null)
            {
                if (Minion.GetComponent<CostStat>())
                {
                    CostStat MCost = Minion.GetComponent<CostStat>();
                    if(Buyer.Money < MCost._Cost)
                    {
                        Debug.Log("Player too poor");
                        return;
                    }
                    Buyer.Money -= Mathf.RoundToInt(MCost._Cost);
                }
                
                Debug.Log(Minion.name);
                tower.MinionTemplate = Minion;
            }
        }
    }
    public void SellTower()
    {
        Debug.Log("Tower Sell");
        if (SelectedObject.GetComponent<Tower>() != null)
        {
            Tower tower = SelectedObject.GetComponent<Tower>();
            Player Buyer = tower.Owner;
            if (tower.MinionTemplate != null)
            {
                if (tower.MinionTemplate.GetComponent<CostStat>())
                {
                    CostStat MCost = tower.MinionTemplate.GetComponent<CostStat>();
                    Buyer.Money += Mathf.RoundToInt(MCost._Cost / 2);
                }
                foreach(GameObject minion in tower.MinionsSpawned)
                {
                    Destroy(minion);
                }
                tower.MinionTemplate = null;
            }
        }
    }
    void DropdownValueChanged()
    {
        Tower tower = SelectedObject.GetComponent<Tower>();
        Player Buyer = tower.Owner;
        GameObject Minion = GetPlayerShip(Buyer, DropDownList.value);
        Debug.Log("Updating Minion Cost");
        if (Minion != null)
        {
            if (Minion.GetComponent<CostStat>())
            {
                CostStat MCost = Minion.GetComponent<CostStat>();
                TowerCostInfo.text = "YourMoney: "+ Buyer.Money + ", Cost: " + MCost._Cost + ", Upkeep:" + MCost._BaseUpkeep;
            }
            else
            {
                TowerCostInfo.text = "YourMoney: " + Buyer.Money + ", Cost: Free";
            }
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
            LoadTower();
            DropdownValueChanged();
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
