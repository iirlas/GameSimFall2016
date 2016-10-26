using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WalkingAudioSwap : MonoBehaviour {


	public AudioSource walkingSound;
	public AudioClip walkingOnGrass;
	public AudioClip walkingOnTile;

	string SceneName = "";

	void Awake () {
		SceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (SceneName.Equals ("TestStartingArea")) {
			walkingSound.clip = walkingOnGrass;
		} else {
			walkingSound.clip = walkingOnTile;
		}

	}
}
