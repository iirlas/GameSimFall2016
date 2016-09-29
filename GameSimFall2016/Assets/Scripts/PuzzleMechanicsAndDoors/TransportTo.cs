using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


//========================================================================================================
//                                              Transport To
// This script uses BASIC TRIGGER
// Upon hitting an item, this script causes transportation to the defined level
// Name of level desired to be transported to is seen in the Inspector.
//========================================================================================================

public class TransportTo : MonoBehaviour {
    [Tooltip("Name of Level to be Transported To")]
    public string nameOfNext;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "nextLevel")
        {
            SceneManager.LoadScene(nameOfNext);
        }
    }
}
