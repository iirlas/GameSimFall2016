using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransportTo : MonoBehaviour {

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
            SceneManager.LoadScene("TestOverworld");
        }
    }
}
