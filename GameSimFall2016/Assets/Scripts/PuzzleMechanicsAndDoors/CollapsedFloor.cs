using UnityEngine;
using System.Collections;

public class CollapsedFloor : MonoBehaviour {

    public GameObject floor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Kira"))
        {
            floor.SetActive(false);
        }
    }
}
