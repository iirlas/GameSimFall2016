using UnityEngine;
using System.Collections;

public class NumberPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   void OnTriggerEnter(Collider other)
   {
      if (other.name.Equals("Kira"))
      {
         Debug.Log("Hit");
         Debug.Log(GetComponent<Animator>().GetBool("MoveDown"));
         if (GetComponent<Animator>().GetBool("MoveDown") != true)
         {
            Debug.Log("NotDown");
            this.GetComponent<Animator>().SetBool("MoveDown", true);
         }
      }
   }
}
