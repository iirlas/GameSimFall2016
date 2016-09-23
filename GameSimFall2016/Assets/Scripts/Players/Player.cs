using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

abstract public class Player : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
    };

    protected delegate void StateRunner();

    private Transform myPlatform;
    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    protected Dictionary<Enum, StateRunner> myStates { get; private set; }
    
    [Range(5, 90)]
    public float floorAngleLimit = 30;
    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;
    public float targetRange = 10.0f;
    public Enum playerState { get; protected set; }

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


    //constructor
    public Player()
    {
        myStates = new Dictionary<Enum, StateRunner>();
        playerState = State.DEFAULT;
    }

    // Update is called once per frame
    protected void Update()
    {
    }

    public void FixedUpdate()
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
            //            angle of joystick  + angle of camera
            float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;

            // smooths the rotation transition
            rigidbody.position += transform.forward * movementSpeed * Time.deltaTime;
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
        float distance = collider.bounds.size.y * 1.75f;
        Vector3 halfExtents = new Vector3(collider.bounds.extents.x, 0.01f, collider.bounds.extents.z);
        hit = new RaycastHit();


        // Are we level with the ground?
        if (Physics.BoxCast(transform.position, halfExtents, Vector3.down, out hit, transform.rotation, distance))
        {
           //Debug.DrawLine(transform.position, hit.point);
           return true;
        }
        return false;
    }


}
