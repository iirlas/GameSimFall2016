using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour {

	public AudioSource soundEffect;

	void Start () {

	}


	void Update () {
		
	}

	public void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player" && soundEffect.isPlaying == false) {
			soundEffect.Play ();
		}
			
	}

	public void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player" && soundEffect.isPlaying == true) {
			soundEffect.loop = true;
		}
	}
	public void OnTriggerExit(Collider col)
	{
		if (soundEffect.loop == true) {
			soundEffect.loop = false;
		}
	}
}
