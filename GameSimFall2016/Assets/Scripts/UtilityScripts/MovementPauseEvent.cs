using UnityEngine;
using System.Collections;

public class MovementPauseEvent : MonoBehaviour {

    public float pauseTime = 1.0f;

    private Timer myTimer = new Timer();
    private Movement myMovement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( myTimer.elapsedTime() >= pauseTime )
        {
            myMovement.canMove = true;
            myTimer.stop();
        }
	}

    void OnMovementStart ( Movement movement )
    {
        myMovement = movement;
    }

    void OnMovementEnd ()
    {

    }

    void OnNodeReached ()
    {
        myMovement.canMove = false;
        myTimer.start();
    }
}
