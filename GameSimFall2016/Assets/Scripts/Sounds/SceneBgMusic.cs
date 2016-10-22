using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneBgMusic : MonoBehaviour {

    public AudioSource levelMusic;
    public AudioClip secondaryMusic;


    // Use this for initialization
    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1 && this.levelMusic.isPlaying == false)
        {
            Destroy(this.gameObject);
            this.gameObject.GetComponentInParent<SoundScenePlay>().enabled = false;
        }

        levelMusic.Stop();
        levelMusic.clip = secondaryMusic;
        levelMusic.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
