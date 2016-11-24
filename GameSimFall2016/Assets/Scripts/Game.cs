﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// Use this for initialization
	override protected void Init () 
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Cursor.lockState = (Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None);
            Cursor.visible = !Cursor.visible;
        }


        if ( Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseUI.getInstance().pauseToggle();
        }

    }

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
