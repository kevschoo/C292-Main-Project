using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class ShipStoreUI : MonoBehaviour
{
    [field: SerializeField] public GameObject SelectedEntity { get; set; }
    [field: SerializeField] public Entity EntityInfo { get; set; }

    [field: SerializeField] public GameObject SelectedProduct { get; set; }

    [SerializeField] TextMeshProUGUI StoreItemDescription;

    [SerializeField] DynamicScrollView ShipStoreScroller;

    [SerializeField] Player Shopper;
    void Start()
    {
        EntitySelectEvent.SelectionCleared += ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged += ShowSelectedEntity;
        StoreSelectEvent.PurchaseChanged += ChangePurchase;
        StoreSelectEvent.PurchaseCleared += ClearPurchase;
        this.gameObject.SetActive(false);
        WaveEvent.GameEnd += OnGameEnd;

    }
    private void OnDestroy()
    {
        EntitySelectEvent.SelectionCleared -= ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged -= ShowSelectedEntity;
        StoreSelectEvent.PurchaseChanged -= ChangePurchase;
        StoreSelectEvent.PurchaseCleared -= ClearPurchase;
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
        if (args.SelectedEntity == null)
        { return;}

        SelectedEntity = args.SelectedEntity;
        if (SelectedEntity.TryGetComponent<EntityShip>(out EntityShip EInfo))
        {
            EntityInfo = EInfo;
            Shopper = args.player;
            if (EInfo.Owner == args.player)
            {

                //Debug.Log("Open the store!");

                if (EntityInfo.IsTower == true)
                {
                    this.gameObject.SetActive(true);
                    ShipStoreScroller.LoadAvailableStoreItems(EInfo.Owner.PKEntities.Entities);
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
        else if (SelectedEntity.TryGetComponent<EntityTower>(out EntityTower TInfo))
        {
            Shopper = args.player;
            if (TInfo.Owner == args.player)
            {
                this.gameObject.SetActive(true);
                ShipStoreScroller.LoadAvailableStoreItems(TInfo.Owner.PKEntities.Entities);
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
                if(BaseInfo.IsDefending == false)
                {
                    this.gameObject.SetActive(true);
                    ShipStoreScroller.LoadAvailableStoreItems(TInfo.Owner.PKEntities.Entities);
                }
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }


    void ChangePurchase(object sender, UpgradeEventArgs args)
    {
        Debug.Log("Purchase Selected " + args.SelectedIndex);
        if (Shopper.PKEntities.Entities[args.SelectedIndex] != null)
        {
            SelectedProduct = Shopper.PKEntities.Entities[args.SelectedIndex];
            this.StoreItemDescription.text = SelectedProduct.name;
            if (SelectedProduct.TryGetComponent<EntityShip>(out EntityShip SPES))
            {
                this.StoreItemDescription.text += ", Price: " + SPES.Price;
            }
            if (SelectedProduct.TryGetComponent<AttributeMinions>(out AttributeMinions Min))
            {
                if(Min.atr_CanSpawnMinions)
                {
                    this.StoreItemDescription.text += ", Minions: " + Min.atr_BaseMaxMinionAmount;
                }
            }
            if (SelectedProduct.TryGetComponent<AttributeOffense>(out AttributeOffense Off))
            {
                this.StoreItemDescription.text += ", DMG/PEN/AS/AR/+%: " + Off.atr_BaseDamage + "/" + Off.atr_BasePenetration + "/" + Off.atr_BaseAttackSpeed + "/" + Off.atr_BaseAttackRange + "/" + Off.atr_BaseDamageIncrease ;
            }
            if (SelectedProduct.TryGetComponent<AttributeDefense>(out AttributeDefense Def))
            {
                this.StoreItemDescription.text += ", HP/RG/DEF/-%: " + Def.atr_BaseMaxHealth +"/" + Def.atr_BaseRegeneration + "/" + Def.atr_BaseDefense + "/" + Def.atr_DamageReduction;
            }
            if (SelectedProduct.TryGetComponent<AttributeMobility>(out AttributeMobility Mob))
            {
                this.StoreItemDescription.text += ", SPD/TR: " + Mob.atr_BaseSpeed + "/" + Mob.atr_BaseTravelRange;
            }
            if (SelectedProduct.TryGetComponent<AttributeUpgradeSystem>(out AttributeUpgradeSystem UP))
            {
                this.StoreItemDescription.text += ", MaxPart/MaxSystems: " + UP.MaxParts + "/" + UP.MaxEntitySystems;
            }
        }
    }
    void ClearPurchase(object sender, EventArgs args)
    {
        Debug.Log("Purchase cleared ");
    }

    public void DestroyEntity()
    {
        if (SelectedEntity == null)
        { return; }
        if (EntityInfo == null)
        { return; }
        if(SelectedEntity.TryGetComponent<EntityShip>( out EntityShip EShip))
        {
            if(EShip.IsTower)
            {
                Shopper.Money += EntityInfo.Price / 2;
                Debug.Log("Gained:" + EntityInfo.Price / 2);
            }

            Destroy(SelectedEntity);
        }
        if(SelectedEntity.TryGetComponent<EntityTower>(out EntityTower ETower))
        {
            if(ETower.StationedEntity != null)
            {
                if(ETower.StationedEntity.TryGetComponent<EntityShip>(out EntityShip EnShip))
                {
                    Shopper.Money += EnShip.Price / 2;
                    Debug.Log("Gained:" + EnShip.Price / 2);
                }
                GameObject.Destroy(ETower.StationedEntity);
                ETower.StationedEntityDeath();
            }
        }
        EntitySelectEvent.InvokeSelectionCleared();
        StoreSelectEvent.InvokeSelectionCleared();

    }
    public void ReplaceShip()
    {
        if(SelectedProduct == null)
        { return; }
        if (SelectedEntity == null)
        { return; }
        if(SelectedEntity.TryGetComponent<EntityShip>(out EntityShip EShip))
        {
            Debug.Log("whoopsie not implemented!");
        }
        if (SelectedEntity.TryGetComponent<EntityTower>(out EntityTower ETower))
        {
            GameObject preview = Instantiate(SelectedProduct);
            preview.SetActive(false);

            Debug.Log("Tower!");
            if (preview.GetComponent<EntityShip>())
            {
                EntityShip SPEI = preview.GetComponent<EntityShip>();
                if (Shopper.Money >= SPEI.Price)
                {
                    Debug.Log("Tower set minion");
                    Shopper.Money -= SPEI.Price;
                    Debug.Log("Spent:" + SPEI.Price);
                    ETower.SetStationedEntity(this.SelectedProduct);
                    EntitySelectEvent.InvokeSelectionCleared();
                    StoreSelectEvent.InvokeSelectionCleared();
                }  
            }
            Destroy(preview);
        }
    }

}
