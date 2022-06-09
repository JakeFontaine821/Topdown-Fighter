using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    /******************************/
    /* GENERAL MOVEMENT VARIABLES */
    /******************************/
    [Header("General Movement")]
    NavMeshAgent agent;
    [SerializeField] float moveSpeed;
    RaycastHit hit;
    Ray ray;
    Vector3 rayPosition;
    //VECTOR THREE FOR TRACKING WHERE MOUSE IS FACING, NOT PLAYER
    Vector3 faceTowardsMouse;

    /*************/
    /* DASH MECH */
    /*************/
    [Header("Dashing Mech")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashCooldown;
    float dashCDCounter;

    /***************/
    /* ABILITY ONE */
    /***************/
    [Header("Ability One")]
    [SerializeField] GameObject A1Model;
    [SerializeField] float A1Speed;
    [SerializeField] float A1Cooldown;
    float A1CDCounter;

    /***************/
    /* ABILITY TWO */
    /***************/
    [Header("Ability Two")]
    [SerializeField] GameObject A2Model;
    [SerializeField] float A2Speed;
    [SerializeField] float A2Cooldown;
    [SerializeField] float A2AttackRange;
    float A2CDCounter;

    void Start()
    {
        //SET NAV MESH AGENT
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;       
    }
    
    void Update()
    {
        /*********************/
        /* GET MOUSE RAYCAST */
        /*********************/
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            rayPosition = hit.point;
        }

        /********************/
        /* GENERAL MOVEMENT */
        /********************/
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.LookAt(new Vector3(rayPosition.x, gameObject.transform.position.y, rayPosition.z));
            agent.SetDestination(rayPosition);
        }

        /******************************************/
        /* TRACK WERE MOUSE IS RELATING TO PLAYER */
        /******************************************/
        faceTowardsMouse = gameObject.transform.position - rayPosition;
        faceTowardsMouse.Normalize();

        /*************/
        /* DASH MECH */
        /*************/
        Dash();

        /********************/
        /* ABILITY ONE MECH */
        /********************/
        AbilityOne();

        /********************/
        /* ABILITY TWO MECH */
        /********************/
        AbilityTwo();
    }

    void Dash()
    {
        //DASH IS OFF COOLDOWN AND KEY IS PRESSED
        if (Input.GetKeyDown(KeyCode.Space) && dashCDCounter < 0)
        {
            //SET VELOCITY TO DASH DIRECTION AND SPEED
            this.GetComponent<Rigidbody>().velocity = -faceTowardsMouse * dashSpeed;

            //PUT DASH ON COOLDOWN
            dashCDCounter = dashCooldown;
        }

        //DASH COOLDOWN TIMER
        if(dashCDCounter >= 0)
        {
            dashCDCounter -= Time.deltaTime;
        }
    }

    void AbilityOne()
    {
        //ABILITY IS OFF COOLDOWN AND KEY IS PRESSED
        if(Input.GetKeyDown(KeyCode.Q) && A1CDCounter < 0)
        {
            //MAKE NEW ABILITY ONE OBJECT
            GameObject A1 = Instantiate(A1Model, transform.position, transform.rotation);

            //REDO MODEL ROTATION CAUSE IDK HOW TO MAKE IT WORK IN INSTANTIATE FUNCTION
            A1.transform.LookAt(new Vector3(rayPosition.x, gameObject.transform.position.y, rayPosition.z));

            //SET VELOCITY ON OBJECT
            A1.GetComponent<Rigidbody>().velocity = new Vector3(-faceTowardsMouse.x,
                0, -faceTowardsMouse.z) * A1Speed;

            //PUT ABILITY ON COOLDOWN
            A1CDCounter = A1Cooldown;
        }

        //ABILITY ONE COOLDOWN TIMER
        if(A1CDCounter >= 0)
        {
            A1CDCounter -= Time.deltaTime;
        }
    }

    void AbilityTwo()
    {
        if(Input.GetKeyDown(KeyCode.W) && A2CDCounter < 0 &&
            Vector3.Distance(transform.position, rayPosition) <= A2AttackRange)
        {
            //MAKE NEW ABILITY TWO OBJECT IN SKY
            GameObject A2 = Instantiate(A2Model, new Vector3(rayPosition.x, 10, rayPosition.z), new Quaternion());

            //SET ITS VELOCITY TO DOWN
            A2.GetComponent<Rigidbody>().velocity = Vector3.down * A2Speed;

            //SET COOLDOWN
            A2CDCounter = A2Cooldown;
        }

        //ABILITY TWO COOLDOWN TIMER
        if(A2CDCounter >= 0)
        {
            A2CDCounter -= Time.deltaTime;
        }
    }
}
