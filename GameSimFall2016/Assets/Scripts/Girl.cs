using UnityEngine;
using System.Collections;

public class Girl : Player {

    new public enum State
    {
        CLIMB,
        SHOOT,
    }

    [HideInInspector]
    public Animator animator;
    
    public GameObject rockPrefab;
    public Transform rockSpawnNode;
    public float targetRange = 10.0f;
    public float shootingForce = 30.0f;
    public float jumpDistance = 2.5f;

    private Transform[] myTargets;
    private Transform myTarget;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        addRunnable(Player.State.ATTACK, runAttackState);
        addRunnable(Player.State.ACTION, runActionState);
        addRunnable(Player.State.FALL, runFallingState);
        addRunnable(State.SHOOT, runShootingState);
    }

    void runAttackState()
    {
        myTarget = null;
        playerState = State.SHOOT;
    }

    void runActionState()
    {
        playerState = Player.State.MOVE;

        //Are we on an edge?
        if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, collider.bounds.size.y))
        {
            //launches the player forward and up
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce((transform.up + transform.forward) * jumpDistance, ForceMode.Impulse);
            playerState = Player.State.FALL;
        }
    }
	
    void runShootingState()
    {
        if (!isGrounded())
        {
            playerState = Player.State.FALL;
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
        else //fire a projectile towards the shooting target.
        {
            GameObject rock = Instantiate(rockPrefab, rockSpawnNode.position, transform.rotation) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
            Vector3 force = (myTarget ? (myTarget.position - transform.position).normalized : transform.forward) * shootingForce;
            rockBody.AddForce(force, ForceMode.Impulse);
            playerState = Player.State.MOVE;
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
    }

    bool findShootableTarget()
    {
        int targetableLayerMask = 1 << LayerMask.NameToLayer("Targetable");
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
