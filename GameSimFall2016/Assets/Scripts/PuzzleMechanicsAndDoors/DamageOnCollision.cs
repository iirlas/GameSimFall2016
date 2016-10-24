using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Damage on Collision
// This script should be applied to an item that gives damage ON CONTACT
// It then applies the public damage num to health and checks if the player is still colliding
// Damage continues until the collider is exited.
//========================================================================================================



public class DamageOnCollision : MonoBehaviour
{
   private bool takeDamage;
   public int damageToTake;
   public bool isTrigger = false;
   [Tooltip("Tick this box if you wish to deal fire damage instead of normal damage.")]
   public bool dealFireDamage;
   
   [Tooltip("How often to deal damage, interval in seconds.  Does nothing is dealFireDamage is active.")]
   public float intervalTime = 0.0f;
   private float timer = 0.0f;

   // Use this for initialization
   void Awake()
   {
      takeDamage = false;
   }

   // Update is called once per frame
   void Update()
   {
      if (takeDamage)
      {
         if (timer > intervalTime)
         {
            dealDamage();
            timer = 0.0f;
         }
      }

      timer += Time.deltaTime;
   }

   // Called when another collider collides with this collider
   void OnCollisionEnter(Collision other)
   {
      if (!this.isTrigger && other.collider.name.Equals("Kira"))
      {
         if (this.dealFireDamage)
         {
            this.fireDamage(true);
         }
         else
         {
            takeDamage = true;
         }
      }

   }

   // Called when another collider stops colliding with this collider.
   void OnCollisionExit(Collision other)
   {
      if (!this.isTrigger)
      {
         takeDamage = false;
      }
      if (this.dealFireDamage)
      {
         this.fireDamage(false);
      }
   }

   // Called when another collider enters this collision zone.
   void OnTriggerEnter(Collider other)
   {
      if (this.isTrigger && other.name.Equals("Kira"))
      {
         if ( this.dealFireDamage )
         {
            this.fireDamage(true);
         }
         else
         {
            takeDamage = true;
         }
      }
   }

   // Called when another collider exits this collision zone.
   void OnTriggerExit(Collider other)
   {
      if (this.isTrigger)
      {
         takeDamage = false;
      }

      if (this.dealFireDamage)
      {
         this.fireDamage(false);
      }
   }

   // Do damage to the player
   void dealDamage()
   {
      StatusManager.getInstance().fireDamage = this.damageToTake;
      StatusManager.getInstance().health -= damageToTake; 
   }

   // Activate or Deactivate the fire damage effect.
   void fireDamage(bool state)
   {
      StatusManager.getInstance().onFire = state;
   }
}
