using UnityEngine;
using System.Collections;
//using UnityEditor.SceneManagement;

public class SoundScenePlay : MonoBehaviour {

	/*public AudioSource levelMusic;


	public AudioClip outsideMusic;
	public AudioClip TitleScreenMusic;
	public AudioClip insideCave;
	public AudioClip theLoneCampsite;


	string nameTest = "";
	bool OnlyOnce = false;



	void Awake () 
	{
		this.levelMusic.Stop ();
		this.levelMusic.clip = insideCave;
		this.levelMusic.Play ();
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
			this.levelMusic.Stop ();
			this.levelMusic.volume = .4f;
			this.levelMusic.clip = this.outsideMusic;
			this.levelMusic.Play ();
			OnlyOnce = true;
		}
		if (this.nameTest.Equals("TestOverworld") && OnlyOnce == false) {
			this.levelMusic.Stop ();
			this.levelMusic.volume = 1f;
			this.levelMusic.clip = theLoneCampsite;
			this.levelMusic.Play ();
			OnlyOnce = true;
		}
		if (this.nameTest.Equals("ReduxTutorialTemple") && OnlyOnce == false) {
			this.levelMusic.Stop ();
			this.levelMusic.volume = 1f;
			this.levelMusic.clip = this.insideCave;
			this.levelMusic.Play ();
			OnlyOnce = true;
		}
		if (this.nameTest.Equals ("TitleScreen") && this.OnlyOnce == false) {
			this.levelMusic.Stop ();
			this.levelMusic.volume = 1f;
			this.levelMusic.clip = this.TitleScreenMusic;
			this.levelMusic.Play ();
			OnlyOnce = true;
		}


		DontDestroyOnLoad (levelMusic);

	}
     */
}
