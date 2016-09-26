using UnityEngine;
using System.Collections;

public class lightFocus : MonoBehaviour {

   private bool isLocked;

   [HideInInspector]
   public bool isControllingLight;
   [HideInInspector]
   public bool isFocused;

   public int focusedRadius = 5;
   public int unfocusedRadius = 50;

   //==========================================================================
	// Use this for initialization
	void Start () {
      this.isControllingLight = false;
	}

   //==========================================================================
	// Update is called once per frame
	void Update () {
      if (!isLocked)
      {
         if (Input.GetKeyDown(KeyCode.F) && isControllingLight)
         {
            if (isFocused)
            {
               isFocused = !isFocused;
               this.GetComponent<Light>().spotAngle = unfocusedRadius;
            }
            else
            {
               isFocused = !isFocused;
               this.GetComponent<Light>().spotAngle = focusedRadius;
            }
         }
      }
	}

   //==========================================================================
   // Lock this light into place, once it hits a mirror
   public void lockLight()
   {
      if ( this.isLocked == false )
         this.isLocked = true;   
   }
}
