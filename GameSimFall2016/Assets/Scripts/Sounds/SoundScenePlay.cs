using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

		nameTest = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		/*if (FindObjectsOfType (GetType ()).Length > 1 && this.levelMusic.isPlaying == false) {
			Destroy (this.gameObject);
			this.gameObject.GetComponentInParent<SoundScenePlay> ().enabled = false;
			Debug.Log ("Destroyed something");
		}*/
	}
		

	// Update is called once per frame
	void Update () 
	{
		/*if (nameTest != EditorSceneManager.GetActiveScene ().name) {
			nameTest = EditorSceneManager.GetActiveScene ().name;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
		}*/
		// For change of songs only
		// changes the song of the player to play another one based on the name of the scene.
		if (this.nameTest.Equals ("TestStartingArea") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .8f;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			levelMusic.clip = this.testStartingArea;
			Debug.Log("Birds " + this.levelMusic.isPlaying);
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("TestOverworld") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .5f;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			Debug.Log("TestOverworld " + this.levelMusic.isPlaying);
			levelMusic.clip = this.testOverworld;
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("ReduxTutorialTemple")  && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .2f;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			Debug.Log("Redux " + this.levelMusic.isPlaying);
			levelMusic.clip = this.reduxTutorialTemple;
			levelMusic.Play ();
			OnlyOnce = true;

		}
		if (this.nameTest.Equals("JaguarSetup") && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .4f;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			Debug.Log("Jaguar " + this.levelMusic.isPlaying);
			levelMusic.clip = this.reduxTutorialTemple;
			levelMusic.Play ();
			OnlyOnce = true;

		}
			
		if (this.nameTest.Equals ("TitleScreen")  && OnlyOnce == false) {
			if (this.levelMusic.clip != this.titleScreenMusic) {
				levelMusic.Stop ();
				levelMusic.volume = .7f;
				Debug.Log ("The name of the Scene -" + nameTest + "-");
				Debug.Log("Title " + this.levelMusic.isPlaying);
				levelMusic.clip = this.titleScreenMusic;
				levelMusic.Play ();
				OnlyOnce = true;

			}

		}
		if (this.nameTest.Equals ("CreditsScene")  && OnlyOnce == false) {
			levelMusic.Stop ();
			levelMusic.volume = .5f;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			Debug.Log("credits " + this.levelMusic.isPlaying);
			levelMusic.clip = this.creditMusic;
			levelMusic.Play ();
			OnlyOnce = true;
		}
			
		//DontDestroyOnLoad (this);
			
	}
}
