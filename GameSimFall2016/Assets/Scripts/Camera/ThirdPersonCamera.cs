using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public float speed = 2.5f;

    public Vector3 pivotPoint = new Vector3( 0, 0, 0 );
    public Vector3 offset = new Vector3( 0, 1, -5 );

    [HideInInspector]
    public new Transform transform;

    private Player myPlayer;
    private Vector3 myAngle = Vector3.zero;
    //private float  myAngle = 0;

	// Use this for initialization
	void Awake () {
        transform = GetComponent<Transform>();
	}

    void Start()
    {
        myAngle = PlayerManager.getInstance().currentPlayer.transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (myPlayer != PlayerManager.getInstance().currentPlayer)
        {
            myPlayer = PlayerManager.getInstance().currentPlayer;
        }
	}

    public void LateUpdate()
    {
        if (myPlayer == null)
            return;

        float horizontal = Input.GetAxis("Alt_Horizontal");
        float vertical = Input.GetAxis("Alt_Vertical");
        
        //calculate pivot point
        //based on edge of the player's collider.


        if (myPlayer is Girl && (myPlayer as Girl).target != null)
        {
            myAngle = Vector3.Lerp(transform.eulerAngles, myPlayer.transform.eulerAngles, speed * Time.deltaTime);
            myAngle.z = 0;
        }
        else if (Input.GetButtonDown("Center"))
        {
            myAngle = new Vector3(0, myPlayer.transform.eulerAngles.y, 0);
        }
        else if (horizontal != 0 || vertical != 0)
        {
            myAngle += new Vector3(-vertical, horizontal, 0) * (speed * Time.deltaTime);
        }

        myAngle.x = Mathf.Clamp(myAngle.x, -45, 45);

        Quaternion rotation = Quaternion.Euler(myAngle);
        Vector3 targetPosition = myPlayer.transform.position + rotation * pivotPoint;
        Vector3 viewPosition = targetPosition + rotation * offset;
        Vector3 collisionOffset = rotation * (myPlayer.collider.bounds.extents / 2);
        
        RaycastHit hit;
        if (Physics.Linecast(myPlayer.transform.position, viewPosition - collisionOffset, out hit))
        {
            viewPosition = hit.point + collisionOffset;
        }
        transform.position = viewPosition;

        transform.LookAt(myPlayer.transform.position, Vector3.up);
    }
}
