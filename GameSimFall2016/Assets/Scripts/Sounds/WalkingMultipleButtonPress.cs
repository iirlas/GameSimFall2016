using UnityEngine;
using System.Collections;

public class WalkingMultipleButtonPress : MonoBehaviour {

	AudioSource walkingAudio;
	void Start () 
	{
		walkingAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.W) == true && Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == false && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}


		} else if (Input.GetKey (KeyCode.W) == true && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == false && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		} else if (Input.GetKey (KeyCode.W) == true && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		} else if (Input.GetKey (KeyCode.W) == true && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == true) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		} else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == false && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		} else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		}else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == true && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == true) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		}else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == false) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		}else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == true && Input.GetKey (KeyCode.S) == true) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		}else if (Input.GetKey (KeyCode.W) == false && Input.GetKey (KeyCode.D) == false && Input.GetKey (KeyCode.A) == false && Input.GetKey (KeyCode.S) == true) {
			if (walkingAudio.isPlaying == false) {

				walkingAudio.Play ();
			}

		}else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.A)|| Input.GetKeyUp(KeyCode.S))
		{
			walkingAudio.Stop ();
		}
	}
}
