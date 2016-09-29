using UnityEngine;
using System.Collections;
//========================================================================================================
//                                              Match Wheel
// This Script checks if the wkeels are matched (correct) and opens the door
// This script uses BASIC TRIGGER to match the panel and then destroy the functionality of the lever to turn it
//========================================================================================================
public class MatchWheel : MonoBehaviour
{
    [Tooltip("where the lever script is placed")]
    public GameObject platform;
    [Tooltip("The exit door")]
    public GameObject exitDoor;
    [Tooltip("Number of wheels to match")]
    public int numOfWheels;
    public static int wheelsSolved;
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (numOfWheels == wheelsSolved)
            Destroy(exitDoor);

    }

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "matchedPanel")
        {
            Debug.Log("Matched!");
            LeverPlaceAndRotateWheel script = platform.GetComponent<LeverPlaceAndRotateWheel>();
            if (script != null)
            {
                Destroy(platform.GetComponent<LeverPlaceAndRotateWheel>());
            }
            else
            {
                Debug.Log("shouldDestroy");
                Destroy(platform.GetComponent<RotateWheel>());
            }

            Destroy(platform.GetComponent<BasicTrigger>());
            wheelsSolved++;

            
        }
    }
}
