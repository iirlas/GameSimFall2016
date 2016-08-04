using UnityEngine;
using System.Collections;

public class Slant : MonoBehaviour {

	// Update is called once per frame
	void OnEvent (BasicTrigger instigator) 
    {
        switch(instigator.message)
        {
        case "UP":
            transform.position += Vector3.up;
            break;
        case "DOWN":
            transform.position += Vector3.down;
            break;
        }
	}
}
