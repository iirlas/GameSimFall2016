using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float speed = 2.5f;

    public Vector3 pivotPoint;
    public Vector3 freeRoamLocalPosition;
    public Vector3 fixedLocalPosition;


    [HideInInspector]
    public new Transform transform;

    private Transform myTarget;
    private Vector3   myViewPosition;
    private float     myAngle = 0;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Game.getInstance().currentPlayer)
            myTarget = Game.getInstance().currentPlayer.transform;
	}

    public void LateUpdate()
    {
        if (!myTarget)
            return;

        if ( Game.getInstance().state == Game.State.FREEROAM )
        {
            float h = Input.GetAxis("Alt_Horizontal");
            //float v = Input.GetAxis("Alt_Vertical");

            myViewPosition = freeRoamLocalPosition;

            if ( h != 0 )
            {
                // unify speed between mouse and joystick
                h = (h > 0 ? 1 : -1) * speed;
            }
            myAngle += h;

            if (Input.GetButtonDown("Center"))
            {
                myAngle = myTarget.eulerAngles.y;
            }
        }
        else if (Game.getInstance().state == Game.State.PUZZLE)
        {
            myViewPosition = fixedLocalPosition;
            myAngle = 0;
        }
        else if (Game.getInstance().state == Game.State.STRAFE)
        {
            myViewPosition = freeRoamLocalPosition;
            myAngle = Mathf.LerpAngle(transform.eulerAngles.y, myTarget.eulerAngles.y, 10.0f * Time.deltaTime);
        }

        myViewPosition = Quaternion.Euler(0, myAngle, 0) * myViewPosition + myTarget.position;

        transform.position = myViewPosition;//

        transform.LookAt(myTarget.position + pivotPoint, Vector3.up);
    }

}
