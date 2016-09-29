using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Check Lights
// Checks if the number of lights on meets the designated number to finish the puzzle and opens the door.
//========================================================================================================

public class CheckLights : MonoBehaviour {
    [Tooltip("Door to Exit from")]
    public GameObject exitDoor;
    [Tooltip ("Lights to be counted")]
    public Light[] desiredLights; 

    private int count;
    

	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Light myLight in desiredLights)
        {
            if (myLight.intensity > 0)
                count++;
        }

        if (count == desiredLights.Length)
        {
            //Debug.Log("countMet");
            exitDoor.SetActive(false);

        }
        else
        {
            count = 0;
        }
	
	}
}
