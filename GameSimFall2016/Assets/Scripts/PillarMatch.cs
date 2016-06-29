using UnityEngine;
using System.Collections;

public class PillarMatch : MonoBehaviour {
    public int matchNumber;
    private int counter = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RotateMatch")
        {
            counter++;
            Debug.Log(counter);
        }

        if (counter == matchNumber-1)
        {
            Debug.Log("It's a Match!");
            PillarRotate.locked = true;
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "RotateMatch")
        {
            counter--;
            Debug.Log(counter);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
