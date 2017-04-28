using UnityEngine;
using System.Collections;

//========================================================================================================
//                                     Pressure Plate
// Pushes the plate inwards on BASIC TRIGGER, pushes up on END BASIC TRIGGER                                         
//========================================================================================================

public class PressurePlate : MonoBehaviour
{

   //public AudioSource buttonOnClick;
   //public AudioSource buttonOffClick;
   
   /*==========================================================================
    * Kira triggers the hitboxes for the pressure plates multiple times per frame
    * if you position her corrently.  In order to temporarily ammend this the
    * sounds will currently only play once
    */
   bool playOnceOnEvent = true;
   bool playOnceOnEnd = true;

   public void OnEvent(BasicTrigger trigger)
   {
      transform.Translate(0, -.1f, 0);
	  GetComponent<BoxCollider> ().center += new Vector3 (0, .1f, 0);
      Debug.Log("CLicked In Pressure Plate");
      if (this.playOnceOnEvent)
      {
         SoundManager.getInstance().playEffect("Slingshot_Pull_01");
         this.playOnceOnEvent = false;
      }
   }

   public void OnEventEnd(BasicTrigger trigger)
   {
      transform.Translate(0, .1f, 0);
	  GetComponent<BoxCollider> ().center -= new Vector3 (0, .1f, 0);
      if (this.playOnceOnEnd)
      {
         SoundManager.getInstance().playEffect("Slingshot_Pull_02");
         this.playOnceOnEnd = false;
      }
      //if (this.buttonOffClick != null) {
      //	this.buttonOffClick.Play ();
      //}
   }
}