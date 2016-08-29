using UnityEngine;
using System.Collections;

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
