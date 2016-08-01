using UnityEngine;
using System.Collections;

public class Rabbit : Player {

    new public enum State
    {

    };

	// Use this for initialization
    protected void Start()
    {
        addRunnable(Player.State.ACTION, runActionState);
        addRunnable(Player.State.FALL, runFallingState);
	}

    void runActionState ()
    {
        playerState = Player.State.MOVE;
        if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, collider.bounds.size.y))//Are we on an edge then?
        {
            //launches the player forward and up
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
            playerState = Player.State.FALL;
        }
    }
}
