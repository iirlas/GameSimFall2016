using UnityEngine;
using System.Collections;

public class Cat : Player {

    new public enum State 
    {
        CLIMB,
    }

    public float jumpDistance = 5.0f;
    public LayerMask climbingLayer = 1 << 8;

    // Use this for initialization
    protected void Start()
    {
        addRunnable(Player.State.ACTION,runActionState);
        addRunnable(State.CLIMB, runClimbState);
    }

    void runActionState()
    {
        playerState = Player.State.MOVE;

        RaycastHit hit;
        //Are we facing a climbable object
        if (Physics.Raycast(transform.position, transform.forward.normalized, out hit, 1.5f, climbingLayer))
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;

            transform.position = hit.point;
            transform.forward = -hit.normal;
            playerState = State.CLIMB;
        }
        else if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, collider.bounds.size.y))//Are we on an edge then?
        {
            //launches the player forward and up
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce((transform.up + transform.forward) * jumpDistance, ForceMode.Impulse);
            playerState = Player.State.FALL;
        }
    }

    void runClimbState ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        if ( h != 0 || v != 0 )
        {
            float speed = movementSpeed * Time.deltaTime;

            Vector3 hPos = transform.position + (transform.right * h * speed);
            Vector3 vPos = transform.position + (transform.up * v * speed);
            Vector3 offset = Vector3.zero;
            RaycastHit hit;

            if (h != 0 && Physics.Raycast(hPos, transform.forward, out hit, 2f, climbingLayer))
            {
                transform.forward = -hit.normal;
                offset += (hit.point - (transform.forward / 2)) - transform.position;

            }
            if (v != 0 && Physics.Raycast(vPos, transform.forward, out hit, 2f, climbingLayer))
            {
                transform.forward = -hit.normal;
                offset += (hit.point - (transform.forward / 2)) - transform.position;
            }
            
            transform.position += offset;
        }

        if (Input.GetButtonDown("Action"))
        {
            if ( !Physics.Raycast(transform.position + Vector3.up, transform.forward, 2.0f ) )
            {
                transform.position += Vector3.up + transform.forward.normalized;
            }
            rigidbody.useGravity = true;
            playerState = Player.State.MOVE;
        }
        rigidbody.velocity = Vector3.zero;
    }
}
