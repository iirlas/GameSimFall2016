using UnityEngine;
using System.Collections;

public class JaguarBossLever : MonoBehaviour {

   private GameObject jaguarBoss;

   public GameObject[] waterfalls = new GameObject[4];

	// Use this for initialization
	void Start () {

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
         Debug.Log("Kira leaves the lever zone");


         for (int ix = 0; ix < waterfalls.Length; ix++)
         {
            Debug.Log("Waterflow activated: " + waterfalls[ix].gameObject.name);
            waterfalls[ix].GetComponent<JaguarWaterfall>().deactivateWaterFlow();
         }
      }
   }

   void OnTriggerStay(Collider other)
   {
      if (Input.GetButtonDown("Action") && other.name.Equals("Kira"))
      {
         Debug.Log("Kira remains in the lever zone");

         for (int ix = 0; ix < waterfalls.Length; ix++)
         {
            if (waterfalls[ix].GetComponent<JaguarWaterfall>().isJaguarInWaterfall)
            {
               Debug.Log("Waterflow activated: " + waterfalls[ix].gameObject.name);
               waterfalls[ix].GetComponent<JaguarWaterfall>().activateWaterFlow();
            }
         }
      }
   }
}
