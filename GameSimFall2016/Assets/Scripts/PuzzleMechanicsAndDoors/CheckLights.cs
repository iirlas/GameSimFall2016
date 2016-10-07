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
    private Vector3 destination;
    private bool openDoor;
    private float speed;

    private int count;
    

	// Use this for initialization
	void Awake () {
        count = 0;
        speed = 2f;
        destination = new Vector3(exitDoor.transform.position.x,
                                (exitDoor.gameObject.GetComponent<Collider>().bounds.size.y + exitDoor.transform.position.y),
                                  exitDoor.transform.position.z);
        openDoor = false;
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
            openDoor = true;
            

        }
        else
        {
            count = 0;
        }

        if (openDoor)
        {
            if (exitDoor.transform.position.y < destination.y)
            {
                exitDoor.transform.position = Vector3.Lerp(exitDoor.transform.position,
                                          destination,
                                          speed * 3.0f * Time.deltaTime);

            }
        }

        if (exitDoor.transform.position.y >= destination.y)
        {
            Destroy(exitDoor);
        }	
	}
}
