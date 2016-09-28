using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Application.Quit(); // Quits the game
        }
    }
}
