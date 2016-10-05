﻿using UnityEngine;
using System.Collections;

public class TargetLightActivation : MonoBehaviour {

    public Light[] puzzleBoxes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEvent(BasicTrigger trigger)
    {
        //if (trigger.message == "Light")
        {
            Debug.Log("hitPanel");
            foreach (Light boxLight in puzzleBoxes)
            {
                Debug.Log(boxLight.intensity);
                if (boxLight.intensity < 1)
                    boxLight.intensity = 1;
                else

                    boxLight.intensity = 0;

                Debug.Log(boxLight.intensity);
            }
        }
    }
}
