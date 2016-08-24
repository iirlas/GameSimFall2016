using UnityEngine;
using System.Collections;

public class PillarRotate : MonoBehaviour {

    public GameObject pillarPiece;
    public int numOfSides;
    public static bool locked = false;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!locked)
            {
                pillarPiece.transform.Rotate(0, 0, 360 / numOfSides);
            }

        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
