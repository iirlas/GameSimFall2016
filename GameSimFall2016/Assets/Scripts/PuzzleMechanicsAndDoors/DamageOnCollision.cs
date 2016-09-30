using UnityEngine;
using System.Collections;

public class DamageOnCollision : MonoBehaviour {
    private bool takeDamage;
	// Use this for initialization
	void Start () {
        takeDamage = false;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (takeDamage)
        {
            Invoke("takeFireDamage", 1f);

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name.Equals("Kira"))
        {
            takeDamage = true;
        }

    }

    void OnCollisionExit(Collision other)
    {
        takeDamage = false;
    }


    void takeFireDamage()
    {
        GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().modifyHealth(-5);
    }
}
