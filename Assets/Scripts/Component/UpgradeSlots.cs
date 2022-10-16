using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

[DisallowMultipleComponent]
public class UpgradeSlots : MonoBehaviour
{

    [SerializeField] int MaxShipParts; //Shouldn't Change unless a part adds more slots (unplanned)
    [SerializeField] int CurShipParts;
    [SerializeField] List<ShipPart> ChosenParts = new List<ShipPart>();
    [SerializeField] List<ShipPart> IllegalParts = new List<ShipPart>();
    [SerializeField] List<UniquePart> UniqueParts = new List<UniquePart>();

    //Defensive
    [field: SerializeField] public int HealthBonus { get; set; }
    [field: SerializeField] public int DefenseBonus { get; set; }
    [field: SerializeField] public int DamageReductionBonus { get; set; }
    [field: SerializeField] public int HealthRegenBonus { get; set; }
    //Offensive
    [field: SerializeField] public int DamageBonus { get; set; }
    [field: SerializeField] public int PenetrationBonus { get; set; }
    [field: SerializeField] public int DamageIncreaseBonus { get; set; }
    [field: SerializeField] public float AttackRangeBonus { get; set; }
    [field: SerializeField] public float AttackSpeedBonus { get; set; }

    //Utility
    [field: SerializeField] public int SpeedBonus { get; set; }
    [field: SerializeField] public int TravelRangeBonus { get; set; }
    [field: SerializeField] public float CostBonus { get; set; }
    [field: SerializeField] public int MinionAmountBonus { get; set; }
    [field: SerializeField] public float MinionCooldownBonus { get; set; }

    void Awake()
    {
        //Debug.Log("Start Calc Bonus ------------");
        CheckForIllegalParts();
        CheckForTooManyParts();
        CalculateBonuses();
        AddUnqiueParts();
    }

    void CheckForIllegalParts()
    {
        List<ShipPart> PartsToRemove = new List<ShipPart>();
        for (int i = 0; i < ChosenParts.Count; i++)
        {
            if (IllegalParts.Contains(ChosenParts[i]))
            {
                Debug.Log("Found Illegal " + ChosenParts[i].name + "---------------------");
                PartsToRemove.Add(ChosenParts[i]);
            }
            else if (ChosenParts[i].AllowMultiple == false)
            {
                IllegalParts.Add(ChosenParts[i]);
            }
        }
        if(PartsToRemove.Count > 0)
        {
            foreach(ShipPart part in PartsToRemove)
            {
                Debug.Log("Removed Illegal "+ part + "----------------------------------");
                ChosenParts.Remove(part);
            }
        }
    }
    void CheckForTooManyParts()
    {
        CalculateShipParts();
        //Debug.Log("Parts" + this.CurShipParts + ", Max" + this.MaxShipParts);
        while (HasTooManyParts() == true)
        {
           
           this.RemovePart(ChosenParts.Last());
           CalculateShipParts();
        }
        
    }
    public void CalculateBonuses()
    {
        if(ChosenParts.Count == 0)
        {return;}

        //Debug.Log("Set to 0 --------------------------------------------------------------");
        CurShipParts = 0;
        HealthBonus = 0;
        DefenseBonus = 0;
        DamageReductionBonus = 0;
        HealthRegenBonus = 0;
        DamageBonus = 0;
        PenetrationBonus = 0;
        DamageIncreaseBonus = 0;
        AttackRangeBonus = 0;
        AttackSpeedBonus = 0;
        SpeedBonus = 0;
        TravelRangeBonus = 0;
        CostBonus = 0;
        MinionAmountBonus = 0;
        MinionCooldownBonus = 0;

        for (int i = 0; i < ChosenParts.Count; i++)
        {
            CurShipParts += ChosenParts[i].PartSize;
            HealthBonus += ChosenParts[i].HealthBonus;
            DefenseBonus += ChosenParts[i].DefenseBonus;
            DamageReductionBonus += ChosenParts[i].DamageReductionBonus;
            HealthRegenBonus += ChosenParts[i].HealthRegenBonus;
            DamageBonus += ChosenParts[i].DamageBonus;
            PenetrationBonus += ChosenParts[i].PenetrationBonus;
            DamageIncreaseBonus += ChosenParts[i].DamageIncreaseBonus;
            AttackRangeBonus += ChosenParts[i].AttackRangeBonus;
            AttackSpeedBonus += ChosenParts[i].AttackSpeedBonus;
            SpeedBonus += ChosenParts[i].SpeedBonus;
            TravelRangeBonus += ChosenParts[i].TravelRangeBonus;
            CostBonus += ChosenParts[i].CostBonus;
            MinionAmountBonus += ChosenParts[i].MinionAmountBonus;
            MinionCooldownBonus += ChosenParts[i].MinionCooldownBonus;
        }
    }
    bool HasTooManyParts()
    {
        if(MaxShipParts <= 0)
        {
            MaxShipParts = 0;
        }
        if (CurShipParts > MaxShipParts)
        {
            return true;
        }
        return false;
    }
    bool HasRoomForParts()
    {
        if (CurShipParts < MaxShipParts)
        {
            return true;
        }
        return false;
    }

    bool HasRoomForThisPart(ShipPart NewPart)
    {
        if (CurShipParts + NewPart.PartSize <= MaxShipParts)
        {
            return true;
        }
        return false;
    }


    ShipPart MostRecentPart()
    {
        if(ChosenParts.Count == 0)
        {
            return null;
        }
        return ChosenParts.Last();
    }

    bool AddPart(ShipPart Part)
    {
        if(IllegalParts.Contains(Part))
        {
            return false;
        }
        if(HasRoomForThisPart(Part))
        {
            ChosenParts.Add(Part);
            CalculateBonuses();
            return true;
        }
        return false;
    }

    bool RemovePart(ShipPart Part)
    {
        //Debug.Log("Removing Part" + Part.name);
        if (ChosenParts.Contains(Part))
        {
            ChosenParts.Remove(Part);
            CalculateBonuses();
            return true;
        }
        return false;
    }

    void CalculateShipParts()
    {
        CurShipParts = 0;
        for (int i = 0; i < ChosenParts.Count; i++)
        {
            CurShipParts += ChosenParts[i].PartSize;
        }
    }
   
    ShipPart GetLargestPart()
    {
        int index = 0;
        int biggest = 0;
        if (ChosenParts.Count == 0)
        {
            return null;
        }
        for (int i = 0; i < ChosenParts.Count; i++)
        {
            if (ChosenParts[i].PartSize > biggest)
            {
                index = i;
                biggest = ChosenParts[i].PartSize;
            }
        }
        return ChosenParts[index];
    }

    void AddUnqiueParts()
    {
        for (int i = 0; i < ChosenParts.Count; i++)
        {
            if (ChosenParts[i].UniqueShipParts.Count != 0)
            {
                for(int j = 0; j < ChosenParts[i].UniqueShipParts.Count; j++)
                {
                    string classname = ChosenParts[i].UniqueShipParts[j];
                    Component x = gameObject.AddComponent(Type.GetType(classname));
                    if (x.GetType().IsSubclassOf(typeof(UniquePart)))
                    {
                        this.UniqueParts.Add((UniquePart)x);
                    }
                }
                
            }
        }
    }

    void RemoveUnqiueParts()
    {
        for (int i = 0; i < UniqueParts.Count; i++)
        {
            Debug.Log(UniqueParts[i].name + " Removed ---------------------------------------------------------------------------");
            UniqueParts[i].RemovePart();
        }
    }
}
