using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {

	public AudioSource soundEffect;

	void Start () {

	}

	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && soundEffect.isPlaying == false) {
			soundEffect.Play ();
		}
			
	}

	void OnTriggerStay(Collider col)
	{
		// once inside will trigger a continous loop.
		if (col.gameObject.tag == "Player" && soundEffect.isPlaying == true) {
			soundEffect.loop = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		// Turns off the loop in the sound loop but plays it one last time beforeand then it turns off.
		/*if (soundEffect.loop == true) {
			soundEffect.loop = false;
		}*/

		// Completely stops the loop once the player leaves the collider.
		soundEffect.Stop ();
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log ("Inside collision!!");
		if (col.gameObject.tag == "Player") {
			soundEffect.Play ();
		}
	}
}
