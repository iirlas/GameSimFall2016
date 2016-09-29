using UnityEngine;
using System.Collections;

//========================================================================================================
//                                             Key Open
// Uses BASIC TRIGGER message: grabKey
// This script is placed on the exit door/door to be destroyed. When message fires (trigger on key), door opens.
//========================================================================================================

public class KeyOpen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "grabKey")
        {
            Destroy(this.gameObject);
        }
    }
}
