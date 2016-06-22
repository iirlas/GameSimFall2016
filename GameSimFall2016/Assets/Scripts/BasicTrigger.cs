using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Basic On Trigger
//  This script can be used as a holding ground for very basic on enter triggers.
//  It's implementation takes a public object (the thing to be moved onto the trigger) and has the script
//  applied to the trigger item.
//  By calling seperate methods for seperate incidents, instead of coding for each trigger, we can cut down
//  on script bulk. This script is easily expandable or editable for edition of tags or adding trigger exit and stay events.
//
//  22/6: The current additional function, works to move pressure plates down when triggered, respawn does just that.
// 
//
//========================================================================================================

public class BasicTrigger : MonoBehaviour {
	

	public GameObject triggering;
	public Vector3 respawnPt; // only needed  if trigger is deathZone
	void OnTriggerEnter(Collider other)
	{
		if (triggering.tag == "Player" && this.tag == "DeathZone")
		{
			Debug.Log ("DEAD");
			reSpawn ();
		}
		else if (triggering.name == other.name) 
		{
			Debug.Log ("It's triggered!");

			if (this.tag == "PushIn") 
			{
				pushIt ();
			}
		}
	}

	void pushIt ()
	{
		
		this.transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
		Debug.Log ("all's in");
	}

	void reSpawn ()
	{
		triggering.transform.position = respawnPt;
		
	}

}
