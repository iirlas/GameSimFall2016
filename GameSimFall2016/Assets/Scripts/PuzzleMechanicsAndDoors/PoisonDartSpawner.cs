using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoisonDartSpawner : MonoBehaviour {

    private List<Transform> spawners = new List<Transform>();

    public GameObject poisonDartPrefab;
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
        for ( int index =0; index < transform.childCount; index++ )
        {
            spawners.Add(transform.GetChild(index));
        }
	}

    // Update is called once per frame
    public void fire ()
    {
        foreach ( Transform spawner in spawners )
        {
            GameObject poisonDart = Instantiate(poisonDartPrefab, spawner.position, spawner.rotation) as GameObject;
            poisonDart.GetComponent<Rigidbody>().velocity = transform.forward * speed;

        }
    }
}
