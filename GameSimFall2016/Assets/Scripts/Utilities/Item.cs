using UnityEngine;
using System.Collections;
using System.Linq;

public class Item : MonoBehaviour {

    private Collider myCollider;

    [HideInInspector]
    new public Collider collider
    {
        get
        {
            if (myCollider == null)
            {
                myCollider = GetComponent<Collider>();
            }
            return myCollider;
        }
    }

    public Collider[] targets;

    public void OnCollisionEnter (Collision collision)
    {
        if (collision.transform.tag == "Player" && targets.Contains(collision.collider))
        {
            PlayerManager.getInstance().items.Add(transform.name, this);
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player" && targets.Contains(other))
        {
            PlayerManager.getInstance().items.Add(transform.name, this);
            gameObject.SetActive(false);
        }
    }
}
