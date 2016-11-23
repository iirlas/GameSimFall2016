using UnityEngine;
using System.Collections;

public class RisingBlockPuzzle : MonoBehaviour {

   public BasicTrigger trigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      if (StatusManager.getInstance().health <= 0)
      {
         trigger.OnAction();
      }
   }

}
