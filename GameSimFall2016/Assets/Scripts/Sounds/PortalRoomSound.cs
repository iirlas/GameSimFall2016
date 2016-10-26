using UnityEngine;
using System.Collections;

public class PortalRoomSound : MonoBehaviour {

	public float time = 3f;
	AudioSource gameLevelMusic;
	//float musicLevelVolume;
	int fadeType = 0;

	void Awake (){
		gameLevelMusic = GameObject.Find ("GameMusic").GetComponent<AudioSource> ();
		//musicLevelVolume = gameLevelMusic.volume;
	}


	void Update(){

		if (fadeType == 1) {
			fadeOutMusic (gameLevelMusic);
		} else if (fadeType == 2) {
			fadeInMusic (gameLevelMusic);
		}
	}


	void OnTriggerEnter(Collider col){
		if (PlayerManager.getInstance ().currentPlayer is Girl && col.gameObject.tag.Equals ("Player")) {
			fadeType = 1;
		}
	}

	void OnTriggerExit(Collider col){
		if (PlayerManager.getInstance ().currentPlayer is Girl && col.gameObject.tag.Equals ("Player")) {
			fadeType = 2;
		}
	}

	void fadeOutMusic(AudioSource music){

			music.volume -= (Time.deltaTime / this.time);
			Debug.Log ("Sound Fade Out: " + music.volume);

			if (music.volume < .05f){
				this.fadeType = 0;
			}
		}

		void fadeInMusic(AudioSource music){
			music.volume +=  (Time.deltaTime / this.time);
			Debug.Log ("Sound Fade In: " + music.volume);

			if(music.volume >  .4){
				this.fadeType = 0;
				}
		}
}
