using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

abstract public class Player : MonoBehaviour
{
    public enum State
    {
        MOVE,
        FALL,
        ATTACK,
        ACTION
    };

    protected delegate void StateRunner();

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Collider myCollider;


    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;

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

    public Enum playerState { get; protected set; }

    private Dictionary<Enum, StateRunner> myStates;

    //constructor
    public Player()
    {
        myStates = new Dictionary<Enum, StateRunner>();
        addRunnable(State.MOVE, runMoveState);
        addRunnable(State.FALL, runFallingState);
        playerState = State.MOVE;
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
                setParent(hit, 45);
                movePlayer();
            }
            else
            {
                playerState = State.FALL;
            }
        }

        if ( !playerState.Equals(State.MOVE) )
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

    }

    virtual protected void runFallingState()
    {
        if (isGrounded())
        {
            playerState = Player.State.MOVE;
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
            parent.parent = hit.transform;
            parent.localEulerAngles = Vector3.zero;
            parent.localScale = hit.transform.localScale.Inverse();
            //transform.up = hit.normal;// new Vector3(hit.transform.up.x, transform.up.y, hit.transform.up.z);
        }
        transform.parent = parent;
    }

    protected void clearParent ()
    {
        if ( transform.parent != null && transform.parent.parent != null )
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
            //            angle of joystick  + angle of camera
            float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;

            // smooths the rotation transition
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            smoothRotateTowards(0, angle, 0, Time.deltaTime * rotationSmoothSpeed);
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
}
