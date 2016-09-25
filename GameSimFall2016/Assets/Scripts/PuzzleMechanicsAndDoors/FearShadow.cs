using UnityEngine;
using System.Collections;

public class FearShadow : MonoBehaviour {

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
