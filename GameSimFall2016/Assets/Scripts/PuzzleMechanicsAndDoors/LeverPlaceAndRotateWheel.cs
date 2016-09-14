﻿using UnityEngine;
using System.Collections;

public class LeverPlaceAndRotateWheel : MonoBehaviour {

    public Item lever;
    
    public GameObject wheel;   //wheel to turn
    public int sides;         // number of sides so as to rotate just one section
    public int tallness;      // height that needs to be pushed in
    private bool turnWheel;   // can the wheel be turned?



    // Use this for initialization
    void Start () {
        turnWheel = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (turnWheel)
        {
            if (Input.GetButtonDown("Action"))
            {
                wheel.transform.Rotate(0, 0, 360 / sides);
            }
        }


    }

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "placeLever")
        {
            if (PlayerManager.getInstance().items.ContainsValue(lever))
            {
                lever.transform.position = gameObject.transform.position;
                Vector3 vec = lever.transform.position;
                vec.y = lever.transform.position.y + 2;
                lever.transform.forward = transform.up;
                lever.transform.position = vec;
                lever.gameObject.SetActive(true);
                Debug.Log("leverDropped");
                PlayerManager.getInstance().items.Remove(lever.name);
                Destroy(lever.GetComponent<Item>());
                turnWheel = true;

            }
        }
    }



}
