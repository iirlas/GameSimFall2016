using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{
    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "pushIn")
        {
            transform.Translate(0, -.1f, 0);
        }
    }

    void OnEventEnd (BasicTrigger trigger)
    {
        if ( trigger.message == "pushIn" )
        {
            transform.Translate(0, .1f, 0);
        }
    }
}