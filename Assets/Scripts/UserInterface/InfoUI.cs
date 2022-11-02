using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InfoUI : MonoBehaviour
{
    [SerializeField] public GameObject SelectedEntity { get; set; }
    [SerializeField] public Entity EntityInfo { get; set; }
    //UI References
    [SerializeField] TextMeshProUGUI EntityNameText;
    [SerializeField] TextMeshProUGUI OwnerText;
    [SerializeField] TextMeshProUGUI AiText;
    [SerializeField] TextMeshProUGUI StatusText;
    [SerializeField] TextMeshProUGUI CostText;
    [SerializeField] TextMeshProUGUI DefenseText;
    [SerializeField] TextMeshProUGUI OffenseText;
    [SerializeField] TextMeshProUGUI MobilityText;
    [SerializeField] TextMeshProUGUI UpgradesText;
    [SerializeField] TextMeshProUGUI SystemsText;
    [SerializeField] TextMeshProUGUI MinionsText;


    void Start()
    {
        EntitySelectEvent.SelectionCleared += ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged += ShowSelectedEntity;
        SelectedEntity = null;
        EntityInfo = null;
        EntityNameText.text = "";
        OwnerText.text = "";
        AiText.text = "";
        StatusText.text = "";
        CostText.text = "";
        DefenseText.text = "";
        OffenseText.text = "";
        MobilityText.text = "";
        UpgradesText.text = "";
        SystemsText.text = "";
        MinionsText.text = "";
    }

    private void OnDestroy()
    {
        EntitySelectEvent.SelectionCleared -= ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged -= ShowSelectedEntity;
    }

    void ShowSelectedEntity(object sender, EntityEventArgs args)
    {
        this.gameObject.SetActive(true);
        SelectedEntity = args.SelectedEntity;
        OwnerText.text = SelectedEntity.name;
        if(SelectedEntity.TryGetComponent<Entity>(out Entity EInfo))
        {
            EntityInfo = EInfo;
        }
    }
    private void FixedUpdate()
    {
        if (SelectedEntity != null)
        {
            if(EntityInfo != null)
            {
                if(EntityInfo.IsTower == false)
                {
                    EntityNameText.text = EntityInfo.EntityName + " | Ship";
                }
                if (EntityInfo.IsTower == true)
                {
                    EntityNameText.text = EntityInfo.EntityName + " | Tower";
                }
                string OwnerTeamParent = "";
                if (EntityInfo.Owner != null)
                {
                    OwnerTeamParent += EntityInfo.Owner.PlayerName + " | ";
                }
                else
                { OwnerTeamParent += "No Owner | "; }
                if (EntityInfo.team != null)
                {
                    OwnerTeamParent += EntityInfo.team.TeamName + " | ";
                }
                else
                { OwnerTeamParent += "No Team | " ; }
                if (EntityInfo.Parent != null)
                {
                    OwnerTeamParent += EntityInfo.Parent.name + " | ";
                } 
                else 
                { OwnerTeamParent += "No Parent" ; }

                OwnerText.text = OwnerTeamParent;

                if (EntityInfo.entityAI)
                {
                    AiText.text = EntityInfo.entityAI.AITypeName + " | ";
                    if(EntityInfo.entityAI.Target != null)
                    {
                        AiText.text += EntityInfo.entityAI.Target.name;
                    }
                }
                if (EntityInfo.atr_Status)
                {
                    StatusText.enabled = true;
                    StatusText.text = "Effects: ";
                    foreach(string a in EntityInfo.atr_Status.CurrentEffects)
                    {
                        StatusText.text += a;
                    }
                    StatusText.text += "<br>Immune: ";
                    foreach (string a in EntityInfo.atr_Status.ImmuneToEffects)
                    {
                        StatusText.text += a;
                    }
                }
                else
                {StatusText.enabled = false;}
                   
                if (EntityInfo.atr_Cost)
                {
                    CostText.enabled = true;
                    CostText.text = "Cost% " + EntityInfo.atr_Cost.atr_CostModifier + " | Upkeep% " + EntityInfo.atr_Cost.atr_UpkeepModifier;
                }
                else
                { CostText.enabled = false;}
                   
                if (EntityInfo.atr_Defense)
                {
                    DefenseText.enabled = true;
                    DefenseText.text = "HP " + EntityInfo.atr_Defense.atr_CurrentHealth + "/" + EntityInfo.atr_Defense.atr_MaxHealth + " | Regen " + EntityInfo.atr_Defense.atr_Regeneration + "<br>Def " + EntityInfo.atr_Defense.atr_Defense + " | -%DMG " + EntityInfo.atr_Defense.atr_DamageReduction;

                }
                else
                {DefenseText.enabled = false;}

                if (EntityInfo.atr_Offense)
                {
                    OffenseText.enabled = true;
                    OffenseText.text = "DMG " + EntityInfo.atr_Offense.atr_Damage + " | Pen " + EntityInfo.atr_Offense.atr_Penetration + "<br>AS " + EntityInfo.atr_Offense.atr_AttackSpeed + " | AR " + EntityInfo.atr_Offense.atr_AttackRange + "<br> +%DMG " + EntityInfo.atr_Offense.atr_DamageIncrease;
                }
                else
                {OffenseText.enabled = false;}

                if (EntityInfo.atr_Mobility)
                {
                    MobilityText.enabled = true;
                    MobilityText.text = "SPD " + EntityInfo.atr_Mobility.atr_Speed + " | TR" + EntityInfo.atr_Mobility.atr_TravelRange;
                }
                else
                {MobilityText.enabled = false;}

                if (EntityInfo.atr_Upgrade)
                {
                    UpgradesText.enabled = true;
                    UpgradesText.text = "Parts " + EntityInfo.atr_Upgrade.CurrentParts + "/" + EntityInfo.atr_Upgrade.MaxParts + " | ";
                    foreach (EntityPart part in EntityInfo.atr_Upgrade.EquippedEntityParts)
                    {UpgradesText.text += part.PartName;}
                }
                else
                {UpgradesText.enabled = false;}

                if (EntityInfo.atr_Upgrade)
                {
                    SystemsText.enabled = true;
                    SystemsText.text = "Systems " + EntityInfo.atr_Upgrade.CurrentEntitySystems + "/" + EntityInfo.atr_Upgrade.MaxEntitySystems + " | ";
                    foreach(EntitySystem part in EntityInfo.atr_Upgrade.EntitySystems)
                    {SystemsText.text += part.SystemName;}
                }
                else
                {SystemsText.enabled = false;
                }
                if (EntityInfo.atr_Minion)
                {
                    MinionsText.enabled = true;
                    MinionsText.text = "Minions " + EntityInfo.atr_Minion.atr_CurMinionAmount + "/" + EntityInfo.atr_Minion.atr_MaxMinionAmount + " | " ;
                    foreach(GameObject Minion in EntityInfo.atr_Minion.Minions)
                    {MinionsText.text += Minion.name;}
                }
                else
                { MinionsText.enabled = false;}
            }
        }
       

    }
    void ClearSelectedEntity(object sender, EventArgs args)
    {
        this.gameObject.SetActive(false);
        SelectedEntity = null;
        EntityInfo = null;
        EntityNameText.text = "";
        OwnerText.text = "";
        AiText.text = "";
        StatusText.text = "";
        CostText.text = "";
        DefenseText.text = "";
        OffenseText.text = "";
        MobilityText.text = "";
        UpgradesText.text = "";
        SystemsText.text = "";
        MinionsText.text = "";
    }




}
