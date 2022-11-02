using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class EntityTower : MonoBehaviour
    {
        //Entity Manages all interactable units, towers, and objects that can deal with any of the Attribute stats
        //Alerts AI component of changes 
        //knows calculates all stats from components to set base values
        //middleman between interactions of health, damage, status effects, upgrades added, minion death, 

        //Outside Stats
    [field: SerializeField] public  SpriteRenderer SpriteRender { get; set; }
    [field: SerializeField] public  Sprite MySprite { get; set; }
    [field: SerializeField] public  GameObject Parent { get; set; }
    [field: SerializeField] public  Team team { get; set; }
    [field: SerializeField] public  Player Owner { get; set; }
    [field: SerializeField] public GameObject StationedEntityPrefab { get; set; }
    [field: SerializeField] public GameObject StationedEntity { get; set; }

    private void Awake()
    {


    }

    private void Start()
    {
    }

    private void Update()
    {

    }
    private void SpawnStationedEntity()
    {
        StationedEntity = Instantiate(StationedEntityPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        if (StationedEntity.TryGetComponent<EntityShip>(out EntityShip EShip))
        {
            EShip.Parent = this.gameObject;
            EShip.Owner = this.Owner;
            EShip.team = this.team;
        }
    }

    public void KillStationedEntity()
    {
        if (this.StationedEntity != null)
        {
            GameObject.Destroy(this.StationedEntity);
        }
    }
    public void StationedEntityDeath()
    {
        this.StationedEntityPrefab = null;
        this.StationedEntity = null;
        this.gameObject.SetActive(true);
    }

    public void SetStationedEntity(GameObject Minion)
    {
        if (StationedEntity != null)
        {
            Debug.Log("DestroyingOldMinion!");
            GameObject.Destroy(this.StationedEntity);
        }
        StationedEntityPrefab = Minion;
        SpawnStationedEntity();
        this.gameObject.SetActive(false);
    }
    public void InformParentOfDeath()
    {
        if (this.Parent != null)
        { Parent.GetComponent<Entity>().MinionDeath(this.gameObject); }
    }


    private void OnDestroy()
    {
        InformParentOfDeath();
        KillStationedEntity();
    }
    
}










