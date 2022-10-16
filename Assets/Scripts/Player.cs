using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [SerializeField] PermUpgrades PlayersPermUpgrade;
    [SerializeField] List<GameObject> PlayerOwnedObjects = new List<GameObject>();
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject selectedObject;
    [SerializeField] int index = 0;

    private PlayerControls playerControls;
    private void Awake()
    {
        playerControls = new PlayerControls();

    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    void Start()
    {
        if(PlayerOwnedObjects.Count > 0)
        {
            selectedObject = PlayerOwnedObjects[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        float leftclick = playerControls.BaseControls.RightClick.ReadValue<float>();
        if (leftclick > .5f)
        {
            ChangeSelectedObject(1);
        }
        if(selectedObject != null)
        {
            mainCam.transform.position = selectedObject.transform.position;
        }
    }


    void ChangeSelectedObject(int x)
    {
        Debug.Log("Changing Selected Obj");
        if (PlayerOwnedObjects.Count == 0 || PlayerOwnedObjects.Count == 1)
        {
            return;
        }
        if (PlayerOwnedObjects.Count > 1)
        {
            index++;
            if(index > PlayerOwnedObjects.Count-1)
            {
                index = 0;
            }
                selectedObject.GetComponent<PlayerShipAi>().enabled = false;
                selectedObject.GetComponent<NavMeshAgent>().enabled = false;
                
                selectedObject = PlayerOwnedObjects[index];
                selectedObject.GetComponent<NavMeshAgent>().enabled = true;
                selectedObject.GetComponent<PlayerShipAi>().enabled = true;
            
            
        }
    }
}
