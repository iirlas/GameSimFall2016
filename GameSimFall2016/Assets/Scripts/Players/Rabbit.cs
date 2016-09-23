using UnityEngine;
using System.Collections;

public class Rabbit : Player {

    new public enum State
    {
        FALL,
        ACTION
    };

    public float jumpDistance = 5.0f;

	// Use this for initialization
    protected void Start()
    {
        addRunnable(Player.State.DEFAULT, runMoveState);
        addRunnable(State.ACTION, runActionState);
        addRunnable(State.FALL, runFallingState);
	}

    protected void runMoveState ()
    {
        RaycastHit hit;
        if (rigidbody.useGravity)
        {
            if (isGrounded(out hit))
            {
                setParent(hit, floorAngleLimit);
                movePlayer();
                if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, collider.bounds.size.y))//Are we on an edge then?
                {
                    //launches the player forward and up
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce((transform.up + transform.forward) * jumpDistance, ForceMode.Impulse);
                    playerState = State.FALL;
                }
            }
            else
            {
                playerState = State.FALL;
            }
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
