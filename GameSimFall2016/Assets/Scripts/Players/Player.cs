using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

abstract public class Player : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
        FALL,
        ATTACK,
        ACTION
    };

    protected delegate void StateRunner();

    private Transform myPlatform;
    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Collider myCollider;
    private Collider[] myTargets;
    private Transform myTarget;
    private int myTargetableLayerMask = 1 << 9;
    private bool isTargeting = false;

    [Range(5, 45)]
    public float floorAngleLimit = 30;
    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;
    public float targetRange = 10.0f;

    new public Camera camera { get { return PlayerManager.getInstance().camera; } }

    [HideInInspector]
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

    [HideInInspector]
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

    [HideInInspector]
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

    [HideInInspector]
    public Transform target
    {
       get { return myTarget;  }
    }

    public Enum playerState { get; protected set; }

    private Dictionary<Enum, StateRunner> myStates;

    //constructor
    public Player()
    {
        myStates = new Dictionary<Enum, StateRunner>();
        addRunnable(State.DEFAULT, runMoveState);
        addRunnable(State.FALL, runFallingState);
        playerState = State.DEFAULT;
    }

    // Update is called once per frame
    protected void Update()
    {
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
    }

    public void FixedUpdate()
    {
        if (transform.parent != null && myPlatform != null)
        {
            transform.parent.position = Vector3.Lerp(transform.parent.position, myPlatform.position, float.PositiveInfinity);
        }
    }

    protected void addRunnable(Enum state, StateRunner stateRunner)
    {
        if (myStates.ContainsKey(state))
        {
            Debug.Log("Warning: Overriding state [" + state + "] for " + this.name + " !");
            myStates.Remove(state);
        }
        myStates.Add(state, stateRunner);
    }
    
    virtual protected void runMoveState ()
    {
        RaycastHit hit;
        if ( rigidbody.useGravity )
        {
            if ( isGrounded(out hit) )
            {
                setParent(hit, floorAngleLimit);
                movePlayer();
            }
            else
            {
                playerState = State.FALL;
            }
        }

        if ( !playerState.Equals(State.DEFAULT) )
        {
            return;
        }

        if (Input.GetButtonDown("Attack") && myStates.ContainsKey(State.ATTACK))
        {
            playerState = State.ATTACK;
        }
        else if (Input.GetButtonDown("Action") && myStates.ContainsKey(State.ACTION))
        {
            playerState = State.ACTION;
        }
        else if (Input.GetButtonDown("Center"))
        {
           isTargeting = !isTargeting;
        }
        if (isTargeting)
        {
           if (!findTargets())
           {
              isTargeting = false;
           }
        }
        else
        {
           myTarget = null;
        }
    }

    virtual protected void runFallingState()
    {
        if (isGrounded())
        {
            playerState = Player.State.DEFAULT;
        }
        else
        {
            clearParent();
        }
    }

    protected void setParent ( RaycastHit hit, float angleLimit = float.PositiveInfinity )
    {

        Transform parent = transform.parent;
        if ( parent == null )
        {
            parent = new GameObject("Parent").transform;
        }

        if (parent.parent == hit.transform)
            return;

        transform.parent = null;

        if (Vector3.Angle(hit.normal, transform.up) < angleLimit )
        {
            parent.position = hit.transform.position;
            parent.up = hit.normal;
            myPlatform = hit.transform;
        }
        transform.parent = parent;
    }

    protected void clearParent ()
    {
        if ( transform.parent != null )
        {
            transform.parent.parent = null;
        }
    }

    protected void movePlayer()
    {
        Vector3 cameraFoward = camera.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            if ( myTarget != null )
            {
                //rotate towards the shooting target
                Vector3 direction = myTarget.position - rigidbody.position;
                direction.y = 0;//aligns facing direction with the ground.
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
                rigidbody.position += ((camera.transform.forward * v) +
                                       (camera.transform.right * h)) *
                                        movementSpeed * Time.deltaTime;
            }
            else
            {
               //            angle of joystick  + angle of camera
               float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;

               // smooths the rotation transition
               transform.position += transform.forward * movementSpeed * Time.deltaTime;
               smoothRotateTowards(0, angle, 0, Time.deltaTime * rotationSmoothSpeed);
            }
        }
    }

    void strafe()
    {
       float h = Input.GetAxis("Horizontal");
       float v = Input.GetAxis("Vertical");

       if (h != 0 || v != 0)
       {
       }
    }

    public void smoothRotateTowards(float x, float y, float z, float speed)
    {
        Vector3 angle = transform.localEulerAngles;
        angle.x = Mathf.LerpAngle(angle.x, x, speed);
        angle.y = Mathf.LerpAngle(angle.y, y, speed);
        angle.z = Mathf.LerpAngle(angle.z, z, speed);
        transform.localEulerAngles = angle;
    }

    public void smoothRotateTowards(Vector3 target, float speed)
    {
        Vector3 angle = transform.localEulerAngles;
        angle.x = Mathf.LerpAngle(angle.x, target.x, speed);
        angle.y = Mathf.LerpAngle(angle.y, target.y, speed);
        angle.z = Mathf.LerpAngle(angle.z, target.z, speed);
        transform.localEulerAngles = angle;
    }

    protected bool isGrounded ( int steps = 10 )
    {
        RaycastHit hit;
        return isGrounded(out hit, steps);
    }

    protected bool isGrounded(out RaycastHit hit, int steps = 10)
    {
        float distance = collider.bounds.size.y * 0.75f;
        float width = collider.bounds.size.x;
        float depth = collider.bounds.size.z;
        Vector3 origin = collider.bounds.min;

        hit = new RaycastHit();

        // Are we level with the ground?
        for (int x = 0; x <= steps; x++)
        {
            for (int z = 0; z <= steps; z++)
            {
                origin.x = collider.bounds.min.x + ((x / (float)steps) * width);
                origin.y = transform.position.y;
                origin.z = collider.bounds.min.z + ((z / (float)steps) * depth);
                Debug.DrawRay(origin, (-transform.up) * distance);
                if (Physics.Raycast(origin, (-transform.up), out hit, distance))
                {
                    Debug.DrawLine(origin, hit.point);
                    return true;
                }
            }
        }
        return false;
    }

    bool findTargets()
    {
       myTargets = Physics.OverlapSphere(rigidbody.position, targetRange, myTargetableLayerMask);

       if (myTarget == null || Vector3.Distance(rigidbody.position, myTarget.position) > targetRange)
       {
          myTarget = (myTargets.Length != 0 ? myTargets[0].transform : null);

          for (int index = 0; index < myTargets.Length; index++)
          {
             float currentAngle = Vector3.Angle(camera.transform.forward, myTarget.position - rigidbody.position);
             float angle = Vector3.Angle(camera.transform.forward, myTargets[index].transform.position - rigidbody.position);
             if (angle < currentAngle)
             {
                myTarget = myTargets[index].transform;
             }
          }
       }
       return (myTarget != null);
    }
}
