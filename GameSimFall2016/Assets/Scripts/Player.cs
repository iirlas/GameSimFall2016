using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    protected delegate void StateRunner();

    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;


    new public Camera camera;

    [HideInInspector]
    public new Transform transform;
    [HideInInspector]
    public new Rigidbody rigidbody;

    public Enum playerState;

    private float mySmoothAngle;
    private Dictionary<Enum, StateRunner> myStates;

	// Use this for initialization
	protected virtual void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        myStates = new Dictionary<Enum, StateRunner>();

	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
        myStates[playerState]();
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
            mySmoothAngle = Mathf.LerpAngle(mySmoothAngle, angle, Time.deltaTime * rotationSmoothSpeed);
            transform.localEulerAngles = new Vector3(0, mySmoothAngle, 0);
        }
    }
}
