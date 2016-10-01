using UnityEngine;
using System.Collections;

public class SafeRoomMusicSwitch : MonoBehaviour {

	public AudioSource safeRoom;
	public AudioSource levelMusic;
	public float fadeTime = .9f;
	int safeRoomNumber = 0;
	float volumeOfSafeRoom;
	float volumeOfLevelMusic;
	bool hasEntered = false;

	void Start()
	{
		volumeOfSafeRoom = this.safeRoom.volume;
		volumeOfLevelMusic = this.levelMusic.volume;
	}

	void OnTriggerEnter(Collider col){
		if (col.transform.tag == "Player" && !safeRoom.isPlaying && !hasEntered)
		{
			hasEntered = true;
			levelMusic.Stop();
			this.levelMusic.volume = this.volumeOfLevelMusic;
			safeRoom.Play ();
		}
	}

//	void OnTriggerEnter(Collider col){
//		if (col.gameObject.GetComponent<Girl> () != null) 
//		{
//			this.safeRoom.volume = this.volumeOfSafeRoom;
//			if ( safeRoom.isPlaying
//			this.safeRoomNumber = 1;
//		}
//
//	}
//
	void OnTriggerExit(Collider col){
		if (col.transform.tag == "Player" && hasEntered) {
			hasEntered = false;
			safeRoom.Stop();
			volumeOfLevelMusic = this.levelMusic.volume;
			levelMusic.Play();
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
