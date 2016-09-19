﻿using UnityEngine;
using System.Collections;

public class MoveWall : MonoBehaviour {

    public GameObject[] walls;
    private bool beginMove;
    private Animator myanim;


    // Use this for initialization
    void Start() {

        beginMove = false;


    }


    // Update is called once per frame
    void Update() {
        if (beginMove)
            if (Input.GetButtonDown("Action"))
            {
                //wheel.transform.Rotate(0, 0, 360 / sides);

                foreach (GameObject wall in walls)
                {
                    myanim = wall.GetComponent<Animator>();
                    myanim.SetBool("wheelTurned", true);
                }
            }
    }

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "turn")
        {
            beginMove = true;       
        }
    }
}
