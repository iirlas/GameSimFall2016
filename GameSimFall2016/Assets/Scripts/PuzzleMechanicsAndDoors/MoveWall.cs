using UnityEngine;
using System.Collections;

//========================================================================================================
//                                     Move Wall
// This Script is specifically for the wall move puzzle with the rabbit.
// This script turns the wheel if it is the beginning move, 
//calls the animator with a "wheelTurned" transition boolean to begin animation.       
//========================================================================================================

public class MoveWall : MonoBehaviour {
    [Tooltip("Each Wall to begin Movement on BASIC TRIGGER")]
    public GameObject[] walls;
    public GameObject wheel;
    private bool beginMove;
    private Animator myanim;


    // Use this for initialization
    void Awake() {

        beginMove = false;
       


    }


    // Update is called once per frame
    void Update() {
        if (beginMove)
        {
            if (Input.GetButtonDown("Action"))
            {
                

                foreach (GameObject wall in walls)
                {
                    myanim = wall.GetComponent<Animator>();
                    myanim.SetBool("wheelTurned", true);
                }
            }

        }

    }

    public void OnEvent(BasicTrigger trigger)
    {
        //if (trigger.message == "turn")
        {
             beginMove = true;
            wheel.transform.Rotate(0, 0, 360 / 36);
        }

    }
}
