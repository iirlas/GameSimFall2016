using UnityEngine;
using System.Collections;

public class Girl : Player {

    private bool canJump = false;


    // Use this for initialization
    protected override void Start() 
    {
        base.Start();
    }
	
	// Update is called once per frame
    protected override void Update() 
    {
        base.Update();
        int collidableLayerMask = 1 << 8;


        bool isGrounded = false;
        for (int angle = 0; angle < Mathf.PI * 2; angle++)// level with ground check
        {
            Vector3 direction = Quaternion.Euler(0, Mathf.Rad2Deg * angle, 0) * (Vector3.right / 2);
            isGrounded |= Physics.Raycast(transform.position + direction, Vector3.down, 1.0f, collidableLayerMask);
        }

        Debug.DrawLine( transform.position, transform.position + transform.forward );

        //can we move?
        canMove = !Physics.Raycast(transform.position, transform.forward, transform.forward.magnitude, collidableLayerMask);
        canMove &= isGrounded;
        canRotate = isGrounded;

        if (canMove)
        {
            //Are we on an edge?
            if (!Physics.Raycast(transform.position, Vector3.down, 2.0f, collidableLayerMask))
            {
                if (canJump && isGrounded)//&& Input.GetButtonDown("Action"))
                {
                    //launches the player forward and up
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                    canJump = false;
                }
            }
            else
            {
                canJump = true;
            }
        }
	}
}
