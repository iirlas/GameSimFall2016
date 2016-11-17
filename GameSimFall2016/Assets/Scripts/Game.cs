using UnityEngine;
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

    private Canvas pauseCanvas;

    private List<Component> pausedItems = new List<Component>();

	// Use this for initialization
	override protected void Init () 
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseCanvas = GetComponent<Canvas>();
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
            pauseToggle();
        }

    }

    public void pauseToggle ()
    {
        pauseSet(!pauseCanvas.enabled);
    }

    public void pauseSet ( bool status)
    {
        pauseCanvas.enabled = status;

        if (pauseCanvas.enabled)
        {
            Debug.Log("Za Warudo");
            Time.timeScale = 0;
            foreach (Component component in GameObject.FindObjectsOfType<Behaviour>())
            {
                if (!(component is Renderer) &&
                    !(component is Camera) &&
                    (component as Behaviour).transform.root != transform)
                {
                    pausedItems.Add(component);
                    (component as Behaviour).enabled = false;
                }
            }
            
        }
        else
        {
            //pauseCanvas.transform.FindChild("ControlsImage").GetComponent<UnityEngine.UI.Image>().enabled = false;
            Time.timeScale = 1;
            foreach (Component component in pausedItems)
            {
                (component as Behaviour).enabled = true;
            }
            pausedItems.Clear();
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
