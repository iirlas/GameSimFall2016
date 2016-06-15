using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float rotationSmoothSpeed = 10.0f;

    new public Camera camera;

    [HideInInspector]
    public new Transform transform;
    [HideInInspector]
    public new Rigidbody rigidbody;

    private float mySmoothAngle;

	// Use this for initialization
	protected virtual void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
        Vector3 cameraFoward = camera.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if ( h != 0 || v != 0 )
        {
            //            angle of joystick    angle of camera   
            float angle = (Mathf.Atan2(h, v) + Mathf.Atan2(cameraFoward.x, cameraFoward.z)) * Mathf.Rad2Deg;

            // smooths the rotation transition
            transform.position += transform.forward * movementSpeed * Time.deltaTime;
            mySmoothAngle = Mathf.LerpAngle(mySmoothAngle, angle, Time.deltaTime * rotationSmoothSpeed);
            transform.localEulerAngles = new Vector3(0, mySmoothAngle, 0);
        }
	}
}
