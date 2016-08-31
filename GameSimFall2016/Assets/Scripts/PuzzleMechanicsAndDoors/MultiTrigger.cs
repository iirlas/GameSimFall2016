using UnityEngine;
using System.Collections;

public class MultiTrigger : MonoBehaviour {

    private int triggerCount = 0;

    public MonoBehaviour effected;
    public string message;
    public BasicTrigger[] triggers;

    void OnEvent ( BasicTrigger trigger )
    {
        if (trigger.message != message)
        {
            throw new System.Exception("Message for [" + trigger.gameObject.name + "] does not match [" + name + "]");
        }
        if (++triggerCount == triggers.Length)
        {
            effected.SendMessage("OnEvent", trigger);
        }
    }

    void OnEventEnd ( BasicTrigger trigger )
    {
        if (trigger.message != message)
        {
            throw new System.Exception("Message for [" + trigger.gameObject.name + "] does not match [" + name + "]");
        }
        if (--triggerCount == (triggers.Length - 1))
        {
            effected.SendMessage("OnEventEnd", trigger);
        }
    }

}
