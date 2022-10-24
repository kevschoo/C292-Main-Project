using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Networking.UnityWebRequest;

public class Tower : MonoBehaviour
{

    [field: SerializeField] public GameObject MinionTemplate { get; set; }

    [field: SerializeField] public GameObject TestObject { get; set; }
    //Refactor, remove this list, have it so if it doesnt have minionstat, add one to object
    [field: SerializeField] public List<GameObject> MinionsSpawned { get; set; }
    [field: SerializeField] public GameObject Enemy { get; set; }
    [field: SerializeField] public Player Owner { get; set; }
    [field: SerializeField] public string Team { get; set; }
    //[field: SerializeField] public NavMesh LevelMesh { get; set; }
    //maybe want to have selector mouse over allow ui overlay
    [SerializeField] SelectableObj Selector;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<SelectableObj>())
        {
            Selector = this.GetComponent<SelectableObj>();
        }
        NavMeshHit hit;
        if (NavMesh.SamplePosition(this.transform.position, out hit, 5.0f, NavMesh.AllAreas))
        {
            Vector3 result = hit.position;
           
            if(TestObject != null)
            {
                Debug.Log("Spawning object:" +result);
                GameObject newUnit = Instantiate(this.TestObject, new Vector3(result.x, result.y, this.transform.position.z), Quaternion.identity);
            }
            //Spawn(result);


        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MinionsSpawned.Count != 0)
        {
            ActivateTower();
        }
    }

    void ActivateTower()
    {
        if(Enemy == null)
        {
            return;
        }

    }

    public void MinionDeathListener(GameObject unit)
    {
        Debug.Log(this.name + ":heard my minions died");
        if (this.MinionsSpawned.Count != 0)
        {
            MinionsSpawned.Remove(unit);
        }
    }

    private void OnDestroy()
    {
        Debug.Log(this.name + ":Telling minions i died");
        if (this.gameObject.GetComponent<SpaceObject>() == null)
        {
            foreach(GameObject unit in this.MinionsSpawned)
            {
                Destroy(unit);
            }
        }
    }

    public void Spawn()
    {
        if(MinionTemplate == null)
        {
            Debug.Log("No Minion Template");
            return;
        }
        if(this.gameObject.GetComponent<MinionStat>() != null)
        {
            MinionStat min = this.gameObject.GetComponent<MinionStat>();
            if(min._CurMinionAmount < min._MaxMinionAmount)
            {
                min._CurMinionAmount++;
                GameObject newUnit = Instantiate(this.MinionTemplate, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                SpaceObject NUSO = newUnit.GetComponent<SpaceObject>();
                NUSO.SetOwner(this.Owner);
                NUSO.SetTeam(this.Team);
                NUSO.Parent = this.gameObject;
                this.MinionsSpawned.Add(newUnit);
                min.Minions.Add(newUnit);
            }
        }
        else
        {
            if(MinionsSpawned.Count == 0)
            {
                GameObject newUnit = Instantiate(this.MinionTemplate, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                SpaceObject NUSO = newUnit.GetComponent<SpaceObject>();
                NUSO.SetOwner(this.Owner);
                NUSO.SetTeam(this.Team);
                NUSO.Parent = this.gameObject;
                this.MinionsSpawned.Add(newUnit);
            }
        }
    }
    public void Spawn(Vector3 SpawnPos)
    {
        if (MinionTemplate == null)
        {
            Debug.Log("No Minion Template");
            return;
        }
        if (this.gameObject.GetComponent<MinionStat>() != null)
        {
            MinionStat min = this.gameObject.GetComponent<MinionStat>();
            if (min._CurMinionAmount < min._MaxMinionAmount)
            {
                min._CurMinionAmount++;
                GameObject newUnit = Instantiate(this.MinionTemplate, new Vector3(SpawnPos.x, SpawnPos.y, this.transform.position.z), Quaternion.identity);
                SpaceObject NUSO = newUnit.GetComponent<SpaceObject>();
                NUSO.SetOwner(this.Owner);
                NUSO.SetTeam(this.Team);
                NUSO.Parent = this.gameObject;
                this.MinionsSpawned.Add(newUnit);
                min.Minions.Add(newUnit);
            }
        }
        else
        {
            if (MinionsSpawned.Count == 0)
            {
                GameObject newUnit = Instantiate(this.MinionTemplate, new Vector3(SpawnPos.x, SpawnPos.y, this.transform.position.z), Quaternion.identity);
                SpaceObject NUSO = newUnit.GetComponent<SpaceObject>();
                NUSO.SetOwner(this.Owner);
                NUSO.SetTeam(this.Team);
                NUSO.Parent = this.gameObject;
                this.MinionsSpawned.Add(newUnit);
            }
        }
    }
    void OnMouseOver()
    {
        //Debug.Log("MouseOver Tower");
    }

    void OnMouseDown()
    {
        Debug.Log("Tower Clicked");
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exit Tower");
    }


}
