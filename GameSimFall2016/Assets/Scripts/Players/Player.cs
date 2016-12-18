﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// The Player state definition.
public delegate void StateRunner();

// The basic functionality for all derived playable characters.
[RequireComponent(typeof(Collider))]
abstract public class Player : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
        FOREIGN,
    };

    private Transform myPlatform;
    private PhysicMaterial[] myPhysicMaterials;

    protected Dictionary<Enum, StateRunner> myStates { get; private set; }
    
    private Player myFollower;
    public Player follower
    {
        get { return myFollower; }
        set { AssignFollower(value); }
    }

    [Range(5, 90)]
    public float floorAngleLimit = 30;
    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;
    public float targetRange = 10.0f;
    public Enum playerState { get; set; }

    new public Camera camera { get { return PlayerManager.getInstance().camera; } }

    //------------------------------------------------------------------------------------------------
    private Transform myTransform;
    public new Transform transform
    {
        get
        {
            if (myTransform == null)
            {
                myTransform = base.transform;
            }
            return myTransform;
        }
    }

    //------------------------------------------------------------------------------------------------
    private Rigidbody myRigidbody;
    public new Rigidbody rigidbody
    {
        get
        {
            if (myRigidbody == null)
            {
                myRigidbody = GetComponent<Rigidbody>();
            }
            return myRigidbody;
        }
    }

    //------------------------------------------------------------------------------------------------
    private Collider myCollider;
    public new Collider collider
    {
        get
        {
            if (myCollider == null)
            {
                myCollider = GetComponent<Collider>();
            }
            return myCollider;
        }
    }

    //------------------------------------------------------------------------------------------------
    [HideInInspector]
    private NavMeshAgent myAgent;
    public NavMeshAgent agent
    {
        get
        {
            if (myAgent == null)
            {
                myAgent = GetComponent<NavMeshAgent>();
            }
            return myAgent;
        }
    }

    //------------------------------------------------------------------------------------------------
    //constructor
    public Player()
    {
        myStates = new Dictionary<Enum, StateRunner>();
        playerState = State.DEFAULT;
    }

    //------------------------------------------------------------------------------------------------
    void Awake()
    {
       myPhysicMaterials = new PhysicMaterial[2];
       
       myPhysicMaterials[0] = new PhysicMaterial("Friction Full");
       myPhysicMaterials[0].dynamicFriction = 1;
       myPhysicMaterials[0].staticFriction = 1;

       myPhysicMaterials[1] = new PhysicMaterial("Frictionless");
       myPhysicMaterials[1].dynamicFriction = 0.2f;
       myPhysicMaterials[1].staticFriction = 0.2f;
    }

    //------------------------------------------------------------------------------------------------
    // Sets another player to be this Player's follower
    // follower - the player to follow this player
    [HideInInspector]
    public void AssignFollower( Player follower = null )
    {
        myFollower = follower;
    }

    //------------------------------------------------------------------------------------------------
    // Update is called once per frame
    public void Update()
    {
        bool isPlayer = PlayerManager.getInstance().currentPlayer != this;
        agent.enabled = isPlayer;
        GetComponent<NavMeshObstacle>().enabled = !isPlayer;
        if (PlayerManager.getInstance().currentPlayer == this)
        {
            if ( !myStates.ContainsKey(playerState) )
            {
                throw new System.Exception("State [" + playerState + "] is not valid!");
            }
            else
            {
                myStates[playerState]();
            }
        }
        if (myFollower != null && myFollower != PlayerManager.getInstance().currentPlayer && follower.agent.isOnNavMesh)
        {
            Vector3 target = rigidbody.position - (transform.forward * 2);
            float distance = Vector3.Distance(transform.position, myFollower.transform.position);
            //target.y = myFollower.transform.position.y;
            //myFollower.rigidbody.MovePosition(Vector3.Lerp(myFollower.rigidbody.position, target, movementSpeed * Time.deltaTime));
            //if (distance > PlayerManager.getInstance().followDistance)
            myFollower.agent.destination = target;
            if (distance > PlayerManager.getInstance().switchDistance)
            {
               myFollower.agent.enabled = false;
               myFollower.transform.position = target;
            }
         //myFollower.smoothRotateTowards((rigidbody.position - myFollower.transform.position).normalized, rotationSmoothSpeed);
      }
    }

    //------------------------------------------------------------------------------------------------
    public void FixedUpdate()
    {
        if (transform.parent != null && myPlatform != null)
        {
            //transform.parent.position = myPlatform.position;
        }
    }

    //------------------------------------------------------------------------------------------------
    // Used to Set the current Foreign runnable for external state management
    public void addForeignRunnable( StateRunner stateRunner )
    {
        if (myStates.ContainsKey(State.FOREIGN))
        {
            myStates.Remove(State.FOREIGN);
        }
        myStates.Add(State.FOREIGN, stateRunner);
    }

    //------------------------------------------------------------------------------------------------
    // Removes the current foreign runnable
    public void clearForeignRunnable()
    {
        if (myStates.ContainsKey(State.FOREIGN))
        {
            myStates.Remove(State.FOREIGN);
        }
    }

    //------------------------------------------------------------------------------------------------
    // Adds a runnable state assigned to a specific runnable function.
    protected void addRunnable(Enum state, StateRunner stateRunner)
    {
        if (myStates.ContainsKey(state))
        {
            Debug.Log("Warning: Overriding state [" + state + "] for " + this.name + " !");
            myStates.Remove(state);
        }
        myStates.Add(state, stateRunner);
    }

    //------------------------------------------------------------------------------------------------
    // Sets the logic parent of this player.
    // hit - the result from a raycast to determine the parent.
    protected void setParent ( RaycastHit hit )
    {
        Transform parent = transform.parent;
        transform.parent = null;

        if ( parent == null )
        {
            parent = new GameObject("Parent").transform;
        }

        if (parent.parent == hit.transform)
            return;

        //align the player to a slope
        //RaycastHit fHit = new RaycastHit();
        //if (Physics.Raycast(collider.bounds.center + transform.forward / 2, Vector3.down, out fHit) &&
        //    fHit.point.y > hit.point.y)
        //{
        //   rigidbody.
        //}
        //if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        //{
        //    
        //    if ((fHit.point.y - collider.bounds.min.y) > 0.1f )
        //    {
        //        rigidbody.position = new Vector3(rigidbody.position.x, fHit.point.y + collider.bounds.extents.y, rigidbody.position.z);
        //    }
        //}
        parent.position = hit.transform.position;
        myPlatform = hit.transform;
        transform.parent = parent;
    }

    //------------------------------------------------------------------------------------------------
    // Clears the logic parent for this player.
    protected void clearParent()
    {
        if ( transform.parent != null )
        {
            transform.parent.parent = null;
        }
    }

    //------------------------------------------------------------------------------------------------
    // Moves this player in relation to the camera from User Inputs.
    protected void movePlayer()
    {
        Vector3 cameraFoward = camera.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            //            angle of joystick  + angle of camera
           float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;



            if ( !GetComponent<Animator>().hasRootMotion )
            {
               Vector3 moveTo = transform.forward * movementSpeed; //* Time.deltaTime;
               moveTo.y = rigidbody.velocity.y;
               rigidbody.velocity = moveTo;
               //rigidbody.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            // smooths the rotation transition
            smoothRotateTowards(0, angle, 0, Time.deltaTime * rotationSmoothSpeed);
            collider.material = myPhysicMaterials[1];
        }
        else
        {
           collider.material = myPhysicMaterials[0];
        }
    }

    //------------------------------------------------------------------------------------------------
    // Smoothly rotates this Player towards a given position.
    public void smoothRotateTowards(float x, float y, float z, float speed)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(x, y, z), speed);
    }

    //------------------------------------------------------------------------------------------------
    // Smoothly rotates this Player towards a given position.
    public void smoothRotateTowards(Vector3 forward, float speed)
    {
        //;
        //Vector3 angle = transform.localEulerAngles;
        //angle.x = Mathf.LerpAngle(angle.x, target.x, speed);
        //angle.y = Mathf.LerpAngle(angle.y, target.y, speed);
        //angle.z = Mathf.LerpAngle(angle.z, target.z, speed);
        transform.rotation = Quaternion.LookRotation(forward);
    }

    //------------------------------------------------------------------------------------------------
    // Conditions the players current grounded status.
    // steps - the size of the radius to check around this player.
    // Returns whether the player is currently grounded.
    protected bool isGrounded(int steps = 10)
    {
        RaycastHit hit;
        return isGrounded(out hit, steps);
    }

    //------------------------------------------------------------------------------------------------
    // Conditions the players current grounded status.
    // hit - the result of the raycast around this player
    // steps - the size of the radius to check around this player.
    // Returns whether the player is currently grounded.
    protected bool isGrounded(out RaycastHit hit, int steps = 10)
    {
        hit = new RaycastHit();
        float distance = collider.bounds.extents.y + 0.1f;
        float width = collider.bounds.size.x;
        float depth = collider.bounds.size.z;
        Ray ray = new Ray(Vector3.zero, Vector3.down);

        float x = collider.bounds.min.x;
        for (int xStep = 0; xStep <= steps; xStep++, x += width / steps)
        {
            float z = collider.bounds.min.z;
            for (int zStep = 0; zStep <= steps; zStep++, z += depth / steps)
            {
                ray.origin = new Vector3(x, collider.bounds.center.y, z);
                Debug.DrawRay(ray.origin, ray.direction * distance);
                if (Physics.Raycast(ray, out hit, distance) && Vector3.Angle(hit.normal, transform.up) < floorAngleLimit)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
