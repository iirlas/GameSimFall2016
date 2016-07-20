using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

abstract public class Player : MonoBehaviour {

    protected delegate void StateRunner();

    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;


    new public Camera camera;

    [HideInInspector]
    public new Transform transform;
    [HideInInspector]
    public new Rigidbody rigidbody;
    [HideInInspector]
    public new Collider collider;

    public Enum playerState;
    
    private Dictionary<Enum, StateRunner> myStates;

	// Use this for initialization
	protected virtual void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        myStates = new Dictionary<Enum, StateRunner>();
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
        if ( Game.getInstance().currentPlayer == this )
        {
            myStates[playerState]();
        }
	}

    protected void addRunnable ( Enum state, StateRunner stateRunner )
    {
        myStates.Add(state, stateRunner);
    }

    protected void movePlayer ()
    {
        Vector3 cameraFoward = camera.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if ( h != 0 || v != 0 )
        {
            //            angle of joystick  + angle of camera
            float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;

            // smooths the rotation transition
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            smoothRotateTowards( 0, angle, 0, Time.deltaTime * rotationSmoothSpeed );
        }
    }

    public void smoothRotateTowards ( float x, float y, float z, float speed )
    {
        Vector3 angle = transform.localEulerAngles;
        angle.x = Mathf.LerpAngle(angle.x, x, speed);
        angle.y = Mathf.LerpAngle(angle.y, y, speed);
        angle.z = Mathf.LerpAngle(angle.z, z, speed);
        transform.localEulerAngles = angle;
    }

    public void smoothRotateTowards( Vector3 target, float speed)
    {
        Vector3 angle = transform.localEulerAngles;
        angle.x = Mathf.LerpAngle(angle.x, target.x, speed);
        angle.y = Mathf.LerpAngle(angle.y, target.y, speed);
        angle.z = Mathf.LerpAngle(angle.z, target.z, speed);
        transform.localEulerAngles = angle;
    }

    protected bool isGrounded(float? distance = null, int step = 10)
    {
        if (distance == null)
        {
            distance = collider.bounds.extents.y * 1.25f;
        }

        float width = collider.bounds.size.x;
        float depth = collider.bounds.size.z;

        float xSteps = width / step;
        float zSteps = depth / step;

        // Are we level with the ground?
        for (float x = 0; Mathf.Round(x) < width; x += xSteps )
        {
            for (float z = 0; Mathf.Round(z) < depth; z += zSteps )
            {
                Vector3 origin = collider.bounds.min + (Vector3.right * x) + (Vector3.forward * z);
                origin.y = transform.position.y;

                Debug.DrawRay(origin, Vector3.down * (float)distance );
                if (Physics.Raycast(origin, Vector3.down, (float)distance))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
