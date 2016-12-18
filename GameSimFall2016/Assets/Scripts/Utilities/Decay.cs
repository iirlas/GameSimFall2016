﻿using UnityEngine;
using System.Collections;

// Destroys a GameObject after a set amount of time or when
// the GameObject collides with another.
public class Decay : MonoBehaviour {

    public float life = 100.0f;
    public float decaySpeed = 1.0f;
    public bool destroyOnCollision = true;

    //------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start () {
	
	}

    //------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void LateUpdate () {
        life -= decaySpeed * Time.deltaTime;
        if ( life < 0 )
        {
            Destroy(gameObject);
        }
	}

    //------------------------------------------------------------------------------------------------
    public void OnCollisionEnter(Collision collision)
    {
        if (destroyOnCollision)
        {
            print(collision.transform.name);
            Destroy(gameObject);
        }
    }

    //------------------------------------------------------------------------------------------------
    public void OnTriggerEnter(Collider other)
    {
        if (destroyOnCollision)
        {
            print(other.name);
            Destroy(gameObject);
        }
    }
}
