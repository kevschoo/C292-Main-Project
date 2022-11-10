using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttributeUpgradeSystem : MonoBehaviour
{
    //Core Lists and stats
    [field: SerializeField] public int MaxParts { get; set; }
    [field: SerializeField] public int CurrentParts { get; set; }
    [field: SerializeField] public int MaxEntitySystems { get; set; }
    [field: SerializeField] public int CurrentEntitySystems { get; set; }
    [field: SerializeField] public List<EntityPart> EquippedEntityParts { get; set; }
    [field: SerializeField] public List<EntityPart> IllegalParts { get; set; }
    [field: SerializeField] public List<EntitySystem> EntitySystems { get; set; }

    //List of bonuses

    //Defensive
    [field: SerializeField] public float atr_MaxHealthBonus { get; set; }
    [field: SerializeField] public float atr_DefenseBonus { get; set; }
    [field: SerializeField] public float atr_DamageReductionBonus { get; set; }
    [field: SerializeField] public float atr_RegenerationBonus { get; set; }
    //Offensive
    [field: SerializeField] public float atr_DamageBonus { get; set; }
    [field: SerializeField] public float atr_PenetrationBonus { get; set; }
    [field: SerializeField] public float atr_DamageIncreaseBonus { get; set; }
    [field: SerializeField] public float atr_AttackRangeBonus { get; set; }
    [field: SerializeField] public float atr_AttackSpeedBonus { get; set; }

    //Utility
    [field: SerializeField] public float atr_SpeedBonus { get; set; }
    [field: SerializeField] public float atr_TravelRangeBonus { get; set; }
    [field: SerializeField] public float atr_CostModifierBonus { get; set; }
    [field: SerializeField] public float atr_UpkeepModifierBonus { get; set; }
    [field: SerializeField] public int atr_MaxMinionAmountBonus { get; set; }
    [field: SerializeField] public float atr_MinionCooldownModifierBonus { get; set; }

    private void Awake()
    {
        CheckForIllegalParts();
        CheckForTooManyParts();
        CalculateBonuses();
        AddEntitySystems();
    }

    public void CalculateBonuses()
    {

        //Debug.Log("Set to 0 --------------------------------------------------------------");
        CurrentParts = 0;
        atr_MaxHealthBonus = 0;
        atr_DefenseBonus = 0;
        atr_DamageReductionBonus = 0;
        atr_RegenerationBonus = 0;
        atr_DamageBonus = 0;
        atr_PenetrationBonus = 0;
        atr_DamageIncreaseBonus = 0;
        atr_AttackRangeBonus = 0;
        atr_AttackSpeedBonus = 0;
        atr_SpeedBonus = 0;
        atr_TravelRangeBonus = 0;
        atr_CostModifierBonus = 0;
        atr_UpkeepModifierBonus = 0;
        atr_MinionCooldownModifierBonus = 0;

        foreach(EntityPart part in EquippedEntityParts)
        {
            CurrentParts += part.PartSize;
            atr_MaxHealthBonus += part.atr_MaxHealthBonus;
            atr_DefenseBonus += part.atr_DefenseBonus;
            atr_DamageReductionBonus += part.atr_DamageReductionBonus;
            atr_RegenerationBonus += part.atr_RegenerationBonus;
            atr_DamageBonus += part.atr_DamageBonus;
            atr_PenetrationBonus += part.atr_PenetrationBonus;
            atr_DamageIncreaseBonus += part.atr_DamageIncreaseBonus;
            atr_AttackRangeBonus += part.atr_AttackRangeBonus;
            atr_AttackSpeedBonus += part.atr_AttackSpeedBonus; ;
            atr_SpeedBonus += part.atr_SpeedBonus;
            atr_TravelRangeBonus += part.atr_TravelRangeBonus;
            atr_CostModifierBonus += part.atr_CostModifierBonus;
            atr_UpkeepModifierBonus += part.atr_UpkeepModifierBonus;
            atr_MinionCooldownModifierBonus += part.atr_MinionCooldownModifierBonus;
        }
        if (gameObject.GetComponent<Entity>())
        { gameObject.GetComponent<Entity>().CalculateAttributes(); }
        if (gameObject.TryGetComponent<EntityAI>(out EntityAI ThisAi))
        { ThisAi.FindAllEntitySystems(); }
    }

    void CheckForIllegalParts()
    {
        List<EntityPart> PartsToRemove = new List<EntityPart>();
        for (int i = 0; i < EquippedEntityParts.Count; i++)
        {
            if (IllegalParts.Contains(EquippedEntityParts[i]))
            {
                Debug.Log("Found Illegal " + EquippedEntityParts[i].name + "---------------------");
                PartsToRemove.Add(EquippedEntityParts[i]);
            }
            else if (EquippedEntityParts[i].AllowMultiple == false)
            {
                IllegalParts.Add(EquippedEntityParts[i]);
            }
        }
        if (PartsToRemove.Count > 0)
        {
            foreach (EntityPart part in PartsToRemove)
            {
                Debug.Log("Removed Illegal " + part + "----------------------------------");
                EquippedEntityParts.Remove(part);
            }
        }
    }
    void CheckForTooManyParts()
    {
        CalculateShipParts();
        while (HasTooManyParts() == true)
        {
            this.RemovePart(EquippedEntityParts.Last());
            CalculateShipParts();
        }

    }
    bool HasTooManyParts()
    {
        if (MaxParts <= 0)
        { MaxParts = 0; }

        if (CurrentParts > MaxParts)
        { return true; }


        return false;
    }
    void CalculateShipParts()
    {
        CurrentParts = 0;
        for (int i = 0; i < EquippedEntityParts.Count; i++)
        { CurrentParts += EquippedEntityParts[i].PartSize; }
    }

    //Part Adder and removers

    public bool AddPart(EntityPart Part)
    {
        if (IllegalParts.Contains(Part))
        {return false;}
        Debug.Log("Not Illegal");
        if (HasRoomForThisPart(Part))
        {
            //Debug.Log("adding");
            EquippedEntityParts.Add(Part);
            if(this.gameObject.TryGetComponent<EntityAI>(out EntityAI Ai))
            {
                Ai.UpdateMinionUpgrades(Part);
            }


            if(Part.EntitySystems.Count > 0)
            {
                AddEntitySystem(Part);
            }
            CalculateBonuses();
            if (Part.AllowMultiple == false)
            { IllegalParts.Add(Part); }
            return true;
        }
        Debug.Log("No Room");
        return false;
    }
    public bool HasRoomForThisPart(EntityPart NewPart)
    {
        Debug.Log("NewPartsize" + NewPart.PartSize);
        if (CurrentParts + NewPart.PartSize > MaxParts)
        { return false; }
        Debug.Log("NewSystemSize" + NewPart.EntitySystems.Count);
        if (CurrentEntitySystems + NewPart.EntitySystems.Count > MaxEntitySystems)
        {return false;}
        return true;
    }


    public bool RemovePart(EntityPart Part)
    {
        if (EquippedEntityParts.Contains(Part))
        {
            if (this.gameObject.TryGetComponent<EntityAI>(out EntityAI Ai))
            {
                Ai.RemoveMinionUpgrades(Part);
            }
            EquippedEntityParts.Remove(Part);
            this.CurrentParts -= Part.PartSize;
            foreach (string partName in Part.EntitySystems)
            {
                List<EntitySystem> RemoveSystems = new List<EntitySystem>();
                foreach(EntitySystem system in this.EntitySystems)
                {
                    if(system.SystemName == partName)
                    {
                        RemoveSystems.Add(system);
                    }
                }
                foreach(EntitySystem system in RemoveSystems)
                {
                    this.CurrentEntitySystems--; 
                    EntitySystems.Remove(system);
                    system.RemovePart();
                }
            }
            CalculateBonuses();
            if (IllegalParts.Contains(Part))
            {IllegalParts.Remove(Part);}
            return true;
        }

       
        return false;
    }

    void AddEntitySystems()
    {
        for (int i = 0; i < EquippedEntityParts.Count; i++)
        {
            if (EquippedEntityParts[i].EntitySystems.Count != 0)
            {
                for (int j = 0; j < EquippedEntityParts[i].EntitySystems.Count; j++)
                {
                    string classname = EquippedEntityParts[i].EntitySystems[j];
                    Component x = gameObject.AddComponent(Type.GetType(classname));
                    if (x.GetType().IsSubclassOf(typeof(EntitySystem)))
                    {
                        this.EntitySystems.Add((EntitySystem)x);
                    }
                    this.CurrentEntitySystems++;
                }

            }
        }
    }
    void AddEntitySystem(EntityPart part)
    {
        for (int i = 0; i < part.EntitySystems.Count; i++)
        {
            string classname = part.EntitySystems[i];
            Component x = gameObject.AddComponent(Type.GetType(classname));
            if (x.GetType().IsSubclassOf(typeof(EntitySystem)))
            {
                this.EntitySystems.Add((EntitySystem)x);
            }
            CurrentEntitySystems++;
        }
    }
    void RemoveEntitySystems(string systemToRemove)
    {
        Component x = gameObject.GetComponent(Type.GetType(systemToRemove));
        if (x.GetType().IsSubclassOf(typeof(EntitySystem)))
        {
            if (this.EntitySystems.Contains((EntitySystem)x))
            {
                this.EntitySystems.Remove((EntitySystem)x);
            }  
        }
    }

}
