using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TutorialPopups : MonoBehaviour {

    private Text tutText;


	// Use this for initialization
	void Awake () {

        tutText = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //this need to be in a text file...
    public void OnEvent (string message)
    {
        if (message == "Tut1")
        {
            tutText.text = "It looks much safer in the light. I should head towards the fire.";
        }
        else if (message == "TutFearMeter")
        {
            tutText.text = "If I stay in the light I feel less afraid...\n Watch your Fear Meter. If it hits 100, you start taking physical damage.";
        }
        else if (message == "Tut2")
        {
            tutText.text = "What's a bunny doing here? I should protect it! \n Press right click to lock on, left click to shoot.";
        }
        else if (message == "TutPanelUp")
        {
            tutText.text = "It doesn't look like I can jump that. Maybe Bunny can! \n Press Q to switch between bunny and Kira, \npress SPACE as bunny to Jump";
        }
        else if (message == "Tut3")
        {
            tutText.text = "There are two plates. \nI wonder if I step on one and send bunny to the other if the door will open?";
        }
        else if (message == "Tut4")
        {
            tutText.text = "There's nothing here...I feel safe.";
        }
        else if (message == "Tut5")
        {
            tutText.text = "How do I get across? \n Maybe bunny can get to the other side?";
        }

        //JAGUAR TEMPLE TEXTS
        else if (message == "jagFront")
        {
            tutText.text = "What? \n Another temple? \n Mommy...are you here?";
        }
        else if (message == "jagFlood")
        {
            tutText.text = "Looks like the path flooded...";
        }
        else if (message == "birdIntro")
        {
            tutText.text = "Oh! A Quetzal! Maybe it will help me? \n Switch to between the bird, bunny, and Kira by pressing Q. \nPress SPACE to fly. Watch your stamina!";
        }
        else if (message == "targetTut")
        {
            tutText.text = "Hm. Looks like those targets control the lights...\nMaybe if they're all lit the door will open?";
        }
        else if (message == "turnWheel")
        {
            tutText.text = "Looks like I can't get through there...\n Maybe if I turn the wheel? \n Press space to turn the wheel";
        }
        else if (message == "bunnyWalls")
        {
            tutText.text = "Doesn't look I can get through here...maybe bunny can?";
        }
        else if (message == "stopWheel")
        {
            tutText.text = "I bet that pressure plate will stop the walls for Kira to get through!";
        }
        else if (message == "strangeWriting")
        {
            tutText.text = "I wonder who could have left that writing...";
        }
        else if (message == "colorSequence")
        {
            tutText.text = "That looks like a sequence on the wall.\n Maybe if I pick the next block in the sequence the door will open";
        }
        else if (message == "portalNew")
        {
            tutText.text = "Another one of these? \n Guess there's only one way to find out where it goes...";
        }
        else if (message == "portal")
        {
            tutText.text = "There's no turning back now...";
        }
        else if (message == "catTalk")
        {
            tutText.text = "Congratulations for completing the demo...\nUnfortunately Kira's parent's are in another Castle!\nI mean temple. Another temple.";
        }
        else if (message == "empty")
            tutText.text = "";
    }
}
