using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TutorialPopups : MonoBehaviour {

    private Text tutText;


	// Use this for initialization
	void Start () {

        tutText = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "Tut1")
        {
            tutText.text = "It looks much safer in the light. I should head to a torch.";
        }
        else if (trigger.message == "TutFearMeter")
        {
            tutText.text = "If I stay in the light I feel less afraid...\n Watch your Fear Meter. If it hits 100, you start taking physical damage.";
        }
        else if (trigger.message == "Tut2")
        {
            tutText.text = "What's a bunny doing here? I should protect it! \n Press right click to lock on, left click to shoot.";
        }
        else if (trigger.message == "TutPanelUp")
        {
            tutText.text = "It doesn't look like I can jump that. Maybe Bunny can! \n Press Q to switch between bunny and Kira, \npress SPACE as bunny to Jump";
        }
        else if (trigger.message == "Tut3")
        {
            tutText.text = "There are two plates. \nI wonder if I step on one and send bunny to the other if the door will open?";
        }
        else if (trigger.message == "Tut4")
        {
            tutText.text = "There's nothing here...I feel safe.";
        }
        else if (trigger.message == "Tut5")
        {
            tutText.text = "How do I get across? \n Maybe bunny can get to the other side?";
        }
        else if (trigger.message == "portal")
        {
            tutText.text = "There's no turning back now...";
        }
        else if (trigger.message == "empty")
            tutText.text = "";
    }
}
