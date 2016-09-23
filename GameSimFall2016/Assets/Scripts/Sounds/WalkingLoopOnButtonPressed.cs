using UnityEngine;
using System.Collections;

public class WalkingLoopOnButtonPressed : MonoBehaviour {


	public AudioSource walkingSound;
	public AudioSource fastWalkingSound;

	void Update ()
	{
		/*	Button Sprint prototype for having two button pressed for the walking sound and fast running sounds.
			what happens is it only goes into it when you realase all the buttons and then press the buttons you want to press.
		*/

		/*if (Input.GetKeyDown (KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)) {
			if (fastWalkingSound.isPlaying == true) {
				fastWalkingSound.Stop ();
			}
			Debug.Log ("Made it inside the walking");
			walkingSound.Play ();				
		} else if (Input.GetKeyDown (KeyCode.W) && Input.GetKey (KeyCode.LeftShift)) {
			if (walkingSound.isPlaying == true) {
				walkingSound.Stop ();
			}
			Debug.Log ("Made it inside the fast walking");
			fastWalkingSound.Play ();
		}else if(Input.GetKeyUp(KeyCode.W)){
			walkingSound.Stop();
			fastWalkingSound.Stop ();
			Debug.Log ("it is reseting");
		}*/


		/*	Version 1.0
			Works Fine between switching between all states. But there is a problem when pressing the shift only. It starts
		 	to run or sprint when only the shift is pressed. it should sprint only when both the shift and the w is pressed.
			By far works better.
		---------------------------------------------------------------------------------------------------------------------------
			Version 1.1
			Works fine so far im able to switch between the keys really well and switches smoothly back and forth.
		*/

		if (Input.GetKey (KeyCode.W) == true && Input.GetKey(KeyCode.LeftShift) ==  false) {
			if (fastWalkingSound.isPlaying == true) {
				fastWalkingSound.Stop ();
			}
			if (walkingSound.isPlaying == false) {
				walkingSound.Play ();
			}
			Debug.Log ("Made it inside the walking");
		} else if (Input.GetKey (KeyCode.W) == true && Input.GetKey (KeyCode.LeftShift) == true) {
			if (walkingSound.isPlaying == true) {
				walkingSound.Stop ();
			}
			if (fastWalkingSound.isPlaying == false) {
	
				fastWalkingSound.Play ();
			}
			Debug.Log ("Made it inside the fast walking");
		}else if(Input.GetKeyUp(KeyCode.W)){
			walkingSound.Stop();
			fastWalkingSound.Stop ();
			Debug.Log ("it is reseting");
		}

		/*if (Input.GetKeyDown (KeyCode.W)) {
			Debug.Log ("Walking forward");
		} else if (Input.GetKeyUp (KeyCode.W)) {
			Debug.Log ("stop!");
		}*/
	}
}
