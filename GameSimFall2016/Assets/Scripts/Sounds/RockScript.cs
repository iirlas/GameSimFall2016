using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour {

	public AudioSource rockSlideSound;
	public string animationName = "SpikeWall05";
	Vector3 transformCheck;
	Vector3 originalTransform;
	bool isAnimationOn = false;

	Animator anim;

	void Start(){
		this.transformCheck = this.transform.position;
		this.originalTransform = this.transform.position;
		this.anim = GetComponent<Animator> ();
	}


	void Update(){

		if (this.transform.position != this.transformCheck && this.isAnimationOn == false) {
			if (this.anim.GetCurrentAnimatorStateInfo (0).IsName (this.animationName)) {
				this.rockSlideSound.Play ();
				Debug.Log ("Sound is on");
				this.isAnimationOn = true;
			}
		} else if (this.transform.position == this.transformCheck && isAnimationOn == true) {
			this.isAnimationOn = false;
			this.rockSlideSound.Stop ();
			Debug.Log ("Reset All values");

		}

	}
}