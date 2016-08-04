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
    public float distance = 1.0f;
    public bool canRepeat = false;

    private bool isActive = true;

    public void Start()
    {
        print(activator);
    }



    public void Update()
    {
        if ( isActive && type == Type.ACTION && Input.GetButtonDown("Action") )
        {
            Collider[] colliders = Physics.OverlapSphere( transform.position, distance );
            foreach ( Collider col in colliders )
            {
                if ( col.transform.tag == activator )
                {
                    effected.SendMessage("OnEvent", this);
                    isActive = false;
                }
            }
        }
    }

    public void LateUpdate()
    {
        isActive = (canRepeat ? true : isActive);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (isActive && type == Type.COLLISION && collision.transform.tag == activator)
        {
            effected.SendMessage("OnEvent", this);
            isActive = false;
        }
    }

	public void OnTriggerEnter(Collider other)
	{
        if (isActive && type == Type.TRIGGER && other.transform.tag == activator)
        {
            effected.SendMessage("OnEvent", this);
            isActive = false;
        }
    }
}
