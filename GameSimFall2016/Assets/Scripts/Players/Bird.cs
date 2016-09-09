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
    }

    void runAttackState ()
    {
        playerState = Player.State.DEFAULT;
        GameObject rock = Instantiate(rockPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody rockBody = rock.GetComponent<Rigidbody>();
        Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
        rockBody.AddForce(-transform.up * movementSpeed, ForceMode.Impulse);
    }

    void runActionState ()
    {
        playerState = Player.State.FALL;

        if (transform.parent != null)
        {
            transform.parent.parent = null;
        }

        rigidbody.AddForce(transform.up, ForceMode.Impulse);
        rigidbody.useGravity = false;
    }

    override protected void runFallingState()
    {
        movePlayer();
        if (Input.GetButton("Action"))
        {
            rigidbody.useGravity = false;
            playerState = Player.State.FALL;
            transform.position += transform.up * (movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.position -= transform.up * (Time.deltaTime);// * 0.1f);
            if (isGrounded())
            {
                rigidbody.useGravity = true;
                playerState = Player.State.DEFAULT;
            }
            rigidbody.velocity = Vector3.zero;
        }
    }
}
