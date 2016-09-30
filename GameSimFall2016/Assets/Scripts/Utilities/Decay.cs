using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour {

    public float life = 100.0f;
    public float decaySpeed = 1.0f;
    public bool destroyOnCollision = false;
    public bool destroyOnTrigger = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        life -= decaySpeed * Time.deltaTime;
        if ( life < 0 )
        {
            Destroy(gameObject);
        }
	}

    public void OnCollisionEnter(Collision collision)
    {
        if ( destroyOnCollision )
        {
            print(collision.transform.name);
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if ( destroyOnTrigger )
        {
            print(other.name);
            Destroy(gameObject);
        }
    }
}
