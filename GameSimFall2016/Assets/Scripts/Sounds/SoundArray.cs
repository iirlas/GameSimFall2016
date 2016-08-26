using UnityEngine;
using System.Collections;

public class SoundArray : MonoBehaviour {

	AudioSource soundSource;
	public AudioClip[] soundArray;
	public AudioClip sound1;
	public AudioClip sound2;
	public AudioClip sound3;
	public float timeSec = 2f;
	float delay;
	int randomNumber;


	// saving sound clips into an array of sounds that can randomize different sounds for the enemy.
	void Start () 
	{
		soundSource = GetComponent<AudioSource> ();
		randomNumber = Random.Range (0, 3);
		Debug.Log (randomNumber);
		soundArray = new AudioClip[3];
		soundArray [0] = sound1;
		soundArray [1] = sound2;
		soundArray [2] = sound3;
		soundSource.clip = soundArray [randomNumber];
		AudioSource.PlayClipAtPoint(soundArray[randomNumber],this.gameObject.GetComponent<Transform>().position);
		delay = timeSec;
	}
		

	void Update () 
	{
		//soundEffect.PlayDelayed (1.0f);
		// get the constant update position of the gameobject and use it to move the sound around with it.
		if (soundSource.isPlaying == false && timeSec <= 0.0f) 
		{
			randomNumber = Random.Range (0, 3);
			Debug.Log (randomNumber);
			soundSource.clip = soundArray [randomNumber];
			//soundSource.Play ();
			AudioSource.PlayClipAtPoint(soundArray[randomNumber],this.gameObject.GetComponent<Transform>().position);
			timeSec = delay;
		}

	
		if (soundSource.isPlaying == false) {
			timeSec -= Time.deltaTime;
		}
	}
}
