using UnityEngine;
using System.Collections;

public class SoundLocation : MonoBehaviour {

	// Use this for initialization
	public GameObject Location;
	public AudioSource soundEffect;



	// When player collides sound will be player at location passed in.
	void OnTriggerEnter(Collider col){
	//	AudioSource.PlayClipAtPoint (soundEffect, Location.transform.position);
		soundEffect.Play();

	}


	//create a function to play and refernce the position of that the gameobject.

}
