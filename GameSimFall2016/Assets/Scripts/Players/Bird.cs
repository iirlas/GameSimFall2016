using UnityEngine;
using System.Collections;

public class Bird : Player {

    new public enum State
    {
    }
 
    private bool canFly = false;

    // Use this for initialization
    void Start()
    {
        addRunnable(Player.State.DEFAULT, runFallingState);
    }

    protected void runFallingState()
    {
        movePlayer();
        if (Input.GetButton("Action") && canFly)
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
            clearParent();
            StatusManager.getInstance().stamina -= Time.deltaTime * 20.0f;
            rigidbody.position += transform.up * (Time.deltaTime) * movementSpeed;
            if (StatusManager.getInstance().stamina <= 0)
            {
                canFly = false;
                StatusManager.getInstance().stamina = 0;
            }
        }
        else
        {
            RaycastHit hit;
            if (isGrounded(out hit))
            {
                setParent(hit);
                StatusManager.getInstance().stamina = StatusManager.getInstance().stamina + Time.deltaTime * 10.0f;
                if (StatusManager.getInstance().stamina >= 100.0f)
                {
                    canFly = true;
                    StatusManager.getInstance().stamina = 100.0f;
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
