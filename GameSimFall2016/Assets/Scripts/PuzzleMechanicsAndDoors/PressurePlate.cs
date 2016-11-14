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
   public void OnEvent(BasicTrigger trigger)
   {
      //if (trigger.message == "pushIn")
      {
         transform.Translate(0, -.1f, 0);
         Debug.Log("CLicked In Pressure Plate");
         //if (this.buttonOnClick != null) {
         //	this.buttonOnClick.Play ();
         //}
      }
   }

   public void OnEventEnd(BasicTrigger trigger)
   {
      //if (trigger.message == "pushIn")
      {
         transform.Translate(0, .1f, 0);
         //if (this.buttonOffClick != null) {
         //	this.buttonOffClick.Play ();
         //}
      }
   }
}