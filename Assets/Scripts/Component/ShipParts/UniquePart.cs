using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniquePart : MonoBehaviour
{

    [field: SerializeField] public abstract string PartName { get; set; }
    [field: SerializeField] public abstract string Category { get; set; }
    [field: SerializeField] public abstract bool isActive { get; set; }
    public abstract void Activate();
    public abstract void DeActivate();
    public abstract void RemovePart();
  
}
