using UnityEngine;
using System.Collections;
using System;

public class Girl : Player {

    new public enum State
    {
        SHOOT,
    }

    [HideInInspector]
    public Animator animator;
    
    public GameObject rockPrefab;
    public Transform rockSpawnNode;
    public float targetRange = 10.0f;
    public float shootingForce = 30.0f;
    public float jumpDistance = 2.5f;

    private Collider[] myTargets;
    private Transform myTarget;
    private int myTargetableLayerMask;


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        addRunnable(Player.State.ATTACK, runAttackState);
        addRunnable(Player.State.ACTION, runActionState);
        addRunnable(State.SHOOT, runShootingState);
        myTargetableLayerMask = 1 << LayerMask.NameToLayer("Targetable"); 
    }

    void runAttackState()
    {
        myTarget = null;
        playerState = State.SHOOT;
    }

    void runActionState()
    {
        playerState = Player.State.DEFAULT;

        //Are we on an edge?
        if (!Physics.Raycast(rigidbody.position + transform.forward, Vector3.down, collider.bounds.size.y))
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
            if (!findTargets())
            {
                transform.eulerAngles += Vector3.up * Input.GetAxis("Horizontal");
                rigidbody.position += camera.transform.forward * Input.GetAxis("Vertical") * 
                                      movementSpeed * Time.deltaTime;
            }
            else
            {
                Vector3 direction = myTarget.position - rigidbody.position;
                direction.y = 0;//aligns facing direction with the ground.
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
                strafe();
            }
        }
        else //fire a projectile towards the shooting target.
        {
            GameObject rock = Instantiate(rockPrefab, rockSpawnNode.position, transform.rotation) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
            Vector3 force = (myTarget != null ? (myTarget.position - rigidbody.position).normalized : transform.forward) * shootingForce;
            rockBody.AddForce(force, ForceMode.Impulse);
            playerState = Player.State.DEFAULT;
        }

        //toggle our shooting target
        if (Input.GetButtonDown("Action") && myTargets.Length != 0)
        {
            int index = (Array.IndexOf(myTargets, myTarget.GetComponent<Collider>()) + 1) % myTargets.Length;
            myTarget = myTargets[index].transform;
        }
    }

    void strafe ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
        {
            rigidbody.position += ((camera.transform.forward * v) +
                                   (camera.transform.right * h)) *
                                    movementSpeed * Time.deltaTime;
        }
    }

    bool findTargets ()
    {
        myTargets = Physics.OverlapSphere(rigidbody.position, targetRange, myTargetableLayerMask);

        if (myTarget == null || Vector3.Distance(rigidbody.position, myTarget.position) > targetRange)
        {
            myTarget = (myTargets.Length != 0 ? myTargets[0].transform : null );

            for (int index = 0; index < myTargets.Length; index++)
            {
                float currentAngle = Vector3.Angle( camera.transform.forward, myTarget.position - rigidbody.position );
                float angle = Vector3.Angle( camera.transform.forward, myTargets[index].transform.position - rigidbody.position );
                if ( angle < currentAngle )
                {
                    myTarget = myTargets[index].transform;
                }
            }
        }
        return (myTarget != null);
    }
}
