using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour {
    public int numOfPuzzles;
    public static int puzzlesSolved;

	// Use this for initialization
	void Start () {
        puzzlesSolved = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (puzzlesSolved == numOfPuzzles)
        {
            //openDoor
            this.transform.Translate(0, 20, 0);
        }
	
	}
}
