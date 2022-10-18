using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [SerializeField] PermUpgrades PlayersPermUpgrade;

    [SerializeField] List<SpaceObject> PlayerOwnedObjects = new List<SpaceObject>();
    [SerializeField] Camera MainCam;
    [SerializeField] GameObject SelectedObject;
    [SerializeField] int index = 0;
    [SerializeField] bool FreeCam = false;
    [SerializeField] bool InMenu = false;



    void Start()
    {
        if (PlayerOwnedObjects.Count > 0)
        {
            SelectedObject = PlayerOwnedObjects[0].gameObject;
            SetAllOwnedObjects();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //MainCam.transform.position = new Vector3(hit.point.x, hit.point.y, MainCam.transform.position.z);
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }

        if (Input.GetButtonDown("Escape"))
        {
            if (InMenu == true)
            {
                InMenu = false;
                GameTime.Resume();
            }
            else
            {
                InMenu = true;
                GameTime.Pause();
            }
        }


    }

    void SetAllOwnedObjects()
    {
        foreach (SpaceObject obj in this.PlayerOwnedObjects)
        {
            obj.SetOwner(this);
        }
    }
    void SetObjectsOwner(SpaceObject obj)
    {
        obj.SetOwner(this);
    }





}
