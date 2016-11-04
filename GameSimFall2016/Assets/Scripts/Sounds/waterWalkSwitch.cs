using UnityEngine;
using System.Collections;

public class waterWalkSwitch : MonoBehaviour {

	public GameObject player;
	public AudioSource walking;
	public AudioClip waterWalking;
	AudioClip defaultWalking;
	bool isAnotherCollider;
	BoxCollider otherCollider;

	void Awake () {
		this.defaultWalking = this.walking.clip;
	}

	// Changes the walking based on the name of the level.
	void OnTriggerEnter(Collider col){
		Debug.Log ("Enters Collider");
		if (this.walking.clip != waterWalking) {
			if (col.gameObject.tag.Equals (player.tag)) {
				Debug.Log ("Made it to the Level Floor");
				this.walking.clip = this.waterWalking;
			}
		}
	}

	void OnTriggerExit(Collider col){
		Debug.Log ("Is exiting!");

		if (col.gameObject.tag.Equals (player.tag)) {
			Debug.Log ("is exiting and switching sound.");
			this.walking.clip = this.defaultWalking;

		}
	}
}
