using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class InformationScreen : MonoBehaviour {

	// Use this for initialization
	void Start ()
   {

   }

   // Update is called once per frame
	void Update () {
      if (Input.anyKey)
      {
         SceneManager.LoadScene(1);
      }
	}
}
