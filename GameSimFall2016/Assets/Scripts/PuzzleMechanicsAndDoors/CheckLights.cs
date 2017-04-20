using UnityEngine;
using System.Linq;
using System.Collections;

//========================================================================================================
//                                              Check Lights
// Checks if the number of lights on meets the designated number to finish the puzzle and opens the door.
//========================================================================================================

public class CheckLights : MonoBehaviour {
    [Tooltip("Door to Exit from")]
    //public GameObject exitDoor;
	public UnityEngine.Events.UnityEvent completeEvent;
    [Tooltip ("Lights to be counted")]
    public Light[] desiredLights;

    private int count;
    

	// Use this for initialization
	void Awake () {
        count = 0;
    }

    // Update is called once per frame
	void Update () {
		if (count != -1) {
			count = desiredLights.Count ((Light light) => {
				return (light.intensity > 0);
			});
			if (count == desiredLights.Length) {
				completeEvent.Invoke ();
				count = -1;
			}
		}
	}
}
