using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    public int numOfPuzzles;
    public static int puzzlesSolved;

	// Use this for initialization
	void Start () {
        puzzlesSolved = 0;
	
	}
	
	// Update is called once per frame
	void OnEvent (BasicTrigger trigger) 
    {
        if (trigger.message == "doorOpen")
        {
            transform.Translate(0, -10, 0);
        }
	
	}

    void OnEventEnd (BasicTrigger trigger)
    {
        if (trigger.message == "doorOpen")
        {
            transform.Translate(0, 10, 0);
        }
    }
}
