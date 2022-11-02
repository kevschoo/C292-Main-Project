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
    }
    private void OnDestroy()
    {
        EntitySelectEvent.SelectionCleared -= ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged -= ShowSelectedEntity;
        UpgradeSelectEvent.UpgradeChanged -= ChangeUpgrade;
        UpgradeSelectEvent.UpgradeCleared -= ClearUpgrade;
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
