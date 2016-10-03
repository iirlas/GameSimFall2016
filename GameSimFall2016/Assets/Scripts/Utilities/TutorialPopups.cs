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

        //JAGUAR TEMPLE TEXTS
        else if (trigger.message == "jagFront")
        {
            tutText.text = "What? \n Another temple? \n Mommy...are you here?";
        }
        else if (trigger.message == "jagFlood")
        {
            tutText.text = "Looks like the path flooded...";
        }
        else if (trigger.message == "birdIntro")
        {
            tutText.text = "Oh! A Quetzal! Maybe it will help me? \n Switch to between the bird, bunny, and Kira by pressing Q. \nPress SPACE to fly. Watch your stamina!";
        }
        else if (trigger.message == "targetTut")
        {
            tutText.text = "Hm. Looks like those targets control the lights...\nMaybe if they're all lit the door will open?";
        }
        else if (trigger.message == "turnWheel")
        {
            tutText.text = "Looks like I can't get through there...\n Maybe if I turn the wheel? \n Press space to turn the wheel";
        }
        else if (trigger.message == "bunnyWalls")
        {
            tutText.text = "Doesn't look I can get through here...maybe bunny can?";
        }
        else if (trigger.message == "stopWheel")
        {
            tutText.text = "I bet that pressure plate will stop the walls for Kira to get through!";
        }
        else if (trigger.message == "strangeWriting")
        {
            tutText.text = "I wonder who could have left that writing...";
        }
        else if (trigger.message == "colorSequence")
        {
            tutText.text = "That looks like a sequence on the wall.\n Maybe if I pick the next block in the sequence the door will open";
        }
        else if (trigger.message == "portalNew")
        {
            tutText.text = "Another one of these? \n Guess there's only one way to find out where it goes...";
        }
        else if (trigger.message == "portal")
        {
            tutText.text = "There's no turning back now...";
        }
        else if (trigger.message == "catTalk")
        {
            tutText.text = "Congratulations for completing the demo...\nUnfortunately Kira's parent's are in another Castle!\nI mean temple. Another temple.";
        }
        else if (trigger.message == "empty")
            tutText.text = "";
    }
}
