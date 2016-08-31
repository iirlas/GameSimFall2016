using UnityEngine;
using System.Collections;

public class SoundArray : MonoBehaviour {

	public AudioSource soundSource;
	public AudioClip[] soundArray;
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;
	public AudioClip sound4;
	public AudioClip sound5;
	public float timeSecDelay = 2f;
	float delay;
	int randomNumber;

	// saving sound clips into an array of sounds that can randomize different sounds for the enemy.
	void Start (){
		int ArrayLength = 5;
		soundSource = GetComponent<AudioSource> ();

		//******checks to see if the AudioClips are null before adding them to the array to be randomized.********************************

		if (sound1 == null) {
			ArrayLength--;
		}
		if (sound2 == null) {
			ArrayLength--;
		}
		if (sound3 == null) {
			ArrayLength--;
		}
		if (sound4 == null) {
			ArrayLength--;
		}
		if (sound5 == null) {
			ArrayLength--;
		}

		//********Only Concern is that they start in order from top to bottom not seperate.******************************
	
		soundArray = new AudioClip[ArrayLength];

		//Inputs the sounds into the Sound Array after there is a length to be determined.
		for (int index = 0; index < ArrayLength; index++) {
			if (index == 0) {
				soundArray [index] = sound1;
			} else if (index == 1) {
				soundArray [index] = sound2;
			} else if (index == 2) {
				soundArray [index] = sound3;
			} else if (index == 3) {
				soundArray [index] = sound4;
			} else if (index == 4) {
				soundArray [index] = sound5;
			}
		}

		randomNumber = Random.Range (0, soundArray.Length);
		Debug.Log (randomNumber);

		soundSource.clip = soundArray [randomNumber];
		delay = timeSecDelay;
		soundSource.Play ();
		soundSource.spatialBlend = 1.0f;
	}

	void Update () {
		
		// get the constant update position of the gameobject and use it to move the sound around with it.
		if (soundSource.isPlaying == false && timeSecDelay <= 0.0f) {
			
			// if there is a automatic One shot audio gameobject created it will be destroyed. because automaticlly ends when sound ends.
			if (GameObject.Find("One shot audio")) {
				Destroy (GameObject.Find ("One shot audio"));
			}
			randomNumber = Random.Range (0, soundArray.Length);
			Debug.Log ("SoundNumber" + randomNumber);
			soundSource.clip = soundArray [randomNumber];
			soundSource.Play ();
			timeSecDelay = delay;
		}
		soundSource.transform.position = this.gameObject.transform.position;
		if (soundSource.isPlaying == false && timeSecDelay > 0.0f) {
			timeSecDelay -= Time.deltaTime;

			//if gameobject exsist then it will move the position of it to follow the game object it is currently attached to.
			if (GameObject.Find ("One shot audio")) {
				//Debug.Log (GameObject.Find ("One shot audio").transform.position);
				//Debug.Log ("Made it inside the movement");
			}
		}
	}
}
