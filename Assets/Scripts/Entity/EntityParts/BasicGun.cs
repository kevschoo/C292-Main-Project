using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using TMPro;

[System.Serializable]
public class BasicGun : EntitySystem
{

    [field: SerializeField] public override string SystemName { get; set; } = "BasicGun";
    [field: SerializeField] public override string Category { get; set; } = "Offense";
    [field: SerializeField] public override bool isActive { get; set; } = false;

    [field: SerializeField] GameObject BulletType;

    [SerializeField] EntityAI ParentAI;
    [SerializeField] Entity Parent;
    [SerializeField] AttributeOffense atr_Off;
    [SerializeField] int MaxTargetsToPenetrate;
    [SerializeField] float BulletSpeed = 7;
    [SerializeField] float BulletLifetime = 2;
    [SerializeField] float BaseFireCooldown = 1;
    bool IsOnCoolDown;
    Vector3 AimPoint;
    [SerializeField] Quaternion RotationAmount;

    public override void Activate()
    {
        this.isActive = true;
        return;
    }

    public override void Activate(GameObject Target)
    {
        this.isActive = true;
        if (Target.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            AimPoint = Target.transform.position + (agent.velocity * BulletSpeed * Time.deltaTime);
            Vector2 DirectionToLook = AimPoint - this.transform.position;

            float Angle = Mathf.Atan2(DirectionToLook.y, DirectionToLook.x) * Mathf.Rad2Deg;
            RotationAmount = Quaternion.AngleAxis(Angle, Vector3.forward);
        }
    }

    public override void Activate(GameObject Target, GameObject Self)
    {
        this.isActive = true;
    }

    public override void DeActivate()
    {

        this.isActive = false;
    }

    public override void RemovePart()
    {
        Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.isActive = false;
        Debug.Log("BeamsI Part Created");
        this.IsOnCoolDown = false;
        if (this.gameObject.TryGetComponent<EntityAI>(out EntityAI PAI))
        {
            ParentAI = PAI;
            if (ParentAI.OffensiveSystemData)
            {
                BulletType = ParentAI.OffensiveSystemData.SpawnableObjects.Where(gameObject => gameObject.name == "BeamsI").SingleOrDefault();
            }
            else
            {
                Debug.Log("Failure to find proper bullet data");
            }

        }
        if (this.gameObject.TryGetComponent<Entity>(out Entity ENT))
        {
            Parent = ENT;
            if (Parent.atr_Offense)
            { atr_Off = Parent.atr_Offense; }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && BulletType != null && atr_Off != null)
        {
            if (IsOnCoolDown == false)
            {
                IsOnCoolDown = true;
                StartCoroutine(FireLaser());
            }
        }
        if (ParentAI.Target == null)
        { isActive = false; }
    }

    IEnumerator FireLaser()
    {
        //Debug.Log("start firing");

        GameObject Bullet = Instantiate(BulletType, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        if (RotationAmount != null)
        {
            Bullet.transform.rotation = RotationAmount;
        }
        if (Bullet.TryGetComponent<Bullet>(out Bullet B))
        {
            B.Parent = this.gameObject;
            B.team = Parent.team;
            B.Owner = Parent.Owner;
            B.AllowFriendlyFire = Parent.FriendlyFire;
            B.MaxTargetsToPenetrate = MaxTargetsToPenetrate;
            B.Speed = BulletSpeed;
            B.Lifetime = BulletLifetime;
            B.ParentDamage = atr_Off.atr_Damage;
            B.ParentDamageIncrease = atr_Off.atr_DamageIncrease;
            B.ParentPenetration = atr_Off.atr_Penetration;
        }

        Bullet.transform.rotation = this.gameObject.transform.rotation;
        yield return new WaitForSeconds(BaseFireCooldown / atr_Off.atr_AttackSpeed);
        IsOnCoolDown = false;

    }


}
