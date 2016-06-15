using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Girl : Player {

    public enum State
    {
        MOVE,
        CLIMB,
        SHOOT,
        FALL
    }

    private delegate void StateRunner();

    public Object rockPrefab;
    public State state;
    public Transform rockSpawnNode;
    public float targetRange = 10.0f;
    public float shootingForce = 30.0f;


    private Transform[] myTargets;
    private Transform myTarget;
    private Dictionary<State, StateRunner> myStates = new Dictionary<State,StateRunner>();


    // Use this for initialization
    protected override void Start() 
    {
        base.Start();
        myStates.Add(State.MOVE, runMoveState);
        myStates.Add(State.SHOOT, runShootingState);
        myStates.Add(State.FALL, runFallingState);
    }
	
	// Update is called once per frame
    protected override void Update() 
    {
        myStates[state]();
	}

    void runMoveState()
    {
        if (!isGrounded())
        {
            state = State.FALL;
            return;
        }

        //can we attack?
        if (Input.GetButtonDown("Attack"))
        {
            myTarget = null;
            Game.getInstance().gameState = Game.GameState.STRAFE;
            state = State.SHOOT;
            return;
        }

        //Are we on an edge?
        if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f))
        {
            if (Input.GetButtonDown("Action"))
            {
                //launches the player forward and up
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                state = State.FALL;
                return;
            }
        }

        base.Update();
    }

    void runShootingState()
    {
        if ( !isGrounded() )
        {
            state = State.FALL;
            return;
        }


        //rotate towards the shooting target
        if (Input.GetButton("Attack"))
        {
            if (!findShootableTarget())
            {
                transform.eulerAngles += Vector3.up * Input.GetAxis("Horizontal");
            }
            else
            {
                Vector3 direction = myTarget.position - transform.position;
                direction.y = 0;//aligns facing direction with the ground.
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
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
        }

        //fire a projectile towards the shooting target.
        if (Input.GetButtonUp("Attack"))
        {
            GameObject rock = Instantiate(rockPrefab, rockSpawnNode.position, transform.rotation) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
            Vector3 force = (myTarget ? (myTarget.position - transform.position).normalized : transform.forward) * shootingForce;
            rockBody.AddForce(force, ForceMode.Impulse);
            Game.getInstance().gameState = Game.GameState.FREEROAM;
            state = State.MOVE;
        }            
    }

    void runFallingState ()
    {
        if ( isGrounded() )
        {
            state = State.MOVE;
        }
    }

    bool isGrounded ( float outRadius = 0.5f, float distance = 1.1f )
    {
        // Are we level with the ground?
        for (int angle = 0; angle < Mathf.PI * 2; angle++)
        {
            Vector3 origin = transform.position + (Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * (transform.right * outRadius));

            Debug.DrawLine(origin, (origin + Vector3.down));

            if (Physics.Raycast(origin, Vector3.down, distance))
            {
                return true;
            }
        }
        return false;
    }

    bool findShootableTarget()
    {
        int targetableLayerMask = 1 << 9;
        bool isCurrentTargetLost = true;

        Collider[] hits = Physics.OverlapSphere(transform.position, targetRange, targetableLayerMask);
        myTargets = new Transform[hits.Length];

        for (int index = 0; index < myTargets.Length; index++)
        {
            myTargets[index] = hits[index].transform;
            isCurrentTargetLost &= (myTargets[index] != myTarget);
            Debug.DrawLine(transform.position, myTargets[index].position);
        }

        if (isCurrentTargetLost)
        {
            if (myTargets.Length > 0)
            {
                myTarget = myTargets[0];
            }
        }

        return (myTarget != null);
    }
}
