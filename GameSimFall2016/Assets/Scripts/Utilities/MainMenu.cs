using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

   public string mainMenuScene = "TitleScreen";
   public string infoScene = "ControlScene";
   public string firstScene = "IntroSequence";
   public string creditScene = "CreditsScene";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

   public void pressPlay()
   {
      SceneManager.LoadScene(firstScene);
   }

   public void pressInstructions()
   {
      SceneManager.LoadScene(infoScene);
   }

   public void pressCredits()
   {
      SceneManager.LoadScene(creditScene);
   }

   public void pressTitle()
   {
      SceneManager.LoadScene(mainMenuScene);
   }

   public void pressQuit()
   {
      Application.Quit();
   }

   
}
