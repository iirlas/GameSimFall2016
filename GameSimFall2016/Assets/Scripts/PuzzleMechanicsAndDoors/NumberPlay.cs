using UnityEngine;
using System.Collections;

public class NumberPlay : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Kira"))
        {

            if (GetComponent<Animator>().GetBool("MoveDown") != true)
            {
                this.GetComponent<Animator>().SetBool("MoveDown", true);
            }
        }
    }
}
