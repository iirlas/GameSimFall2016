using UnityEngine;
using System.Collections;

public class Passable : MonoBehaviour {

    private Collider myCollider;

    [HideInInspector]
    new public Collider collider
    {
        get
        {
            if ( myCollider == null )
            {
                myCollider = GetComponent<Collider>();
            }
            return myCollider;
        }
    }

    public GameObject[] targets = new GameObject[1];

	// Use this for initialization
	void Start () {

        for (int index = 0; index < targets.Length; index++ )
        {
            Physics.IgnoreCollision(collider, targets[index].GetComponent<Collider>());
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
