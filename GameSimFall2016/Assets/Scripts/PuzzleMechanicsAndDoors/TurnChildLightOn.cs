using UnityEngine;
using System.Collections;

public class TurnChildLightOn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "turnOn")
            GetComponent<Light>().intensity = 8f;
    }
}
