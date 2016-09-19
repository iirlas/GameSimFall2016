using UnityEngine;
using System.Collections;

public class JaguarBossLever : MonoBehaviour {

   //private JaguarBoss jaguarBoss;

	// Use this for initialization
	void Start () {
      //jaguarBoss = GameObject.FindGameObjectWithTag("Boss").GetComponent<JaguarBoss>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   //==========================================================================
   // 
   void OnTriggerExit(Collider other)
   {
      if(other.name.Equals("Kira"))
      {
         Debug.Log("doing the thing");
         GameObject[] arr = GameObject.FindGameObjectsWithTag("Waterfall");

         for (int ix = 0; ix < arr.Length; ix++)
         {
            Debug.Log("Waterflow activated: " + arr[ix].gameObject.name);
            arr[ix].GetComponent<JaguarWaterfall>().deactivateWaterFlow();
         }
      }
   }

   void OnTriggerStay(Collider other)
   {
      if (Input.GetButtonDown("Action") && other.name.Equals("Kira"))
      {
         Debug.Log("doing the thing");
         GameObject[] arr = GameObject.FindGameObjectsWithTag("Waterfall");

         for (int ix = 0; ix < arr.Length; ix++)
         {
            if (arr[ix].GetComponent<JaguarWaterfall>().isJaguarInWaterfall)
            {
               Debug.Log("Waterflow activated: " + arr[ix].gameObject.name);
               arr[ix].GetComponent<JaguarWaterfall>().activateWaterFlow();
            }
         }
      }
   }
}
