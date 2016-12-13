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

    private AsyncOperation asyncOperation;

    private bool isRunning = false;
	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void OnEvent(BasicTrigger trigger)
    {
        if (!isRunning)
        {
            StartCoroutine(Utility.fadeScreen(Color.clear, Color.black, 0.1f, 0.0f));
            StartCoroutine(loadAsync());
        }
    }

    public void OnEvent()
    {
        if (!isRunning)
        {
            StartCoroutine(Utility.fadeScreen(Color.clear, Color.black, 0.1f, 0.0f));
            StartCoroutine(loadAsync());
        }
    }

    IEnumerator loadAsync ()
    {
        isRunning = true;
        
        asyncOperation = SceneManager.LoadSceneAsync(nameOfNext);
        asyncOperation.allowSceneActivation = false;
        
        while (Utility.isFading)
        {
            //print("prog: " + asyncOperation.progress);
            yield return new WaitForEndOfFrame();
        }

        asyncOperation.allowSceneActivation = true;

        isRunning = false;
        yield return null;

    }
}
