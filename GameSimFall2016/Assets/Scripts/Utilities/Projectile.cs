using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float speed = 2.0f;
    public float damage = 1.0f;
	public Transform target;


	//------------------------------------------------------------------------------------------------
	void FixedUpdate ()
	{
		GetComponent<Rigidbody> ().velocity = (target.position - transform.position) * speed;
	}

    //------------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            StatusManager.getInstance().health -= damage;
            Destroy(gameObject);
        }
    }
}
