using UnityEngine;
using System.Collections;
using System;

public class Girl : Player {

    new public enum State
    {
        FALL,
        ATTACK,
    };

    public enum Status
    {
        NONE   = 0x00,  //0000
        POISON = 0x01,  //0001
        FIRE   = 0x02,  //0010
        DARK   = 0x04   //0100
    }

    [HideInInspector]
    public Animator animator;
    
    public GameObject rockPrefab;
    public Transform rockSpawnNode;
    public float shootingForce = 30.0f;
    
    public Status status = Status.DARK;
    public GameObject posisonEffect;
    public GameObject fireEffect;
    public GameObject darkEffect;
    public GameObject fearEffect;
    
    [HideInInspector]
    public Transform target
    {
        get { return myTarget; }
    }

    private int myTargetableLayerMask = 1 << 9;
    private bool isTargeting = false;
    private Collider[] myTargets;
    private Transform myTarget;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        addRunnable(Player.State.DEFAULT, runMoveState);
        addRunnable(State.FALL, runFallingState);
        addRunnable(State.ATTACK, runAttackState);
        //myTargetableLayerMask = 1 << LayerMask.NameToLayer("Targetable"); 
    }

    virtual protected void runMoveState()
    {
        RaycastHit hit;
        if (rigidbody.useGravity)
        {
            if (isGrounded(out hit))
            {
                setParent(hit);
                if (myTarget == null)
                {
                    movePlayer();
                }
                else
                {
                    strafe();
                }
                if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                {
                    animator.SetInteger("state", 1);
                }
                else
                {
                    animator.SetInteger("state", 0);
                }
            }
            else
            {
                animator.SetInteger("state", 2);
                playerState = State.FALL;
            }
        }

        if (!playerState.Equals(Player.State.DEFAULT))
        {
            return;
        }

        if (Input.GetButtonDown("Attack") && myStates.ContainsKey(State.ATTACK))
        {
            playerState = State.ATTACK;
        }
        else if (Input.GetButtonDown("Center"))
        {
            isTargeting = !isTargeting;
        }
        if (isTargeting)
        {
            if (!findTargets())
            {
                isTargeting = false;
            }
        }
        else
        {
            myTarget = null;
        }
    }

    protected void runFallingState()
    {
        if (isGrounded())
        {
            playerState = Player.State.DEFAULT;
        }
        else
        {
            clearParent();
        }
    }

    void runAttackState()
    {
        animator.SetInteger("state", 0);
        if (!isGrounded())
        {
            playerState = State.FALL;
            return;
        }

        if ( myTarget == null )
        {
            movePlayer();
        }
        else
        {
            strafe();
        }

        if (!Input.GetButton("Attack")) //fire a projectile towards the shooting target.
        {
            GameObject rock = Instantiate(rockPrefab, rockSpawnNode.position, transform.rotation) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
            Vector3 force = (target != null ? (target.position - rigidbody.position).normalized : transform.forward) * shootingForce;
            rockBody.AddForce(force, ForceMode.Impulse);
            playerState = Player.State.DEFAULT;
        }

        ////toggle our shooting target
        //if (Input.GetButtonDown("Action") && myTargets.Length != 0)
        //{
        //    int index = (Array.IndexOf(myTargets, myTarget.GetComponent<Collider>()) + 1) % myTargets.Length;
        //    myTarget = myTargets[index].transform;
        //}
    }

    void strafe ()
    {
        Vector3 cameraFoward = camera.transform.forward;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (myTarget != null)
        {
            //rotate towards the shooting target
            Vector3 direction = myTarget.position - rigidbody.position;
            direction.y = 0;//aligns facing direction with the ground.
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
            rigidbody.position += ((camera.transform.forward * v) +
                                   (camera.transform.right * h)) *
                                    movementSpeed * Time.deltaTime;
        }
    }

    bool findTargets()
    {
        myTargets = Physics.OverlapSphere(rigidbody.position, targetRange, myTargetableLayerMask);

        if (myTarget == null || Vector3.Distance(rigidbody.position, myTarget.position) > targetRange)
        {
            myTarget = (myTargets.Length != 0 ? myTargets[0].transform : null);

            for (int index = 0; index < myTargets.Length; index++)
            {
                float currentAngle = Vector3.Angle(camera.transform.forward, myTarget.position - rigidbody.position);
                float angle = Vector3.Angle(camera.transform.forward, myTargets[index].transform.position - rigidbody.position);
                if (angle < currentAngle)
                {
                    myTarget = myTargets[index].transform;
                }
            }
        }
        return (myTarget != null);
    }

    void LateUpdate()
    {
        if (PlayerManager.getInstance().currentPlayer != this)
        {
            animator.SetInteger("state", 0);
        }
        posisonEffect.SetActive((status & Status.POISON) == Status.POISON);
        fireEffect.SetActive((status & Status.FIRE) == Status.FIRE);
        darkEffect.SetActive((status & Status.DARK) == Status.DARK);
        if (darkEffect.activeSelf && GameObject.FindObjectOfType<FearManager>().fearCurrent >= 100 )
        {
            fearEffect.SetActive(true);
        }
        else
        {
            fearEffect.SetActive(false);
        }
    }
}
