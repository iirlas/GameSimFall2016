using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class SoundScenePlay : MonoBehaviour {

	public AudioSource levelMusic;


	public AudioClip testStartingArea;
	public AudioClip titleScreenMusic;
	public AudioClip reduxTutorialTemple;
	public AudioClip testOverworld;
	public AudioClip creditMusic;



	string nameTest = "";
	bool OnlyOnce = false;


	//destroys the game object if there is two.
	void Awake(){
		if (FindObjectsOfType (GetType ()).Length > 1 && this.levelMusic.isPlaying == false) {
			Destroy (this.gameObject);
			this.gameObject.GetComponentInParent<SoundScenePlay> ().enabled = false;
			Debug.Log ("Destroyed something");
		}

	}
		

	// Update is called once per frame
	void Update () 
	{
		if (nameTest != EditorSceneManager.GetActiveScene ().name) {
			nameTest = EditorSceneManager.GetActiveScene ().name;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			OnlyOnce = false;
		}
		// For change of songs only
		// changes the song of the player to play another one based on the name of the scene.
		if (this.nameTest.Equals ("TestStartingArea") && this.OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = 1f;
			levelMusic.clip = this.testStartingArea;
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("TestOverworld") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .5f;
			levelMusic.clip = this.testOverworld;
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("ReduxTutorialTemple") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .4f;
			levelMusic.clip = this.reduxTutorialTemple;
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("JaguarSetup") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .4f;
			levelMusic.clip = this.reduxTutorialTemple;
			levelMusic.Play ();
			OnlyOnce = true;

		}
			
		if (this.nameTest.Equals ("TitleScreen") && this.OnlyOnce == false) {
			if (this.levelMusic.clip != this.titleScreenMusic) {
				levelMusic.Stop ();
				levelMusic.volume = .7f;
				levelMusic.clip = this.titleScreenMusic;
				levelMusic.Play ();
				OnlyOnce = true;
			}

		}
		if (this.nameTest.Equals ("CreditsScene") && this.OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = 1f;
			levelMusic.clip = this.creditMusic;
			levelMusic.Play ();
			OnlyOnce = true;
		}
			
		//DontDestroyOnLoad (this);
			
	}
}
