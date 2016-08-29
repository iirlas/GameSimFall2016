using UnityEngine;
using System.Collections;

public class RespawnFloor : MonoBehaviour {

    public GameObject respawnPoint;

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
            this.gameObject.transform.Translate(respawnPoint.transform.position);
        }
    }
}
