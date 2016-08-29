//Creator: Patricia Sipes
//Date: 29/8/2016
//What to attach to: The Exit Door (to be destroyed)
//Purpose: Attached to MultiPlate.cs (see it for more info)
//         Quick and dirty way of storing hitCounts. 
//   WILL BE REMOVED AND EDITED TO MATCH BROADCAST SYSTEM 

using UnityEngine;
using System.Collections;

public class PlateCounter : MonoBehaviour {

    public static int plateHits;
    public int numOfPlates;

	// Use this for initialization
	void Start () {
        plateHits = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (plateHits == numOfPlates)
        {
            Destroy(this.gameObject);
        }

    }
}
