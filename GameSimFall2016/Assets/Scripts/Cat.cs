﻿ using UnityEngine;
using System.Collections;

public class Cat : Player {

    enum State 
    {
        WALK,
        CLIMB,
        FALL,
    }

    public LayerMask climbingLayer;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        addRunnable(State.WALK, runWalkState);
        addRunnable(State.CLIMB, runClimbState);
        addRunnable(State.FALL, runFallingState);
        playerState = State.WALK;
    }

    void runWalkState ()
    {
        if (isGrounded( .7f)) 
        {
            movePlayer();
        }

        //Are we on an edge?
        if (!Physics.Raycast(transform.position + transform.forward, Vector3.down, 2.0f))
        {
            if (Input.GetButtonDown("Action"))
            {
                //launches the player forward and up
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce((transform.up + transform.forward) * movementSpeed, ForceMode.Impulse);
                playerState = State.FALL;
                return;
            }
        }

        if (Input.GetButtonDown("Action"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f, climbingLayer))
            {
                rigidbody.useGravity = false;
                rigidbody.velocity = Vector3.zero;

                transform.position = hit.point;
                transform.eulerAngles = hit.normal;

                playerState = State.CLIMB;
            }
        }
    }

    void runClimbState ()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float colliderMag = collider.bounds.size.z;

        if ( h != 0 || v != 0 )
        {
            if (Physics.Raycast(transform.position, transform.forward + transform.right * h, colliderMag))
            {
                transform.position += transform.right * h * movementSpeed * Time.deltaTime;
            }
            if ( Physics.Raycast(transform.position, transform.forward + transform.up * v, colliderMag)) 
            {
                transform.position += transform.up * v * movementSpeed * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Action"))
        {
            if ( !Physics.Raycast(transform.position + Vector3.up, transform.forward, 2.0f ) )
            {
                transform.position += Vector3.up + transform.forward.normalized;
            }
            rigidbody.useGravity = true;
            playerState = State.WALK;
        }
        rigidbody.velocity = Vector3.zero;
    }

    void runFallingState()
    {
        if (isGrounded())
        {
            playerState = State.WALK;
        }
    }
}
