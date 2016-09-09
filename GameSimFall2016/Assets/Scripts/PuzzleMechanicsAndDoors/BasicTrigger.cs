using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Basic On Trigger
//  This script can be used as a holding ground for very basic on enter triggers.
//  It's implementation takes a public object (the thing to be moved onto the trigger) and has the script
//  applied to the trigger item.
//  By calling seperate methods for seperate incidents, instead of coding for each trigger, we can cut down
//  on script bulk. This script is easily expandable or editable for edition of tags or adding trigger exit and stay events.
//
//  22/6: The current additional function, works to move pressure plates down when triggered, respawn does just that.
// 
//
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
