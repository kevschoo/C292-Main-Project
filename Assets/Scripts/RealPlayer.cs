using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : Player
{
    [field: SerializeField] public override PermUpgrades PlayersPermUpgrade { get; set; }
    [field: SerializeField] public override List<SpaceObject> PlayerOwnedObjects { get; set; }
    [field: SerializeField] public override GameObject SelectedObject { get; set; }
    [field: SerializeField] public override int index { get; set; }

    [SerializeField] Camera MainCam;

    [SerializeField] bool InMenu = false;
    [SerializeField] bool PauseInMenu = false;

    //listens for invoke(pause)
    //calls menu closing stuff
    void Start()
    {
        if (PlayerOwnedObjects == null)
        {
            PlayerOwnedObjects = new List<SpaceObject>();
        }
        index = 0;
        this.SelectedObject = this.gameObject;
        this.MainCam = Camera.main;
        ObjectSelectEvent.SelectionCleared += OnClearedSelect;
        ObjectSelectEvent.SelectionChanged += ShowSelectedShip;

        if (PlayerOwnedObjects.Count > 0)
        {
            SetAllOwnedObjects();
            if(this.PlayersPermUpgrade != null)
            {
                this.SetAllPermUpgrades();
            }
        }

    }
    private void OnDestroy()
    {
        ObjectSelectEvent.SelectionCleared -= OnClearedSelect;
        ObjectSelectEvent.SelectionChanged -= ShowSelectedShip;
    }

    void ShowSelectedShip(object sender, ObjectSelectEventArgs args)
    {
        SelectedObject = args.SelectedObj;
        //Camera.main.transform.position = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20);
    }
    void OnClearedSelect(object sender, EventArgs args)
    {
        this.SelectedObject = null;
        this.SelectedObject = this.gameObject;
        Debug.Log("UserShipShop: Event caused object clear ");
    }

    private void LateUpdate()
    {

        if (MainCam != null)
        {
            if (SelectedObject == null)
            {
                //Camera.main.transform.position = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20);
                Vector3 SmoothPos = Vector3.Lerp(MainCam.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, -20), .05f);
                Camera.main.transform.position = SmoothPos;
            }
            else
            {
                Vector3 TargetPos = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20);
                Vector3 SmoothPos = Vector3.Lerp(MainCam.transform.position, TargetPos, .05f);
                Camera.main.transform.position = SmoothPos;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Shift"))
        {
            float minSize = 1f;
            float maxSize = 20f;
            float CameraSize = Camera.main.orthographicSize;
            CameraSize += Input.GetAxis("Mouse ScrollWheel") * -10f;
            CameraSize = Mathf.Clamp(CameraSize, minSize, maxSize);
            Camera.main.orthographicSize = CameraSize;
        }

        if (Input.GetButtonDown("Escape") || Input.GetMouseButtonDown(2))
        {
            Debug.Log("Player calling clear");
            ObjectSelectEvent.InvokeSelectionCleared();
            
            if (InMenu == true && PauseInMenu == true)
            {
                InMenu = false;
                GameTime.Resume();

            }
            else if (InMenu == false && PauseInMenu == true)
            {
                InMenu = true;
                GameTime.Pause();
            }
            
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (SelectedObject != null)
            {
                SelectedObject = null;
            }
            Vector3 convertpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(convertpos.x, convertpos.y, -20);
        }
        if (Input.GetButtonDown("TimeSpeedUp"))
        {
            GameTime.IncreaseTime();
        }
        if (Input.GetButtonDown("TimeSpeedDown"))
        {
            GameTime.DecreaseTime();
        }
        if (Input.GetButtonDown("TimePause"))
        {
            if(GameTime.IsPaused())
            {
                GameTime.Resume();
            }
            else
            {
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

    void SetAllPermUpgrades()
    {
        foreach (SpaceObject obj in this.PlayerOwnedObjects)
        {
            obj.permUpgrades = this.PlayersPermUpgrade;
        }
    }
    void SetPermUpgrade(SpaceObject obj)
    {

        obj.permUpgrades = this.PlayersPermUpgrade;

    }
}
