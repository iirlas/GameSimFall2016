using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

   public int mainMenuScene = 0;
   public int infoScene = 1;
   public int firstScene = 2;
   public int creditScene = 8;

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
