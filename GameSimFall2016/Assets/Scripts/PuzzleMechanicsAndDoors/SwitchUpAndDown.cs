using UnityEngine;
using System.Collections;

public class SwitchUpAndDown : MonoBehaviour {

    //TODO: make the items children and call the children? Maybe?
    //TODO: animate/LERP
    public GameObject platform1;
    public GameObject start1;
    public GameObject end1;
    public GameObject platform2;
    public GameObject start2;
    public GameObject end2;

    private bool movePlatform;


    // Use this for initialization
    void Awake () {

        movePlatform = false;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (movePlatform)
        {
            if (platform1.transform.position == start1.transform.position)
            {
                SwitchControl.counter++;
                Debug.Log("PlatformUP");
                platform1.transform.position = end1.transform.position;
            }
            else
            {
                SwitchControl.counter--;
                platform1.transform.position = start1.transform.position;
            }

         if (platform2 != null)
         {
            if (platform2.transform.position == start2.transform.position)
            {
               SwitchControl.counter++;
               platform2.transform.position = end2.transform.position;
               movePlatform = false;
            }
            else
            {
               SwitchControl.counter--;
               platform2.transform.position = start2.transform.position;
               movePlatform = false;
            }
         }
        }
                
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HIT");
            movePlatform = true;
        }
    }
}
