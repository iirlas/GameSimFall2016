using UnityEngine;
using System.Collections;

public class Girl : Player {

    public enum State
    {
        MOVE,
        CLIMB,
        SHOOT,
    }

    public Object rockPrefab;
    public State state;

    private bool canJump = false;
    private Transform[] myTargets;
    private int myTargetIndex = 0;

    // Use this for initialization
    protected override void Start() 
    {
        base.Start();
        //myTargets = GameObject.FindGameObjectsWithTag("Targetable");
    }
	
	// Update is called once per frame
    protected override void Update() 
    {
        base.Update();
        int collidableLayerMask = 1 << 8;
        int targetableLayerMask = 1 << 9;


        bool isGrounded = false;
        for (int angle = 0; angle < Mathf.PI * 2; angle++)// level with ground check
        {
            Vector3 direction = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * (Vector3.right / 2);
            isGrounded |= Physics.Raycast(transform.position + direction, Vector3.down, 1.0f, collidableLayerMask);
        }

        Debug.DrawLine( transform.position, transform.position + transform.forward );

        //can we move?
        canMove = !Physics.Raycast(transform.position, transform.forward, transform.forward.magnitude, collidableLayerMask);
        canMove &= isGrounded;
        canMove &= (state == State.MOVE);
        canRotate = isGrounded;
        canRotate &= (state == State.MOVE);

        if (Input.GetButtonDown("Attack"))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 10.0f, targetableLayerMask);
            myTargets = new Transform[hits.Length];
            int index = 0;
            foreach ( Collider hit in hits )
            {
                myTargets[index] = hit.transform;
                index++;
            }
            state = State.SHOOT;
            Game.getInstance().state = Game.State.STRAFE;
        }

        if (Input.GetButtonDown("Action") && state == State.SHOOT)
        {
            myTargetIndex = (myTargetIndex + 1) % myTargets.Length;
        }

        if (Input.GetButton("Attack"))
        {
            if ( myTargetIndex < myTargets.Length )
            {
                transform.LookAt( myTargets[myTargetIndex].position );            
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            GameObject rock = Instantiate( rockPrefab, transform.position, transform.rotation ) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision( rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>() );
            rockBody.AddForce( transform.forward * 50, ForceMode.Impulse );
            state = State.MOVE;
            Game.getInstance().state = Game.State.FREEROAM;
            //rock
            //GameObject rock = new GameObject();
            //rock.AddComponent<Transform>();
            //rock.AddComponent<Rigidbody>().velocity 
        }

        if (canMove)
        {
            //Are we on an edge?
            if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f, collidableLayerMask))
            {
                if (canJump && isGrounded && Input.GetButtonDown("Action"))
                {
                    //launches the player forward and up
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                    canJump = false;
                }
            }
            else
            {
                canJump = true;
            }
        }
	}
}
