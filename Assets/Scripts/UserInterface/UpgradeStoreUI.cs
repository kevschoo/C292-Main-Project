using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class UpgradeStoreUI : MonoBehaviour
{
    [field: SerializeField] public GameObject SelectedEntity { get; set; }
    [field: SerializeField] public Entity EntityInfo { get; set; }
    [field: SerializeField] public EntityPart SelectedEntityPart { get; set; }

    [SerializeField] TextMeshProUGUI StoreItemDescription;

    [SerializeField] DynamicScrollView ShipStoreScroller;

    [SerializeField] Player Shopper;

    void Start()
    {
        EntitySelectEvent.SelectionCleared += ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged += ShowSelectedEntity;
        UpgradeSelectEvent.UpgradeChanged += ChangeUpgrade;
        UpgradeSelectEvent.UpgradeCleared += ClearUpgrade;
        this.gameObject.SetActive(false);
        WaveEvent.GameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        EntitySelectEvent.SelectionCleared -= ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged -= ShowSelectedEntity;
        UpgradeSelectEvent.UpgradeChanged -= ChangeUpgrade;
        UpgradeSelectEvent.UpgradeCleared -= ClearUpgrade;
        WaveEvent.GameEnd -= OnGameEnd;
    }

    void OnGameEnd(object sender, WaveEventArgs args)
    {
        this.gameObject.SetActive(false);
        this.enabled = false;
    }

    void ClearSelectedEntity(object sender, EventArgs args)
    {
        this.gameObject.SetActive(false);
        SelectedEntity = null;
        EntityInfo = null;
        this.StoreItemDescription.text = "";
    }
    void ShowSelectedEntity(object sender, EntityEventArgs args)
    {
        this.gameObject.SetActive(false);
        SelectedEntity = args.SelectedEntity;
        Shopper = args.player;
        if(SelectedEntity == null)
        {
            return;
        }
        if (SelectedEntity.TryGetComponent<EntityShip>(out EntityShip EInfo))
        {
            EntityInfo = EInfo;
            if (EInfo.Owner == args.player)
            {
                //Debug.Log("Open the store!");
                if (EntityInfo.IsTower == true)
                {
                    this.gameObject.SetActive(true);
                    ShipStoreScroller.LoadAvailableUpgradeItems(EInfo.Owner.PKUpgrades.EntityParts);
                }
                else if (EntityInfo.IsTower == false)
                {
                    this.gameObject.SetActive(false);
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (SelectedEntity.TryGetComponent<EntityBase>(out EntityBase BaseInfo))
        {
            Shopper = args.player;
            if (BaseInfo.Owner == args.player)
            {
                this.gameObject.SetActive(true);
                ShipStoreScroller.LoadAvailableStoreItems(BaseInfo.Owner.PKEntities.Entities);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    void ChangeUpgrade(object sender, UpgradeEventArgs args)
    {
        Debug.Log("Upgrade Selected " + args.SelectedIndex );
        if (Shopper.PKUpgrades.EntityParts[args.SelectedIndex] != null)
        {
            SelectedEntityPart = Shopper.PKUpgrades.EntityParts[args.SelectedIndex];
            this.StoreItemDescription.text = SelectedEntityPart.PartName;

            this.StoreItemDescription.text += ", Price: " + SelectedEntityPart.PartCost;

            this.StoreItemDescription.text += ", Size: " + SelectedEntityPart.PartSize;
            if (SelectedEntityPart.AllowMultiple == false)
                this.StoreItemDescription.text += ", One Per Ship";
            if (SelectedEntityPart.EntitySystems.Count != 0)
            {
                this.StoreItemDescription.text += ", Systems: ";
                foreach(string sysname in SelectedEntityPart.EntitySystems)
                {
                    this.StoreItemDescription.text += sysname + " ";
                }
            }
            if (SelectedEntityPart.atr_MaxHealthBonus != 0)
                this.StoreItemDescription.text += ", HP: " + SelectedEntityPart.atr_MaxHealthBonus;

            if (SelectedEntityPart.atr_DefenseBonus != 0)
                this.StoreItemDescription.text += ", Def: " + SelectedEntityPart.atr_DefenseBonus;

            if (SelectedEntityPart.atr_RegenerationBonus != 0)
                this.StoreItemDescription.text += ", RG: " + SelectedEntityPart.atr_RegenerationBonus;

            if (SelectedEntityPart.atr_DamageBonus != 0)
                this.StoreItemDescription.text += ", DMG: " + SelectedEntityPart.atr_DamageBonus;

            if (SelectedEntityPart.atr_PenetrationBonus != 0)
                this.StoreItemDescription.text += ", PEN: " + SelectedEntityPart.atr_PenetrationBonus;

            if (SelectedEntityPart.atr_AttackRangeBonus != 0)
                this.StoreItemDescription.text += ", AR: " + SelectedEntityPart.atr_AttackRangeBonus;

            if (SelectedEntityPart.atr_AttackSpeedBonus != 0)
                this.StoreItemDescription.text += ", AS: " + SelectedEntityPart.atr_AttackSpeedBonus;

            if (SelectedEntityPart. atr_SpeedBonus != 0)
                this.StoreItemDescription.text += ", SPD: " + SelectedEntityPart.atr_SpeedBonus;

            if (SelectedEntityPart.atr_TravelRangeBonus != 0)
                this.StoreItemDescription.text += ", TR: " + SelectedEntityPart.atr_TravelRangeBonus;

            if (SelectedEntityPart.atr_MaxMinionAmountBonus != 0)
                this.StoreItemDescription.text += ", MIN+: " + SelectedEntityPart.atr_MaxMinionAmountBonus;

            if (SelectedEntityPart.atr_CostModifierBonus != 0)
                this.StoreItemDescription.text += ", COST+: " + SelectedEntityPart.atr_CostModifierBonus;

        }
    }
    public void ClearUpgrade(object sender, EventArgs args)
    {
        Debug.Log("Upgrade cleared ");
    }

    public void RemoveUpgradeFromShip()
    {
        if(SelectedEntityPart == null)
        { return;}
        if(SelectedEntity == null)
        { return;}
        if(EntityInfo == null)
        { return;}
        if(EntityInfo.atr_Upgrade)
        {
            bool value;
            value = EntityInfo.atr_Upgrade.RemovePart(SelectedEntityPart);
            Debug.Log("True if worked, false if fail" + value);
            if(value == true)
            {
                if(EntityInfo.IsTower)
                {
                    Shopper.Money += SelectedEntityPart.PartCost / 2;
                    Debug.Log("Gained:" + SelectedEntityPart.PartCost / 2);
                }

            }
        }
    }
    public void AddUpgradeToShip()
    {
        if (SelectedEntityPart == null)
        { return; }
        if (SelectedEntity == null)
        { return; }
        if (EntityInfo == null)
        { return; }
        if (EntityInfo.atr_Upgrade)
        {
            float PartPrice = SelectedEntityPart.PartCost;
            if (EntityInfo.atr_Cost)
            {
                PartPrice = Mathf.RoundToInt( PartPrice * ((EntityInfo.atr_Cost.atr_CostModifier + 100)/ 100));
            }
            bool value = false;
            if (Shopper.Money >= PartPrice )
            {
                Debug.Log("Checking if can place");
                if (EntityInfo.atr_Upgrade.AddPart(SelectedEntityPart) == true)
                {
                    value = true;
                    Shopper.Money -= PartPrice;
                    Debug.Log("Spent:" + PartPrice);
                }
            }
            else
            {
                value = false;
            }
            
            Debug.Log("True if worked, false if fail" + value);
            
        }

    }
}
