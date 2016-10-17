﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//========================================================================================================
//                                              Cursor Lock And Escape
// This script sets the cursor state on start to confine it to the window.
// On escape key, the whole application exits
// This script should be placed on the Director, it is unnecessary to add it anywhere else.
//========================================================================================================

public class CursorLock : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {

        
        
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
	    if (SceneManager.GetActiveScene().name.Equals("Name_Of_Your_Scene"))
            {
                Application.Quit();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("TitleScreen");
            }
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            //Application.Quit(); // Quits the game
        }
        


        
    }
}
