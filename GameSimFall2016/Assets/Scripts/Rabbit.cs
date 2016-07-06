using UnityEngine;
using System.Collections;

public class Rabbit : Player {

    enum State
    {
        MOVE,
        FALL,
    }

	// Use this for initialization
    protected override void Start()
    {
        base.Start();

        addRunnable(State.MOVE, runMoveState);
        addRunnable(State.FALL, runFallingState);

        playerState = State.MOVE;
	}
	
	// Update is called once per frame
    protected override void Update()
    {
        base.Update();
	}

    void runMoveState ()
    {
        movePlayer();

        if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f))//Are we on an edge then?
        {
            //launches the player forward and up
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
            playerState = State.FALL;
        }
    }

    void runFallingState ()
    {
        if (isGrounded())
        {
            playerState = State.MOVE;
        }
    }

}
