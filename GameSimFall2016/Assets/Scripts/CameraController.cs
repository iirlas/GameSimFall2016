using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [HideInInspector]
    public Transform target;
    public Transform parent;

    public float speed = 2.5f;

    public Vector3 pivotPoint;
    public Vector3 freeRoamLocalPosition;
    public Vector3 FixedLocalPosition;


    [HideInInspector]
    public new Transform transform;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Game.getInstance().currentPlayer)
            target = Game.getInstance().currentPlayer.transform;
	}

    public void LateUpdate()
    {
        if (!Game.getInstance().currentPlayer)
            return;

        if ( Game.getInstance().state == Game.State.FREEROAM )
        {
            float h = Input.GetAxis("Alt_Horizontal");
            float v = Input.GetAxis("Alt_Vertical");

            transform.localPosition = freeRoamLocalPosition;

            if ( h != 0 || v != 0 )
            {
                // unify speed between mouse and joystick
                h = (h > 0 ? 1 : -1) * speed;
                v = (v > 0 ? 1 : -1) * speed;

                parent.eulerAngles += new Vector3(0, h, 0);
            }
        }
        else if ( Game.getInstance().state == Game.State.PUZZLE )
        {
            transform.localPosition = FixedLocalPosition;
            parent.eulerAngles = Vector3.zero;
        }


        parent.position = target.position + pivotPoint;
        transform.LookAt(target.position + pivotPoint, Vector3.up);
    }

}
