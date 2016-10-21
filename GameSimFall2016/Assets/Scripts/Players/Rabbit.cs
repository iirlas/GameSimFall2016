using UnityEngine;
using System.Collections;

public class Rabbit : Player {

    

    new public enum State
    {
        FALL,
        ACTION
    };
    [HideInInspector]
    public Animator animator;
    public float jumpDistance = 5.0f;

	// Use this for initialization
    protected void Start()
    {
        animator = GetComponent<Animator>();
        addRunnable(Player.State.DEFAULT, runMoveState);
        addRunnable(State.ACTION, runActionState);
        addRunnable(State.FALL, runFallingState);
	}

    protected void runMoveState ()
    {
        RaycastHit hit;
        if (isGrounded(out hit))
        {
            setParent(hit);
            movePlayer();
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                animator.SetInteger("State", 2);
            }
            else
            {
                animator.SetInteger("State", 1);
            }

            if (Input.GetButtonDown("Action"))
            {
                //launches the player forward and up
                animator.SetInteger("State", 3);
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce((transform.up + transform.forward) * jumpDistance, ForceMode.Impulse);
                playerState = State.FALL;
            }
        }
        else
        {
            animator.SetInteger("State", 1);
            playerState = State.FALL;
        }
    }

    void runActionState ()
    {

        playerState = Player.State.DEFAULT;
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

}
