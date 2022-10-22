using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] bool IsMouseOver = false;

    [field: SerializeField] public GameObject MinionTemplate { get; set; }
    [field: SerializeField] public List<GameObject> MinionsSpawned { get; set; }
    [field: SerializeField] public GameObject Enemy { get; set; }
    [field: SerializeField] public Transform HomeLocation { get; set; }
    [field: SerializeField] public Player Owner { get; set; }
    [field: SerializeField] public string Team { get; set; }
    [SerializeField] SelectableObj Selector;

    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<SelectableObj>())
        {
            Selector = this.GetComponent<SelectableObj>();
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
