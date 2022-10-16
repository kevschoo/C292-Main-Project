using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerShipAi : MonoBehaviour
{
    NavMeshAgent Agent;
    [SerializeField] GameObject Target;
    private PlayerControls playerControls;

    SpeedStat speedStat;

    public float RotationSpeed = 15;

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

   
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<SpeedStat>() != null)
            this.speedStat = gameObject.GetComponent<SpeedStat>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.speed = speedStat._Speed;

    }

    // Update is called once per frame
    void Update()
    {
        Agent.speed = speedStat._Speed;
        Agent.SetDestination(Target.transform.position);

        float leftclick = playerControls.BaseControls.LeftClick.ReadValue<float>();

        if(leftclick > .5f)
        {
            //Debug.Log("Set Target to" + Target.transform.position);
            Target.transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            Target.transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, 0);
        }
        if(this.gameObject.transform.position.z != 0)
        {
            this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }

        Vector2 direction = Target.transform.position - this.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion Rot = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rot, RotationSpeed * Time.deltaTime);
    }




}
