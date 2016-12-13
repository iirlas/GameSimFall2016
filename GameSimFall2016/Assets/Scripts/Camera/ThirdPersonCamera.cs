using UnityEngine;
using System.Collections;
//Third person Camera
//      Fixates Unity's in a third person position relative to the current player.
public class ThirdPersonCamera : MonoBehaviour {

    //The movement Speed of the camera
    public float speed = 10f;

    //The Pivot point relative to the current player that the camera will rotate on.
    public Vector3 pivotPoint = new Vector3(0, 0, 0);

    //The offset from the player
    public Vector3 offset = new Vector3( 0, 1, -5 );

    //The Obstructing Layers
    public LayerMask layerMask = 0x1; //default layer

    [HideInInspector]
    public new Transform transform;

    [HideInInspector]
    public new Camera camera;

    private Player myPlayer;
    private Vector3 myAngle = Vector3.zero;
    private float time = 0.0f;
    private bool isTracking = true;

    //------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Awake () {
        transform = GetComponent<Transform>();
        camera = GetComponent<Camera>();
	}

    //------------------------------------------------------------------------------------------------
    // Start is called at the start of on object life
    void Start()
    {
        myAngle = PlayerManager.getInstance().currentPlayer.transform.eulerAngles;
    }

    //------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update () {
        if (myPlayer != PlayerManager.getInstance().currentPlayer)
        {
            myPlayer = PlayerManager.getInstance().currentPlayer;
        }
	}

    //------------------------------------------------------------------------------------------------
    // Late Update is called once per frame after Update is called
    public void LateUpdate()
    {
        if (myPlayer == null)
            return;

        Vector3 target = Vector3.zero;
        Vector3 view = Vector3.zero;

        Girl kira = (myPlayer as Girl);
        if (kira != null && kira.target != null)
        {
            targeting(out view, out target);
        }
        else
        {
            thirdPerson(out view, out target);
        }
        Vector3 velocity = Vector3.zero;

        //smooth erratic camera movement
        float distance = Vector3.Distance(transform.position, view);
        if (distance > offset.magnitude && isTracking)
        {
            transform.position = Vector3.SmoothDamp(transform.position, view, ref velocity, 2 / time);
        }
        else
        {
            transform.position = Vector3.Lerp( transform.position, view, time);
            isTracking = false;
        }
        time += Time.deltaTime * speed;

        transform.LookAt(target, Vector3.up);
    }

    //------------------------------------------------------------------------------------------------
    //Aligns the camera to the mouse's movement 
    void thirdPerson ( out Vector3 view, out Vector3 target )
    {
        float horizontal = Input.GetAxis("Alt_Horizontal");
        float vertical = Input.GetAxis("Alt_Vertical");
        Quaternion rotation = Quaternion.identity;
        Vector3 targetView = myPlayer.collider.bounds.center + pivotPoint; 

        if (Input.GetButtonDown("Center"))
        {
            myAngle = new Vector3(0, myPlayer.transform.eulerAngles.y, 0);
            time = 0;
        }
        else if (horizontal != 0 || vertical != 0)
        {
            myAngle += new Vector3(-vertical, horizontal, 0) * (speed * Time.deltaTime);
        }
        //lock camera angles
        myAngle.x = Mathf.Clamp(myAngle.x, -30, 45);

        rotation = Quaternion.Euler(myAngle);
        view = targetView + rotation * offset;
        target = targetView;

        RaycastHit hit;
        if (Physics.Linecast(targetView, view, out hit, layerMask))
        {
         view = hit.point + (targetView - hit.point).normalized * 0.1f;
        }

        //Debug.DrawLine(view, target);

    }

    //------------------------------------------------------------------------------------------------
    //Aligns the camera to the player's current target
    void targeting (out Vector3 view, out Vector3 target)
    {
        Girl kira = (myPlayer as Girl);
        target = kira.target.position;

        Quaternion rotation = Quaternion.LookRotation(kira.transform.position - target);
        view = kira.transform.position + rotation * (Vector3.forward - Vector3.right * 0.4f) + Vector3.up;
   }
}
