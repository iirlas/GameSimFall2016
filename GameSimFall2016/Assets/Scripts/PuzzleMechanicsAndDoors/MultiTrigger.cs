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

    public bool canRepeat = false;
    public BasicTrigger[] triggers;
    public TriggerEvent onEvent = new TriggerEvent();
    public TriggerEvent onEventEnd = new TriggerEvent();


    public MultiTrigger()
    {
       myNextState = State.ENTER;
    }

    public void OnEvent ( BasicTrigger trigger )
    {
        if (myNextState == State.ENTER && ++triggerCount == triggers.Length)
        {
            myNextState = State.EXIT;
            onEvent.Invoke(trigger);
        }
    }

    public void OnEventEnd ( BasicTrigger trigger )
    {
        if (--triggerCount == (triggers.Length - 1))
        {
           myNextState = (canRepeat ? State.ENTER : State.DONE);
           onEventEnd.Invoke(trigger);
        }
    }

}
