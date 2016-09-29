using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Basic Trigger
// Can be used as a trigger, collision, or action
// This script is to be placed on the object that causes a script to fire. It sends a message (String)
// The inspector calls for the tag of the activator (what can set off the message)
//                         *the Game Object with the attached script describing the action that needs to happen
//                         the Type (trigger, collision, action)
//                         the message
//                         a boolean as to whether the action can be repeated
//
//
// *The item needs a script that looks for OnEvent(BasicTrigger trigger)
//                                         { if (trigger.message == "chosenMessage") { do this} }
//========================================================================================================

public class BasicTrigger : MonoBehaviour
{

    public enum Type
    {
        TRIGGER,
        COLLISION,
        ACTION,
    };

    enum State
    {
       ENTER,
       EXIT,
       DONE
    }

    public Tag activatorTag = "Untagged";
    [HideInInspector]
    public GameObject activator;
    public MonoBehaviour effected;
    public Type type;
    public string message;
    public bool canRepeat = false;

    private State myNextState;

    public BasicTrigger()
    {
       myNextState = State.ENTER;
    }

    public void OnAction ()
    {
       if (myNextState == State.ENTER && type == Type.ACTION)
       {
          myNextState = State.EXIT;
          effected.SendMessage("OnEvent", this);
       }
    }

    public void OnActionEnd ()
    {
       if (myNextState == State.EXIT && type == Type.ACTION)
       {
          myNextState = (canRepeat ? State.ENTER : State.DONE);
          effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
       }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (myNextState == State.ENTER && type == Type.COLLISION && collision.transform.tag == activatorTag)
        {
            myNextState = State.EXIT;
            activator = collision.gameObject;
            effected.SendMessage("OnEvent", this);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (myNextState == State.EXIT && type == Type.COLLISION && 
            collision.transform.tag == activatorTag && collision.gameObject.Equals(activator))
        {
            myNextState = (canRepeat ? State.ENTER : State.DONE);
            effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
        }
    }

	public void OnTriggerEnter(Collider other)
	{
        if (myNextState == State.ENTER && type == Type.TRIGGER && other.transform.tag == activatorTag)
        {
            myNextState = State.EXIT;
            activator = other.gameObject;
            effected.SendMessage("OnEvent", this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (myNextState == State.EXIT && type == Type.TRIGGER && 
            other.transform.tag == activatorTag && other.gameObject.Equals(activator))
        {
            myNextState = (canRepeat ? State.ENTER : State.DONE);
            effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
        }
    }
}
