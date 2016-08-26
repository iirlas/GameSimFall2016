using UnityEngine;
using System.Collections;

public class SoundDelay : MonoBehaviour {

	public AudioSource soundEffect;
	public float timeSec = 2f;
	public float delay;

	void Start ()
	{
		soundEffect.Play ();
		delay = timeSec;
	}

	void Update () 
	{
		//soundEffect.PlayDelayed (1.0f);	
		if (soundEffect.isPlaying == false && timeSec <= 0.0f) 
		{
			soundEffect.Play ();
			timeSec = delay;
		}
		if (soundEffect.isPlaying == false) {
			timeSec -= Time.deltaTime;
		}
	}
}
