using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public abstract class Entity : MonoBehaviour
{
    //Entity Manages all interactable units, towers, and objects that can deal with any of the Attribute stats
    //Alerts AI component of changes 
    //knows calculates all stats from components to set base values
    //middleman between interactions of health, damage, status effects, upgrades added, minion death, 

    //Outside Stats
    [SerializeField] public abstract SpriteRenderer SpriteRender { get; set; }
    [SerializeField] public abstract Sprite MySprite { get; set; }
    [SerializeField] public abstract GameObject Parent { get; set; }
    [SerializeField] public abstract Team team { get; set; }
    [SerializeField] public abstract Player Owner { get; set; }
    [SerializeField] public abstract PermanentAttributes atr_Perma { get; set; }

    //Inside Stats to be obtained
    [SerializeField] public abstract string EntityName { get; set; }
    [SerializeField] public abstract AttributeOffense atr_Offense { get; set; }
    [SerializeField] public abstract AttributeDefense atr_Defense { get; set; }
    [SerializeField] public abstract AttributeMinions atr_Minion { get; set; }
    [SerializeField] public abstract AttributeMobility atr_Mobility { get; set; }
    [SerializeField] public abstract AttributeCost atr_Cost { get; set; }
    [SerializeField] public abstract AttributeUpgradeSystem atr_Upgrade { get; set; }
    [SerializeField] public abstract AttributeStatusEffect atr_Status { get; set; }
    [SerializeField] public abstract EntityAI entityAI { get; set; }

    //Baseline Bools and conditions for entities
    [SerializeField] public abstract bool IsTower { get; set; }
    [SerializeField] public abstract float Price { get; set; }
    [SerializeField] public abstract float HealthSpawnModifier { get; set; } 
    [SerializeField] public abstract float HealthRegenerationModifier { get; set; } 
    [SerializeField] public abstract bool CanRegenerate { get; set; }
    [SerializeField] public abstract bool IsRegenerating { get; set; } 
    [SerializeField] public abstract bool IsInvincible { get; set; } 
    [SerializeField] public abstract bool CanDieAtZero { get; set; }
    [SerializeField] public abstract bool CanDie { get; set; } 
    [SerializeField] public abstract bool FriendlyFire { get; set; }

    [SerializeField] public abstract bool MinionsDieOnDeath { get; set; }

    public abstract void KillMinions();
    public abstract void MinionDeath(GameObject Minion);
    public abstract void InformParentOfDeath();

    public abstract void CalculateAttributes();
}
