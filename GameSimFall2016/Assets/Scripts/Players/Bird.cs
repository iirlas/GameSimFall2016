using UnityEngine;
using System.Collections;

public class Bird : Player {

    new public enum State
    {
    }

    float staminaSpeed = 10.0f;

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
            //rigidbody.velocity = Vector3.zero;
            clearParent();
            StatusManager.getInstance().stamina -= Time.deltaTime * 20.0f;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, movementSpeed, rigidbody.velocity.z);
            //rigidbody.position += ;
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
                StatusManager.getInstance().stamina += Time.deltaTime * staminaSpeed;
                if (StatusManager.getInstance().stamina >= 100.0f)
                {
                    canFly = true;
                    StatusManager.getInstance().stamina = 100.0f;
                }
            }
        }
    }

    void LateUpdate()
    {
        bool isPlayer = (PlayerManager.getInstance().currentPlayer == this);
        if ( !isPlayer )
        {
            StatusManager.getInstance().stamina = StatusManager.getInstance().stamina += Time.deltaTime * staminaSpeed;
        }
    }
}
