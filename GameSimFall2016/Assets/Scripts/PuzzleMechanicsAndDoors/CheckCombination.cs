using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Check Combination
// This script checks whether a combination has been succesfully cracked.
// IT DOES NOT REQUIRE THE SOLUTION TO BE IN ORDER
//
// The script uses Lights turned on and off to check if the solution has been solved
// See ToDo for creating a randomized combinitation each time.
//========================================================================================================

public class CheckCombination : MonoBehaviour {
    [Tooltip("All parent objects to be interacted with")]
    public GameObject[] allLights;
    //public Light[] allLights;
    [Tooltip("Number of elements in solution")]
    public int numOfSolution;
    [Tooltip("Door to Exit from")]
    public GameObject exitDoor;
    private int[] comboNums;

	public AudioSource stoneDoorOpenSoundEffect;
	bool playOnlyOnce = false;

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

				if (this.playOnlyOnce == false && this.stoneDoorOpenSoundEffect != null){

					this.stoneDoorOpenSoundEffect.Play ();
					Debug.Log ("Door Opens");
					this.playOnlyOnce = true;
				}
            }
            else
            {
                foreach (GameObject indLight in allLights)
                {
                    indLight.GetComponent<Light>().intensity = 0;
					indLight.GetComponent<AudioSource> ().Stop ();
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
