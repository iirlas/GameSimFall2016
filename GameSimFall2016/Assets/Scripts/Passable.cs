using UnityEngine;
using System.Collections;

public class Passable : MonoBehaviour {

    [HideInInspector]
    new public Collider collider;

    public MonoBehaviour[] targets;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();

        foreach ( MonoBehaviour target in targets )
        {
            Physics.IgnoreCollision(collider, target.GetComponent<Collider>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
