using UnityEngine;
using System.Collections;

public class CheckLights : MonoBehaviour {

    public GameObject exitDoor;
    public Light[] desiredLights; 

    private int count;
    

	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Light myLight in desiredLights)
        {
            if (myLight.intensity > 0)
                count++;
        }

        if (count == desiredLights.Length)
        {
            Debug.Log("countMet");
            exitDoor.SetActive(false);

        }
        else
        {
            count = 0;
        }
	
	}
}
