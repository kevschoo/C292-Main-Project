using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaserShooter : UniquePart
{
    [field: SerializeField] public override string PartName { get; set; }
    [field: SerializeField] public override string Category { get; set; }
    [field: SerializeField] public override bool isActive { get; set; }

    public override void Activate()
    {
        return;
    }

    public override void DeActivate()
    {
        return;
    }

    public override void RemovePart()
    {
        Destroy(this); 
    }



    // Start is called before the first frame update
    void Start()
    {
        this.PartName = "Laser Shooter";
        this.Category = "Weapon";
        this.isActive = false;
        Debug.Log("LaserShooter Part Created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
