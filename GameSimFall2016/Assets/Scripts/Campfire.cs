using UnityEngine;
using System.Collections;

public class Campfire : MonoBehaviour
{

   private Transform respawnPoint;

   //==========================================================================
   // Use this for initialization
   void Start()
   {
      respawnPoint = this.GetComponentInChildren<Transform>();
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {

   }

   //==========================================================================
   // When the player enters the trigger zone, set to current spawn point
   void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.name.Equals("Kira"))
      {
         GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().campfireRespawn = this.respawnPoint;
         Debug.Log("Respawn point changed");
      }
   }

   //==========================================================================
   // While the player stays in the trigger zone, heal the player.
   void OnTriggerStay(Collider other)
   {
      if (other.gameObject.name.Equals("Kira"))
      {
         healPlayer();
      }
   }

   //==========================================================================
   // heals the player
   void healPlayer()
   {
      GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().modifyHealth(1);
      GameObject.FindGameObjectWithTag("HealthManager").GetComponent<FearManager>().modifyFear(-1);
   }
}
