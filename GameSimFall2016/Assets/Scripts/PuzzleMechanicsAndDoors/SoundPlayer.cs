using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

    public void Invoke (string name)
    {
        SoundManager.getInstance().playAtPosition(name, transform.position);
    }
}
