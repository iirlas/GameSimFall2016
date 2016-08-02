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

public class BasicTrigger : MonoBehaviour {

    public enum Type
    {
        TRIGGER,
        COLLISION,
        ACTION,
    };

    public GameObject triggerObject;
    public Type type;
    public float distance = 1.0f;

    public void Update()
    {
        if ( type == Type.ACTION && Input.GetButtonDown("Action") &&
             Vector3.Distance(transform.position, triggerObject.transform.position) < distance )
        {
            triggerObject.SendMessage("OnEvent", transform);
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if ( type == Type.COLLISION && collision.transform == triggerObject.transform )
        {
            triggerObject.SendMessage("OnEvent", transform);
        }
    }

	public void OnTriggerEnter(Collider other)
	{
        if ( type == Type.TRIGGER && other.transform == triggerObject.transform )
        {
            triggerObject.SendMessage("OnEvent", transform);
        }
    }
}
