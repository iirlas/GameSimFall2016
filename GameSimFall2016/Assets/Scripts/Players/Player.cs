using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public delegate void StateRunner();

[RequireComponent(typeof(BoxCollider))]
abstract public class Player : MonoBehaviour
{
    public enum State
    {
        DEFAULT,
        FOREIGN,
    };
    private Transform myPlatform;
    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Collider myCollider;
    private Transform myFollowTarget;

    protected Dictionary<Enum, StateRunner> myStates { get; private set; }
    
    [Range(5, 90)]
    public float floorAngleLimit = 30;
    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;
    public float targetRange = 10.0f;
    public Enum playerState { get; set; }

    new public Camera camera { get { return PlayerManager.getInstance().camera; } }

    [HideInInspector]
    public Transform transform
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

    [HideInInspector]
    public void AssignTarget( Transform targetTo = null )
    {
        myFollowTarget = targetTo;
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
        else
        {
            if( myFollowTarget != null )
            {
                rigidbody.MovePosition( Vector3.Lerp( rigidbody.position, myFollowTarget.position - (myFollowTarget.forward), 
                                        movementSpeed * Time.deltaTime) );
            }
        }
        if (transform.parent != null && myPlatform != null)
        {
            transform.parent.position = myPlatform.position;
        }
    }

    public void addForeignRunnable( StateRunner stateRunner )
    {
        if (myStates.ContainsKey(State.FOREIGN))
        {
            myStates.Remove(State.FOREIGN);
        }
        myStates.Add(State.FOREIGN, stateRunner);
    }

    public void clearForeignRunnable ()
    {
        if (myStates.ContainsKey(State.FOREIGN))
        {
            myStates.Remove(State.FOREIGN);
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
        //if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        //{
        //    RaycastHit fHit = new RaycastHit();
        //    Physics.Raycast(collider.bounds.center + transform.forward / 2, Vector3.down, out fHit);
        //    if ((fHit.point.y - collider.bounds.min.y) > 0.1f )
        //    {
        //        rigidbody.position = new Vector3(rigidbody.position.x, fHit.point.y + collider.bounds.extents.y, rigidbody.position.z);
        //    }
        //}
        parent.position = hit.transform.position;
        myPlatform = hit.transform;
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

    protected bool isGrounded (int steps = 10)
    {
        RaycastHit hit;
        return isGrounded(out hit, steps);
    }

    protected bool isGrounded (out RaycastHit hit, int steps = 10)
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
