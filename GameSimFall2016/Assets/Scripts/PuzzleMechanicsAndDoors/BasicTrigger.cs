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

    public Tag activator = "Untagged";
    public MonoBehaviour effected;
    public Type type;
    public string message;
    public bool canRepeat = false;

    public bool isActive
    {
      get;
      private set;
    }

    public BasicTrigger()
    {
       isActive = true;
    }

    public virtual void OnAction ()
    {
       if (isActive && type == Type.ACTION)
       {
          effected.SendMessage("OnEvent", this);
          isActive = false;
       }
    }

    public virtual void OnActionEnd ()
    {
       if (!isActive && type == Type.ACTION)
       {
          effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
          isActive = (canRepeat ? true : isActive);
       }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isActive && type == Type.COLLISION && collision.transform.tag == activator)
        {
            effected.SendMessage("OnEvent", this);
            isActive = false;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (!isActive && type == Type.COLLISION && collision.transform.tag == activator)
        {
            effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
            isActive = (canRepeat ? true : isActive);
        }
    }

	public void OnTriggerEnter(Collider other)
	{
        print("Trigger");
        if (isActive && type == Type.TRIGGER && other.transform.tag == activator)
        {
            effected.SendMessage("OnEvent", this);
            isActive = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!isActive && type == Type.TRIGGER && other.transform.tag == activator)
        {
            effected.SendMessage("OnEventEnd", this, SendMessageOptions.DontRequireReceiver);
            isActive = (canRepeat ? true : isActive);
        }
    }
}
