using UnityEngine;
using System.Collections;

public class Slant : MonoBehaviour {

    public void moveUp ()
    {
        transform.position += Vector3.up;
    }

    public void moveDown ()
    {
        transform.position += Vector3.down;
    }
}
