using UnityEngine;
using System.Collections;

public class PlayAnimationOnEvent : MonoBehaviour {
    Animator anim;

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
