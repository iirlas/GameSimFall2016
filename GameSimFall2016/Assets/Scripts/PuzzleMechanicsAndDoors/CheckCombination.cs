using UnityEngine;
using System.Collections;

public class CheckCombination : MonoBehaviour {

    public GameObject[] allLights;
    //public Light[] allLights;
    public int numOfSolution;
    public GameObject exitDoor;
    private int[] comboNums;

	// Use this for initialization
	void Start () {
        comboNums = new int[numOfSolution];
        //this is where randomization will happen, it selects a random number 0-whatever and places it in the comboNums array.
        //TODO this is hardcoded currently to 7041
        comboNums[0] = 7;
        comboNums[1] = 0;
        comboNums[2] = 4;
        comboNums[3] = 1;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (checkNumofLights() == numOfSolution)
        {
            Debug.Log("Lights On");
            if (checkLightsOn())
            {
                exitDoor.SetActive(false);
            }
            else
            {
                foreach (GameObject indLight in allLights)
                {
                    indLight.GetComponent<Light>().intensity = 0;
                }
            }
        }
        


	
	}

    private bool checkLightsOn()
    {
        foreach (int num in comboNums)
        {
            if (allLights[num].GetComponent<Light>().intensity < 1)
            {
                return false;
            }
        }
        return true;
    }

    private int checkNumofLights()
    {
        int numOn = 0;
        foreach (GameObject indLight in allLights)
        {
            if (indLight.GetComponent<Light>().intensity > 1)
                numOn++;
        }

        return numOn;
    }
}
