﻿using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public float speed = 2.5f;

    public Vector3 pivotPoint = new Vector3( 0, 1 );
    public Vector3 offset = new Vector3( 0, 1, -5 );

    [HideInInspector]
    public new Transform transform;

    private Player myPlayer;
    private Vector3 myAngle = Vector3.zero;
    //private float  myAngle = 0;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerManager.getInstance().currentPlayer)
            myPlayer = PlayerManager.getInstance().currentPlayer;
	}

    public void LateUpdate()
    {
        if (!myPlayer)
            return;

        float h = Input.GetAxis("Alt_Horizontal");
        float v = Input.GetAxis("Alt_Vertical");
        
        if (myPlayer is Girl && (myPlayer as Girl).target != null)
        {
            myAngle.y = Mathf.LerpAngle(transform.eulerAngles.y, myPlayer.transform.eulerAngles.y, speed * Time.deltaTime);
        }
        else if (Input.GetButtonDown("Center"))
        {
            myAngle.y = myPlayer.transform.eulerAngles.y;
        }
        else if (h != 0)
        {
            myAngle.y += h * speed;
        }
        Vector3 targetPosition = myPlayer.transform.position + pivotPoint;
        Vector3 viewPosition = targetPosition + Quaternion.Euler(0, myAngle.y, 0) * offset;

        RaycastHit hit;
        if (Physics.Linecast(targetPosition, viewPosition, out hit))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = viewPosition;
        }

        transform.LookAt(targetPosition, Vector3.up);
    }
}
