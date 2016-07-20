using UnityEngine;
using System.Collections;

public class Cat : Player {

    enum State 
    {
        MOVE,
        CLIMB,
        FALL,
    }

    public LayerMask climbingLayer;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        addRunnable(State.MOVE, runWalkState);
        addRunnable(State.CLIMB, runClimbState);
        addRunnable(State.FALL, runFallingState);
        playerState = State.MOVE;
    }

    void runWalkState ()
    {
        if (!isGrounded()) 
        {
            playerState = State.FALL;
            return;
        }

        movePlayer();

        if (Input.GetButtonDown("Action"))
        {
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
            else if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f))//Are we on an edge then?
            {
                //launches the player forward and up
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                playerState = State.FALL;
            }
        }
    }

    void runClimbState ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float colliderMag = collider.bounds.size.z;
        
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
            playerState = State.MOVE;
        }
        rigidbody.velocity = Vector3.zero;
    }

    void runFallingState()
    {
        if (isGrounded())
        {
            playerState = State.MOVE;
        }
    }
}
