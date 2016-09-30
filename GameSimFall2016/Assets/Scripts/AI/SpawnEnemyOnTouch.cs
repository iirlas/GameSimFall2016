using UnityEngine;
using System.Collections;

public class spawnEnemyOnTouch : MonoBehaviour
{

    public GameObject theEnemy;
    public GameObject spawnPOS;

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
            GameObject tempEnemy = Instantiate(theEnemy, spawnPOS.transform.position, Quaternion.identity) as GameObject;
        }
    }
}
