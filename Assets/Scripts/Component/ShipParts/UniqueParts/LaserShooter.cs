using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LaserShooter : UniquePart
{
    [field: SerializeField] public override string PartName { get; set; }
    [field: SerializeField] public override string Category { get; set; }
    [field: SerializeField] public override bool isActive { get; set; }

    [field: SerializeField] GameObject BulletType;

    bool IsOnCoolDown;

    public override void Activate()
    {
        this.isActive = true;
        return;
    }

    public override void Activate(GameObject Target)
    {
        this.isActive = true;
        return;
    }

    public override void Activate(GameObject Target, GameObject Self)
    {
        this.isActive = true;
        return;
    }

    public override void DeActivate()
    {
        this.isActive = false;
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
        this.IsOnCoolDown = false;
        if(this.gameObject.GetComponent<ShipNavMeshAI>().BulletType)
        {
            BulletType = this.gameObject.GetComponent<ShipNavMeshAI>().BulletType;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && BulletType != null)
        {
            if(IsOnCoolDown == false)
            {
                IsOnCoolDown = true;
                StartCoroutine(FireLaser());
            }

        }
    }

    IEnumerator FireLaser()
    {
        //Debug.Log("start firing");
        
        GameObject Bullet = Instantiate(BulletType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Bullet.transform.rotation = this.gameObject.transform.rotation;
        yield return new WaitForSeconds(.5f);
        IsOnCoolDown = false;

    }
}
