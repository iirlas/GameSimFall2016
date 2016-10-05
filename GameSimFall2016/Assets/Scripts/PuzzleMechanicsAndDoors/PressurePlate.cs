using UnityEngine;
using System.Collections;

//========================================================================================================
//                                     Pressure Plate
// Pushes the plate inwards on BASIC TRIGGER, pushes up on END BASIC TRIGGER                                         
//========================================================================================================

public class PressurePlate : MonoBehaviour
{
    public void OnEvent(BasicTrigger trigger)
    {
        //if (trigger.message == "pushIn")
        {
            transform.Translate(0, -.1f, 0);
        }
    }

    public void OnEventEnd (BasicTrigger trigger)
    {
        //if (trigger.message == "pushIn")
        {
            transform.Translate(0, .1f, 0);
        }
    }
}