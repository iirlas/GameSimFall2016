using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    public float speed = 10f;

    public Vector3 pivotPoint = new Vector3( 0, 0, 0 );
    public Vector3 offset = new Vector3( 0, 1, -5 );

    public LayerMask layerMask = 0x1; //default layer

    [HideInInspector]
    public new Transform transform;

    private Player myPlayer;
    private Vector3 myAngle = Vector3.zero;

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

        Girl kira = (myPlayer as Girl);
        if (kira != null && kira.target != null)
        {
            Quaternion rotation = myPlayer.transform.rotation;
            transform.position = kira.transform.position + rotation * (pivotPoint) - 
                                 (transform.forward * 2);

            transform.LookAt(kira.target.position, Vector3.up);
            //myAngle.y = Mathf.LerpAngle(transform.eulerAngles.y, myPlayer.transform.eulerAngles.y,speed * Time.deltaTime );
            //myAngle = Vector3.Lerp(transform.eulerAngles, myPlayer.transform.eulerAngles, );
            //myAngle.x = 0;
            //myAngle.z = 0;
        }
        else
        {
            if (Input.GetButtonDown("Center"))
            {
                myAngle = new Vector3(0, myPlayer.transform.eulerAngles.y, 0);
            }
            else if (horizontal != 0 || vertical != 0)
            {
                myAngle += new Vector3(-vertical, horizontal, 0) * (speed * Time.deltaTime);
            }
            //limit pitch rotation
            myAngle.x = Mathf.Clamp(myAngle.x, -45, 45);

            //rotation around the player
            Quaternion rotation = Quaternion.Euler(myAngle);

            //player position + pivotPoint
            Vector3 targetPosition = myPlayer.transform.position + pivotPoint;
            Vector3 viewPosition = targetPosition + rotation * (offset);
            Vector3 lineCastStartPosition = Quaternion.LookRotation( myPlayer.collider.bounds.max ) * Vector3.up;

            RaycastHit hit;
            if (Physics.Linecast(targetPosition, viewPosition, out hit, layerMask))
            {
                viewPosition = targetPosition + rotation * (offset.normalized * (hit.distance - 0.1f));
            }


            //smooth erratic camera movement
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, viewPosition, ref velocity, Time.deltaTime / speed);

            //if (Vector3.Distance(transform.position, viewPosition) > 0.5f)
            //{
            //   time += Time.fixedDeltaTime;
            //   transform.position = Vector3.Lerp(transform.position, viewPosition, speed * time);
            //}
            //else
            //{
            //   time = 0;
            //   transform.position = viewPosition;
            //}


            transform.LookAt(targetPosition, Vector3.up);
        }
    }
}
