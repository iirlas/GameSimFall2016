using UnityEngine;
using System.Collections;

public class SwitchControl : MonoBehaviour {

    public int solvedNum = 0;
    public static int counter = 0;
    public GameObject exitDoor;

   
    private float speed;
    private Vector3 start;
    private Vector3 destination;

    public AudioSource OpenStoneDoorSoundEffect;
    bool playDoorOpenSoundOnce = false;

	// Use this for initialization
	void Awake () {
      
      speed = 2f;
      destination = new Vector3(exitDoor.transform.position.x,
                              (exitDoor.gameObject.GetComponent<Collider>().bounds.size.y + exitDoor.transform.position.y * 4), //wtf why did I have to times it by 2?
                                exitDoor.transform.position.z);
      start = exitDoor.transform.position;

	
	}
	
	// Update is called once per frame
	void Update () {

        if (counter > 0 && counter == solvedNum)
        {
            Debug.Log("Open the door");
            if (exitDoor.transform.position.y < destination.y)
          {
             exitDoor.transform.position = Vector3.Lerp(exitDoor.transform.position,
                                       destination,
                                       speed * 3.0f * Time.deltaTime);
				if (this.playDoorOpenSoundOnce == false && this.OpenStoneDoorSoundEffect != null) {
					this.OpenStoneDoorSoundEffect.Play ();
					this.playDoorOpenSoundOnce = true;
				}

          }
       
        }

	
	}
}
