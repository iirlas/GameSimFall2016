using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float damage = 1.0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StatusManager.getInstance().health -= damage;
            Destroy(gameObject);
        }
    }
}
