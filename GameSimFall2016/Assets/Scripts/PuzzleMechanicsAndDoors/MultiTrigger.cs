using UnityEngine;
using System.Collections;

public class MultiTrigger : MonoBehaviour {

   enum State
   {
      ENTER,
      EXIT,
      DONE
   }

    private int triggerCount = 0;
    private State myNextState;

    public MonoBehaviour effected;
    public string message;
    public bool canRepeat = false;
    public BasicTrigger[] triggers;


    public MultiTrigger()
    {
       myNextState = State.ENTER;
    }

    void OnEvent ( BasicTrigger trigger )
    {
        if (trigger.message != message)
        {
            throw new System.Exception("Message for [" + trigger.gameObject.name + "] does not match [" + name + "]");
        }
        if (myNextState == State.ENTER && ++triggerCount == triggers.Length)
        {
            myNextState = State.EXIT;
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
           myNextState = (canRepeat ? State.ENTER : State.DONE);
           effected.SendMessage("OnEventEnd", trigger, SendMessageOptions.DontRequireReceiver);
        }
    }

}
