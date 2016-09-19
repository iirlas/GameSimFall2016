using UnityEngine;
using System.Collections;

public class openWall : MonoBehaviour {

    
    public GameObject[] walls;
    private Animator myAnim;

	// Use this for initialization
	void Start () {
        
	
	}
	
	// Update is called once per frame
	void Update () {
        
	
	}

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "openWalls")
        {
            foreach (GameObject wall in walls)
            {
                myAnim = wall.GetComponent<Animator>();
                myAnim.SetBool("puzzleSolved", true);
            }
        }
    }
}
