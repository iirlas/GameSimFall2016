using UnityEngine;
using System.Collections;

public class TurnOnOff : MonoBehaviour
{
    public GameObject darkness;
    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))

            darkness.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            darkness.SetActive(false);
    }


}