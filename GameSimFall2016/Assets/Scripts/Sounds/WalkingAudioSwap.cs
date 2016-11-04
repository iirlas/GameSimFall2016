using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WalkingAudioSwap : MonoBehaviour {


	public AudioSource walkingSound;
	public AudioClip walkingOnGrass;
	public AudioClip walkingOnTile;

	string SceneName = "";


	//Switches the walking audio when in different scenes.
	void Awake () {
		SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (SceneName.Equals ("TestStartingArea") && this.walkingOnGrass != null) {
			walkingSound.clip = walkingOnGrass;
		} else {
			if (walkingOnTile != null) {
				walkingSound.clip = walkingOnTile;
			}
		}

	}
}
