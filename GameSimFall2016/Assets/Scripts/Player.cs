using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

abstract public class Player : MonoBehaviour
{

    protected delegate void StateRunner();

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;

    new public Camera camera;

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
    }

    // Update is called once per frame
    protected void Update()
    {
        if (PlayerManager.getInstance().currentPlayer == this)
        {
            myStates[playerState]();
        }
    }

    protected void addRunnable(Enum state, StateRunner stateRunner)
    {
        myStates.Add(state, stateRunner);
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

    protected bool isGrounded(float sizeOffset = 0, int step = 10, float? distance = null)
    {
        if (distance == null)
        {
            distance = collider.bounds.size.y * 0.75f;
        }

        float width = collider.bounds.size.x + sizeOffset;
        float depth = collider.bounds.size.z + sizeOffset;
        float xSteps = width / step;
        float zSteps = depth / step;

        Debug.DrawLine(collider.bounds.max, collider.bounds.min);

        Vector3 origin = collider.bounds.min;

        // Are we level with the ground?
        for (int x = 0; x <= step; x++)
        {
            for (int z = 0; z <= step; z++)
            {
                origin = new Vector3(collider.bounds.min.x + (x / (float)step), transform.position.y, collider.bounds.min.z + (z / (float)step));
                Debug.DrawRay(origin, Vector3.down * (float)distance);
                RaycastHit hit;
                if (Physics.Raycast(origin, Vector3.down, out hit, (float)distance))
                {
                    if ( transform.parent == null )
                    {
                        GameObject node = new GameObject("Player Parent");
                        node.transform.parent = hit.transform;
                        transform.parent = node.transform;
                    }
                    else if (transform.parent.parent != hit.transform)
                    {
                        transform.parent.parent = hit.transform;
                    }
                    return true;
                }
            }
        }
        if (transform.parent != null)
        {
            transform.parent.parent = null;
        }
        return false;
    }
}
