using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Fear Shadow
// This script is attached to anything that needs to apply fear damage (that misses the light collider)
// 
//========================================================================================================

public class FearShadow : MonoBehaviour {

    [Tooltip("damage to take")]
   public float fearDamage;
   FearManager myFearManager;

	// Use this for initialization
	void Start () {
      this.myFearManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   void OnTriggerStay(Collider other)
   {
      if (other.name.Equals("Kira"))
      {
         this.myFearManager.modifyFear(this.fearDamage);
      }
   }
}
