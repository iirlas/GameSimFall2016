using UnityEngine;
using System.Collections;

public class Bird : Player {

    new public enum State
    {
    }

    public GameObject rockPrefab;

    // Use this for initialization
    void Start()
    {
        addRunnable(Player.State.ATTACK, runAttackState);
        addRunnable(Player.State.ACTION, runActionState);
        addRunnable(Player.State.FALL, runFallingState);
    }


    void runAttackState ()
    {
        playerState = Player.State.MOVE;
        GameObject rock = Instantiate(rockPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody rockBody = rock.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
        rockBody.AddForce(-transform.up * movementSpeed, ForceMode.Impulse);
    }

    void runActionState ()
    {
        playerState = Player.State.MOVE;
        transform.position += transform.up * (movementSpeed * Time.deltaTime);
    }

    override protected void OnFallingState()
    {
        if (Input.GetButton("Action"))
        {
            rigidbody.useGravity = false;
            playerState = Player.State.MOVE;
        }
    }

    override protected void OnMoveState()
    {
        movePlayer();
        if (!Input.GetButton("Action"))
        {
            transform.position -= transform.up * (Time.deltaTime);// * 0.1f);
            if ( isGrounded() )
            {
                rigidbody.useGravity = true;
            }
        }
        rigidbody.velocity = Vector3.zero;
    }
}
