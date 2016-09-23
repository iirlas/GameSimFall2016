using UnityEngine;
using System.Collections;

//this script is used to inflict fear damage and then heal fear damage based on darkened rooms. 
//This script is used for rooms that should give damage UNLESS you're in the campfire light.
//The room should have a pressure plate at the door to start damage, any lights that are safe and one light at the end of the room(on the door)
//to trigger opening the door and end the damage possibility.

public class RoomFearDamage : MonoBehaviour
{

    private bool increaseFear;
    private bool decreaseFear;
    public bool isRoomEnd;
    private Timer tim;
    // Use this for initialization
    void Start()
    {
        increaseFear = false;
        decreaseFear = false;
        tim = new Timer();
        tim.start();

    }

    // Update is called once per frame
    void Update()
    {
        if (increaseFear)
        {
            //do fear damage

            if (tim.elapsedTime() % 1 == 0)
            {
                if (GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().fearCurrent != GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().fearMax)
                    GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().modifyFear(2);
            }

        }
        if (decreaseFear)
        {

            if (tim.elapsedTime() % 1 == 0)
            {
                if (GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().fearCurrent != GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().fearMin)
                    GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().modifyFear(-4);
            }


        }

    }

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "fearDamage")
        {
            increaseFear = true;
            decreaseFear = false;
        }
        if (trigger.message == "healFear")
        {
            increaseFear = false;
            decreaseFear = true;
        }

    }

    void OnEventEnd(BasicTrigger trigger)
    {
        if (trigger.message == "healFear")
        {
            if (!(this.isRoomEnd))
            {
                decreaseFear = false;
                increaseFear = false;
            }
            else
            {
                decreaseFear = false;
                increaseFear = true;
            }
        }
    }
}
