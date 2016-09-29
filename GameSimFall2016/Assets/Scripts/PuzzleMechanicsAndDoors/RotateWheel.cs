using UnityEngine;
using System.Collections;

//========================================================================================================
//                                  Rotate Wheel
// Rotates the wheel using BASIC TRIGGER
//             
//========================================================================================================

public class RotateWheel : MonoBehaviour {
    [Tooltip("The Wheel to Turn")]
    public GameObject wheel;   //wheel to turn
    [Tooltip("Number of Sides (to figure out degrees)")]
    public int sides;         // number of sides so as to rotate just one section
    [Tooltip("Currently Unused")]
    public int tallness;      // height that needs to be pushed in
    private bool turnWheel; // can the wheel be turned?

    // Use this for initialization
    void Start () {
        turnWheel = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turnWheel)
        {
            if (Input.GetButtonDown("Action"))
            {
                wheel.transform.Rotate(0, 0, 360 / sides);
            }
        }

    }


    void OnEvent(BasicTrigger trigger)
    {
        turnWheel = true;
    }

    void OnEventEnd(BasicTrigger trigger)
    {
        turnWheel = false;
    }
}
