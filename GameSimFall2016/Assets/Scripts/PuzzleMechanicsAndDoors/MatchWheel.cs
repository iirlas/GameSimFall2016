using UnityEngine;
using System.Collections;

public class MatchWheel : MonoBehaviour
{
    public GameObject platform;
    public GameObject exitDoor;
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
