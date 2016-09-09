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
    public float shootingForce = 30.0f;
    public float jumpDistance = 2.5f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        addRunnable(Player.State.ATTACK, runAttackState);
        addRunnable(Player.State.ACTION, runActionState);
        addRunnable(State.SHOOT, runShootingState);
        //myTargetableLayerMask = 1 << LayerMask.NameToLayer("Targetable"); 
    }

    void runAttackState()
    {
        //myTarget = null;
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

        movePlayer();

        if ( Input.GetButtonUp("Attack") ) //fire a projectile towards the shooting target.
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


}
