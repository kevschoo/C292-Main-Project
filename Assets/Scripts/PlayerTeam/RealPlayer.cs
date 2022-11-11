using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class RealPlayer : Player
{
    [field: SerializeField] public override string PlayerName { get; set; } = "The Playa";
    [field: SerializeField] public override PermanentAttributes PlayerPermAttributes { get; set; }
    [field: SerializeField] public override PlayerKnownEntities PKEntities { get; set; }
    [field: SerializeField] public override PlayerKnownUprades PKUpgrades { get; set; }

    [field: SerializeField] public override BaseBonus baseBonus { get; set; }
    [field: SerializeField] public override GameObject PlayerBase { get; set; }
    //[SerializeField] public abstract List<SpaceObject> PlayerOwnedObjects { get; set; }

    //[SerializeField] public abstract List<> PlayerAvaliableShips { get; set; }
    [SerializeField] public override List<EntityAI> PlayerEntities { get; set; }
    [field: SerializeField] public override int Index { get; set; }
    [field: SerializeField] public override float Money { get; set; }
    [field: SerializeField] public override SpecialAbility SelectedAbility { get; set; }
    [field: SerializeField] public override bool CanActivateSelectedAbility { get; set; }

    [field: SerializeField] List<GameObject> OldObjects = new List<GameObject>();
    [field: SerializeField] List<GameObject> NewObjects = new List<GameObject>();
    [field: SerializeField] public override GameObject SelectedObject { get; set; }

    [field: SerializeField] float EdgeSize = 30;
    [SerializeField] Vector3 CameraNextPos;
    [field: SerializeField] public bool IsMainPlayer { get; set; } = true;


    Camera mainCam;
    Ray2D ray;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        EntitySelectEvent.SelectionCleared += ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged += SetSelectedEntity;
        CameraNextPos = mainCam.transform.position;
    }
    private void OnDestroy()
    {
        EntitySelectEvent.SelectionCleared -= ClearSelectedEntity;
        EntitySelectEvent.SelectionChanged -= SetSelectedEntity;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerBase == null)
        { WaveEvent.InvokeGameEnd(this, false);}

        if(IsMainPlayer)
        {
            CameraController();
            CameraZoom();

            if (Input.GetButtonDown("LeftClick"))
            {
                Vector2 raycastPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                new Ray(raycastPos, Vector2.zero);
                RaycastHit2D[] hit = Physics2D.RaycastAll(raycastPos, Vector2.zero);

                if (hit != null)
                {
                    foreach (RaycastHit2D hits in hit)
                    {
                        NewObjects.Add(hits.collider.gameObject);
                    }
                    SendNewObject();
                }
            }
            if (Input.GetButtonDown("RightClick"))
            {
                Vector2 raycastPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("rightclicker");
                if (CanActivateSelectedAbility)
                {
                    Debug.Log("ability used");
                    if (SelectedAbility != null)
                    {
                        SelectedAbility.Activate(this, this.mainCam.ScreenToWorldPoint(Input.mousePosition));
                    }
                }

            }
            if (Input.GetButtonDown("MiddleClick"))
            {
                //Debug.Log("middler");
                if (SelectedObject != null)
                {
                    CameraNextPos = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20);
                    SelectedObject = null;
                }
                EntitySelectEvent.InvokeSelectionCleared();
            }

            if (Input.GetButtonDown("Escape"))
            {
                Debug.Log("Player calling pause menu");
                EntitySelectEvent.InvokeSelectionCleared();

                if (!GameTime.IsPaused())
                {
                    TimeEffectEvent.InvokeEventStart(Time.timeScale);
                    GameTime.Pause();
                }
                else
                {
                    TimeEffectEvent.InvokeEventEnd(Time.timeScale);
                    GameTime.Resume();
                }
            }
            if (Input.GetButtonDown("TimeSpeedUp"))
            { GameTime.IncreaseTime(); }
            if (Input.GetButtonDown("TimeSpeedDown"))
            { GameTime.DecreaseTime(); }
            if (Input.GetButtonDown("TimePause"))
            {
                if (GameTime.IsPaused())
                { GameTime.Resume(); }
                else
                { GameTime.Pause(); }
            }
        }
        


    }

    void CameraController()
    {
        if (SelectedObject != null)
        {
            //Camera.main.transform.position = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20);
            Vector3 SmoothPos = Vector3.Lerp(mainCam.transform.position, new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, -20), .05f);
            Camera.main.transform.position = SmoothPos;
        }
        else
        {
            if (Input.mousePosition.x > Screen.width - EdgeSize)
            {
                CameraNextPos.x += 10f * Time.deltaTime;
            }
            if (Input.mousePosition.x < EdgeSize)
            {
                CameraNextPos.x -= 10f * Time.deltaTime;
            }
            if (Input.mousePosition.y > Screen.height - EdgeSize)
            {
                CameraNextPos.y += 10f * Time.deltaTime;
            }
            if (Input.mousePosition.y < EdgeSize)
            {
                CameraNextPos.y -= 10f * Time.deltaTime;
            }
            mainCam.transform.position = CameraNextPos;
        }
    }
    float minSize = 1f;
    float maxSize = 25f;
    void CameraZoom()
    {
        if (Input.GetButton("Shift"))
        {
       
            float CameraSize = Camera.main.orthographicSize;
            CameraSize += Input.GetAxis("Mouse ScrollWheel") * -10f;
            CameraSize = Mathf.Clamp(CameraSize, minSize, maxSize);
            mainCam.orthographicSize = CameraSize;
        }
    }
    void SendNewObject()
    {
        //set lastobjects to savedlastobjects
        //List of new objects hit > CurrentObjects
        //List of old objects hit > LastObjects
        //if obj is in LastObjects ignore
        //go to next obj in current and send
        //clear lastObjects of any objects not in current and set to saved
        List<GameObject> RemoveTheseObjects = new List<GameObject>();
        List<GameObject> RemoveTheseObjectsFromOld = new List<GameObject>();

        //clear oldobjects of any objects that left the stack
        foreach (GameObject go in OldObjects)
        {
            if (!NewObjects.Contains(go))
            {
                RemoveTheseObjectsFromOld.Add(go);
            }
        }
        foreach (GameObject go in RemoveTheseObjectsFromOld)
        {
            OldObjects.Remove(go);
        }

        //remove blacklist objects that are already seen in stack
        foreach (GameObject go in NewObjects)
        {
            if (OldObjects.Contains(go))
            {
                RemoveTheseObjects.Add(go);
            }
        }
        foreach (GameObject go in RemoveTheseObjects)
        {
            NewObjects.Remove(go);
        }
        //if we manage to remove all items from newobjects, that means we should loop back to the first item
        // do that by clearing the blacklist and repopulating it through clicks again
        if (NewObjects.Count == 0)
        {
            if (RemoveTheseObjects.Count > 0)
            {
                EntitySelectEvent.InvokeSelectionChanged(RemoveTheseObjects[0], this);
                //Debug.Log("sent" + RemoveTheseObjects[0].name);
            }
            //Debug.Log("Clearing");
            OldObjects.Clear();
        }
        else
        {
            OldObjects.Add(NewObjects[0]);
            if(NewObjects[0] != null)
            {
                EntitySelectEvent.InvokeSelectionChanged(NewObjects[0], this);
            }

            //Debug.Log("sent" + NewObjects[0]);
        }
    }


    void SetSelectedEntity(object sender, EntityEventArgs args)
    {
        SelectedObject = args.SelectedEntity;

    }

    void ClearSelectedEntity(object sender, EventArgs args)
    {
        SelectedObject = null;
    }


}