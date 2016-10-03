using UnityEngine;
using System.Collections;

public class StopWall : MonoBehaviour {
    bool endMove;
    [Tooltip("The object that starts the animation")]
    public GameObject switchWheel;
    public GameObject invisiWall;
    [Tooltip("Each Wall to begin Movement on BASIC TRIGGER")]
    public GameObject[] walls;
	// Use this for initialization
	void Start () {
        endMove = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (endMove)
        {

            foreach (GameObject wall in walls)
            {
                Animator myanim = wall.GetComponent<Animator>();
                myanim.SetBool("puzzleSolved", true);
            }

        }
	
	}

    void OnEvent(BasicTrigger trigger)
    {

        if (trigger.message == "stopWalls")
        {
            endMove = true;
            Destroy(switchWheel.GetComponent<MoveWall>());
            Destroy(invisiWall);
        }
    }
}
