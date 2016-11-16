using UnityEngine;
using System.Collections;

public class SingleUseMultiples : MonoBehaviour {
   //public int size;
   public GameObject[] startPos;
   public GameObject[] endPos;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   void OnTriggerEnter(Collider other)
   {
      if (other.name.Equals("Kira"))
      {
         for (int i = 0; i < startPos.Length; i++)
         {
            if (startPos[i].transform.position != endPos[i].transform.position)
            {
               startPos[i].transform.position = endPos[i].transform.position;
            }
         }
      }
   }
}
