using UnityEngine;
using System.Collections;

public class Girl : Player {

    public enum State
    {
        MOVE,
        CLIMB,
        SHOOT,
        JUMP,
        FALL
    }

    public Object rockPrefab;
    public State state;

    private Transform[] myTargets;
    private Transform myTarget;
    //private int myTargetIndex = 0;

    // Use this for initialization
    protected override void Start() 
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update() 
    {
        // Are we level with the ground?
        if ( state == State.JUMP || state == State.FALL || state == State.MOVE )
        {
            for (int angle = 0; angle < Mathf.PI * 2; angle++)
            {
                Vector3 direction = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * (Vector3.right / 2);
                if ( Physics.Raycast(transform.position + direction, Vector3.down, 0.55f) )
                {
                    state = State.MOVE;
                    break;
                }
                state = State.FALL;            
            }
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.5f);

        //can we attack?
        if (state == State.MOVE && Input.GetButtonDown("Attack"))
        {
            findShootingTarget();
            Game.getInstance().state = Game.State.STRAFE;
            state = State.SHOOT;
        }

        if (state == State.SHOOT)
        {
            //rotate towards the shooting target
            if (Input.GetButton("Attack"))
            {
                findShootingTarget();
                if (myTargets != null && myTarget != null)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(myTarget.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
                    state = State.SHOOT;
                }
            }

            //toggle our shooting target
            if (Input.GetButtonDown("Action"))
            {
                for (int i = 0, next = 0; i < myTargets.Length; i++)
                {
                    if (myTarget == myTargets[i])
                    {
                        next = i;
                        do
                        {
                            next = (next + 1) % myTargets.Length;
                        } while (!myTargets[next] && next != i);

                        myTarget = myTargets[next];
                        break;
                    }
                }
                //myTargetIndex = (myTargetIndex + 1) % myTargets.Length;
            }

            //fire a projectile towards the shooting target.
            if (Input.GetButtonUp("Attack"))
            {
                GameObject rock = Instantiate( rockPrefab, transform.position, transform.rotation ) as GameObject;
                Rigidbody rockBody = rock.GetComponent<Rigidbody>();
                Physics.IgnoreCollision( rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>() );
                rockBody.AddForce( transform.forward * 50, ForceMode.Impulse );
                state = State.MOVE;
                Game.getInstance().state = Game.State.FREEROAM;
            }            
        }

        //can we move?
        canMove = (state == State.MOVE);

        if (canMove)
        {
            //Are we on an edge?
            if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f))
            {
                if (Input.GetButtonDown("Action"))
                {
                    //launches the player forward and up
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                    state = State.JUMP;
                }
            }
            else
            {
                state = State.MOVE;
            }
        }

        base.Update();
	}

    void findShootingTarget ()
    {
        int targetableLayerMask = 1 << 9;
        bool isCurrentTargetLost = true;

        Collider[] hits = Physics.OverlapSphere(transform.position, 10.0f, targetableLayerMask);
        myTargets = new Transform[hits.Length];

        for (int index = 0; index < myTargets.Length; index++ )
        {
            myTargets[index] = hits[index].transform;
            isCurrentTargetLost &= (myTargets[index] != myTarget);
        }

        if ( isCurrentTargetLost )
        {
            if ( myTargets.Length > 0 )
            {
                myTarget = myTargets[0];
            }
        }
    }
}
