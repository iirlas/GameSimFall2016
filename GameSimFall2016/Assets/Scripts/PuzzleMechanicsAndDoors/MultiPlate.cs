//Creator: Patricia Sipes
//Date: 29/8/2016
//Purpose: This is a "quick fix" script to have the door open only if the player and their companion stand on two
//         seperate plates. The script works by matching the desired number of plates (see plateCounter script)
//         to plate hits (ontrigenter, exiting subtracts)
//         Destroys the door object upon both plates being stood on at the same time.
//   WILL BE REMOVED AND EDITED TO MATCH BROADCAST SYSTEM AND DOOR SCRIPT


using UnityEngine;
using System.Collections;

public class MultiPlate : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlateCounter.plateHits++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlateCounter.plateHits--;
        }
    }
}
