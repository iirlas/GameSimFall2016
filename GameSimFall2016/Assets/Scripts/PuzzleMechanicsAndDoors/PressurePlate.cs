﻿using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "pushIn")
        {
            this.gameObject.transform.Translate(0, -.1f, 0);
        }
    }
}