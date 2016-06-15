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
    public Transform rockSpawnNode;
    public float targetRange = 10.0f;
    public float shootingForce = 30.0f;

    private Transform[] myTargets;
    private Transform myTarget;

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
            float outRadius = 0.5f;
            float distance = 1.1f;

            for (int angle = 0; angle < Mathf.PI * 2; angle++)
            {
                Vector3 origin = transform.position + (Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * (transform.right * outRadius));

                Debug.DrawLine(origin, (origin + Vector3.down));

                if (Physics.Raycast(origin, Vector3.down, distance))
                {
                    state = State.MOVE;
                    break;
                }
                state = State.FALL;
            }
        }

        //can we attack?
        if (state == State.MOVE && Input.GetButtonDown("Attack"))
        {
            myTarget = null;
            Game.getInstance().gameState = Game.GameState.STRAFE;
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
                    Vector3 direction = myTarget.position - transform.position;
                    direction.y = 0;//aligns facing direction with the ground.
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
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
            }

            //fire a projectile towards the shooting target.
            if (Input.GetButtonUp("Attack"))
            {
                GameObject rock = Instantiate(rockPrefab, rockSpawnNode.position, transform.rotation) as GameObject;
                Rigidbody rockBody = rock.GetComponent<Rigidbody>();
                Physics.IgnoreCollision( rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>() );
                Vector3 force = (myTarget ? (myTarget.position - transform.position).normalized : transform.forward) * shootingForce;
                rockBody.AddForce(force, ForceMode.Impulse);
                state = State.MOVE;
                Game.getInstance().gameState = Game.GameState.FREEROAM;
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

        Collider[] hits = Physics.OverlapSphere(transform.position, targetRange, targetableLayerMask);
        myTargets = new Transform[hits.Length];

        for (int index = 0; index < myTargets.Length; index++ )
        {
            myTargets[index] = hits[index].transform;
            isCurrentTargetLost &= (myTargets[index] != myTarget);
            Debug.DrawLine(transform.position, myTargets[index].position);
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
