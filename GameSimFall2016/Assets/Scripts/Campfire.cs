using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
   public float healSpeed = 15f;
   private Transform respawnPoint;

   //==========================================================================
   // Use this for initialization
   void Awake()
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
         StatusManager.getInstance().respawnPoint = this.respawnPoint.position;
         //GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().campfireRespawn = this.respawnPoint;
         if (StatusManager.getInstance().hasStatus(StatusManager.Status.FEAR))
         {
            Debug.Log("ping");
            StatusManager.getInstance().ToggleStatus(StatusManager.Status.FEAR);
         }
         //GameObject.FindGameObjectWithTag("FearManager").GetComponent<StatusManager>().ToggleStatus();
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

   void OnTriggerExit(Collider other)
   {
      if (other.gameObject.name.Equals("Kira"))
      {
         if (!StatusManager.getInstance().hasStatus(StatusManager.Status.FEAR))
         {
            StatusManager.getInstance().ToggleStatus(StatusManager.Status.FEAR);
         }
      }
   }

   //==========================================================================
   // heals the player
   void healPlayer()
   {
      StatusManager.getInstance().health += healSpeed * Time.deltaTime;
      StatusManager.getInstance().fear -= healSpeed * Time.deltaTime;
   }
}
