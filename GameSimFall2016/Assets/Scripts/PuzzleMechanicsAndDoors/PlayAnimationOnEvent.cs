using UnityEngine;
using System.Collections;

//========================================================================================================
//                                       Play Animation on Event
// This script uses a BASIC TRIGGER to Play the animation on the THIS object                                           
//========================================================================================================

public class PlayAnimationOnEvent : MonoBehaviour {
    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "playAnimationOnce")
        {
            anim.SetBool("playAnimation", true);
        }
    }
}
