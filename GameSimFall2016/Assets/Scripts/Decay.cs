using UnityEngine;
using System.Collections;

public class Decay : MonoBehaviour {

    public float life = 100.0f;
    public float decaySpeed = 1.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        life -= decaySpeed * Time.deltaTime;
        if ( life < 0 )
        {
            Destroy(gameObject);
        }
	}
}
