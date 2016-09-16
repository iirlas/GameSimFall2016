using UnityEngine;
using System.Collections;

public class WalkingLoopOnButtonPressed : MonoBehaviour {


	public AudioSource walkingSound;

	void Update () 
	{

		if(Input.GetKeyDown(KeyCode.W)){
				walkingSound.Play ();
		}else if(Input.GetKeyUp(KeyCode.W)){
			walkingSound.Stop();
		}

	
	}
}
