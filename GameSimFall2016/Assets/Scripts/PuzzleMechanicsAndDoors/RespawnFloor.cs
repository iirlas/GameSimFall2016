using UnityEngine;
using System.Collections;

//RespawnFloor code
//Creator: Pat Sipes
//Version 2.0
//Description: This code works in conjunction with the basicTrigger script. 
//It is placed on the empty object to be respawned to.
//Death floor must have the basic trigger script send the message "respawn" and be repeatable.

public class RespawnFloor : MonoBehaviour {

    
    private GameObject[] playerUnits; //to collect the player units together
     

	// Use this for initialization
	void Start () {
        playerUnits = GameObject.FindGameObjectsWithTag("Player"); //populate array

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEvent(BasicTrigger trigger) //Basic Trigger script
    {
        
        //if (trigger.message == "respawn")
        {
            

            foreach (GameObject unit in playerUnits)
            {
                if (trigger.activator.Equals(unit))
                {
                    unit.transform.position = transform.position; //respawn to designated respawn block
                }
            }
            //TODO: change HealthPlayer to singleton
            GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().modifyHealth(-10);
            //player.transform.position = transform.position;
        }
    }
}
