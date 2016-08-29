using UnityEngine;
using System.Collections;

public class RespawnFloor : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "respawn")
        {
            player.transform.position = transform.position;
        }
    }
}
