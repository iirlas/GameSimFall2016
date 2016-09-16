using UnityEngine;
using System.Collections;

public class SnakeRattleLoop : MonoBehaviour {

	public AudioSource rattleAudioSource;

	// Triggers the Sound to play
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && rattleAudioSource.loop == true) {
			this.rattleAudioSource.Play ();
		}
	}

	//Triggers the sound loop to off so that it may play once
	void OnTriggerExit(Collider col)
	{
		//this.rattleAudioSource.Stop ();
		this.rattleAudioSource.loop = false;
	}

	// when the sound stops playing at the end it will turn the loop back on so when the player gets close to ti again.
	void Update()
	{
		if (rattleAudioSource.isPlaying == false) {
			this.rattleAudioSource.loop = true;
		}
	}



}
