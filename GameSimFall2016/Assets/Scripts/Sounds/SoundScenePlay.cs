using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;

public class SoundScenePlay : MonoBehaviour {

	public AudioSource levelMusic;


	public static AudioClip[] gameMusic = new AudioClip[2];
	public AudioClip outsideMusic;
	public AudioClip insideCave;
	string nameTest = "";
	bool OnlyOnce = false;



	void Awake () 
	{
		this.levelMusic.Stop ();
		this.levelMusic.clip = insideCave;
		this.levelMusic.Play ();
		/*for (int soundIndex = 0; soundIndex < gameMusic.Length; soundIndex++) {
			gameMusic[soundIndex] = this.o
		}*/
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (nameTest != EditorSceneManager.GetActiveScene ().name) {
			nameTest = EditorSceneManager.GetActiveScene ().name;
			Debug.Log ("The name of the Scene -" + nameTest + "-");
			OnlyOnce = false;
		}

		// changes the song of the player to play another one based on the name of the scene.
		if (this.nameTest.Equals("TestOverworld") && OnlyOnce == false) {
			this.levelMusic.Stop ();
			this.levelMusic.clip = outsideMusic;
			this.levelMusic.Play ();
			Debug.Log ("made it into the switch method");
			OnlyOnce = true;
		}


		DontDestroyOnLoad (levelMusic);

	}
}
