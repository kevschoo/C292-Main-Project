using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    [SerializeField] bool IsMouseOver = false;

    [SerializeField] GameObject MinionTemplate;
    [SerializeField] GameObject MinionSpawned;

    [SerializeField] GameObject Enemy;

    [SerializeField] Transform HomeLocation;
    [SerializeField] Player Owner;
    [SerializeField] string Team;
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
        if(MinionSpawned != null)
        {
            ActivateTower();
        }
    }

    void ActivateTower()
    {
        if(Enemy != null)
        {

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
