using UnityEngine;
using System.Collections;

public class SwitchControl : MonoBehaviour {

    public int solvedNum = 0;
    public static int counter = 0;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (counter > 0 && counter == solvedNum)
        {
            Debug.Log("Open the door");
            //door Open
        }
	
	}
}
