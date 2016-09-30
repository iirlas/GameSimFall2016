using UnityEngine;
using System.Collections;

public class Bird : Player {

    new public enum State
    {
    }
 
    public float stamina = 100.0f;
    private bool canFly = false;

    // Use this for initialization
    void Start()
    {
        addRunnable(Player.State.DEFAULT, runFallingState);
        //addRunnable(State.ATTACK, runAttackState);
        //addRunnable(State.ACTION, runActionState);
    }

    protected void runFallingState()
    {
        movePlayer();
        if (Input.GetButton("Action") && canFly)
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
            clearParent();
            stamina -= Time.deltaTime * 20.0f;
            rigidbody.position += transform.up * (Time.deltaTime) * movementSpeed;
            if ( stamina <= 0 )
            {
                canFly = false;
                stamina = 0;
            }
        }
        else
        {
            RaycastHit hit;
            if (isGrounded(out hit))
            {
                setParent(hit);
                stamina += stamina + Time.deltaTime * 10.0f;
                if ( stamina >= 100.0f )
                {
                    canFly = true;
                    stamina = 100.0f;
                }
            }
            rigidbody.useGravity = true;
        }
    }

    void LateUpdate()
    {
        rigidbody.useGravity = (PlayerManager.getInstance().currentPlayer != this);
    }
}
