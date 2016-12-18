using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Controls and Contains the state of the Game.
public class Game : Singleton<Game> 
{

    public enum GameState
    {
        CUTSCENE,
        PAUSE,
        PLAY,
    }

    public enum InputState
    {
        KEYBOARD,
        JOYSTICK
    }

    public GameState gameState;

    public InputState inputState;

    //------------------------------------------------------------------------------------------------
    // Use this for initialization
    override protected void Init () 
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
	}

    //------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        // Pressing the F10 key will toggle the Mouse's Locked mode and visibility.
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Cursor.lockState = (Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None);
            Cursor.visible = !Cursor.visible;
        }

        // Pressing the Escape key will toggle the pause menu.
        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseUI.getInstance().pauseToggle();
        }

    }

    //------------------------------------------------------------------------------------------------
    // Used to quit the application.
    // Note: Only used for a complete build, not usable in the Editor.
    public void Quit()
    {
        Application.Quit();
    }

    public void OnGUI()
    {
        //set current  input state
        if (Event.current.isKey || Event.current.isMouse)
        {
            inputState = InputState.KEYBOARD;
        }
        else
        {
            bool isJoystick = false;
            for ( KeyCode key = KeyCode.JoystickButton0; key != KeyCode.JoystickButton19 && !isJoystick; key++ )
            {
                isJoystick |= Input.GetKey(key);
            }


            if ( isJoystick )
            {
                inputState = InputState.JOYSTICK;
            }
        }
    }
}
