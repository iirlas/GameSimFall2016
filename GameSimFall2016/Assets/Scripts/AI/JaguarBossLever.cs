using UnityEngine;
using System.Collections;

public class JaguarBossLever : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   // 
   void OnTriggerEnter(Collider other)
   {
      GameObject[] arr = GameObject.FindGameObjectsWithTag("Waterfall");

      for (int ix = 0; ix < arr.Length; ix++)
      {

      }
   }
}
