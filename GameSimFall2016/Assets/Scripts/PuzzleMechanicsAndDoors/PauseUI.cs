using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * Terrible terrible class 
*/

public class PauseUI : Singleton<PauseUI>
{
    private Canvas pauseCanvas;

    private List<Component> pausedItems = new List<Component>();

    // Use this for initialization
    override protected void Init()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void pauseToggle()
    {
        pauseSet(!pauseCanvas.enabled);
    }

	public void cursorToggle ()
	{
		Cursor.lockState = (Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None);
		Cursor.visible = (Cursor.lockState == CursorLockMode.None);
	}

	public void cursorSet (bool show)
	{
		Cursor.lockState = (!show ? CursorLockMode.Locked : CursorLockMode.None);
		Cursor.visible = (show);
	}

    public void pauseSet(bool status)
    {
        pauseCanvas.enabled = status;

        if (pauseCanvas.enabled)
        {
            Debug.Log("Za Warudo");
            Time.timeScale = 1;
            foreach (Component component in GameObject.FindObjectsOfType<Component>())
            {
                bool isValid = !(component is Camera) && !(component is Renderer) &&
                               component.transform.root != transform.root;
                if (isValid)
                {
                    pausedItems.Add(component);

                    //component is a behaviour
                    if (component is Behaviour && (component as Behaviour).enabled && 
                        (component as Behaviour).transform.root != transform.root)
                    {
                        (component as Behaviour).enabled = false;
                    }
                    else if ( component is Rigidbody )
                    {
                        (component as Rigidbody).Sleep();
                    }
                }
            }
        }
        else
        {
            //pauseCanvas.transform.FindChild("ControlsImage").GetComponent<UnityEngine.UI.Image>().enabled = false;		
            Time.timeScale = 1;
            foreach (Component component in pausedItems)
            {
                if ( component is Behaviour )
                {
                    (component as Behaviour).enabled = true;
                }
                else if (component is Rigidbody)
                {
                    (component as Rigidbody).WakeUp();//wake me up...
                }
            }
            pausedItems.Clear();
        }
    }
}
