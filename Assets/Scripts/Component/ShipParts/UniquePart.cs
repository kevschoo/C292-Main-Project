using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UniquePart : MonoBehaviour
{

    [SerializeField] public abstract string PartName { get; set; }
    [SerializeField] public abstract string Category { get; set; }
    [SerializeField] public abstract bool isActive { get; set; }
    public abstract void Activate(GameObject Target);
    public abstract void Activate(GameObject Target, GameObject Self);
    public abstract void Activate();
    public abstract void DeActivate();
    public abstract void RemovePart();
  
}
