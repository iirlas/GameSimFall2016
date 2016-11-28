using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseUI : Singleton<PauseUI> {



   private Canvas pauseCanvas;

   private List<Component> pausedItems = new List<Component>();

	// Use this for initialization
	override protected void Init () {
      pauseCanvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void pauseToggle()
   {
      pauseSet(!pauseCanvas.enabled);
   }

   public void pauseSet(bool status)
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
}
