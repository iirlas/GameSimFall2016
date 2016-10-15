using UnityEngine;
using System.Collections;

public class SafeRoomMusicSwitch : MonoBehaviour {

	public AudioSource safeRoom;
	AudioSource levelMusic;
	public float fadeTime = .9f;
	int safeRoomNumber = 0;
	float volumeOfSafeRoom;
	float volumeOfLevelMusic;
	//bool hasEntered = false;

	void Start()
	{
		this.levelMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
		volumeOfSafeRoom = this.safeRoom.volume;
		volumeOfLevelMusic = this.levelMusic.volume;
	}

	void OnTriggerEnter(Collider col){
		//if (col.transform.tag == "Player" && !safeRoom.isPlaying && !hasEntered)
		if(col.gameObject.name == GameObject.Find("Kira").name)
		{
			safeRoomNumber = 1;
			this.safeRoom.volume = this.volumeOfSafeRoom;
			safeRoom.Play ();
		}
	}


	void OnTriggerExit(Collider col){
		if (col.gameObject.name == GameObject.Find("Kira").name) {
			safeRoomNumber = 2;
			this.levelMusic.volume = .02F;
			Debug.Log ("volume of Level music is: " + levelMusic.volume);
			levelMusic.Play ();
		}
	}

	void Update()
	{
		if (this.safeRoomNumber == 1) {
			fadeOutMusic (levelMusic);
		} else if (this.safeRoomNumber == 2) {
			fadeOutMusic (safeRoom);
		}

	}

	void fadeOutMusic(AudioSource music){
		music.volume -=  Time.deltaTime * this.fadeTime;
		if (music.volume == 0) {
			music.Stop ();
			this.safeRoomNumber = 0;

		}
	}
}
