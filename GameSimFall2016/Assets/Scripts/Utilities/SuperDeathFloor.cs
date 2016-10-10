using UnityEngine;
using System.Collections;

public class SuperDeathFloor : MonoBehaviour
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
      if (other.gameObject.name.Equals("Kira"))
      {
         StatusManager.getInstance().health -= 10000;
      }
      else if (other.gameObject.tag.Equals("Player"))
      {
         other.transform.position = StatusManager.getInstance().respawnPoint;
      }
   }
}
