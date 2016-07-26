using UnityEngine;
using System.Collections;

public class Bird : Player {

    enum State
    {
        FLY,
    }

    public GameObject rockPrefab;

    private Vector3 myDirection;

    // Use this for initialization
    void Start()
    {
        addRunnable(State.FLY, runFlyState);
        playerState = State.FLY;
    }

    void runFlyState ()
    {
        movePlayer();

        if ( myDirection == Vector3.zero )
        {
            StartCoroutine(setFlyDirection(0.3f));
        }

        if ( Input.GetButtonDown("Attack") )
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, transform.rotation) as GameObject;
            Rigidbody rockBody = rock.GetComponent<Rigidbody>();
            Physics.IgnoreCollision(rockBody.GetComponent<Collider>(), rigidbody.GetComponent<Collider>());
            rockBody.AddForce(-transform.up * movementSpeed, ForceMode.Impulse);
        }

        if (Input.GetButton("Action"))
        { 
            transform.position += myDirection * movementSpeed * Time.deltaTime;
        }
        else
        {
            myDirection = Vector3.zero;
        }
    }

    IEnumerator setFlyDirection ( float pressDelay )
    {
        Timer timer = new Timer();
        if (Input.GetButtonDown("Action"))
        {
            myDirection = transform.up;
            timer.start();

            yield return new WaitForEndOfFrame();// wait for input reset.
            while (timer.elapsedTime() < pressDelay)
            {
                if (Input.GetButtonDown("Action"))
                {
                    myDirection = -transform.up;
                    break;
                }
                yield return null;
            }
            yield break;
        }
        myDirection = Vector3.zero;
        yield break;
    }
}
