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
   
   [Tooltip("How often to deal damage, interval in seconds.")]
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

   void OnCollisionEnter(Collision other)
   {
      if (!this.isTrigger && other.collider.name.Equals("Kira"))
      {
         takeDamage = true;
      }

   }

   void OnCollisionExit(Collision other)
   {
      if (!this.isTrigger)
      {
         takeDamage = false;
      }
   }

   void OnTriggerEnter(Collider other)
   {
      if (this.isTrigger && other.name.Equals("Kira"))
      {
         takeDamage = true;
      }

   }

   void OnTriggerExit(Collider other)
   {
      if (this.isTrigger)
      {
         takeDamage = false;
      }
   }

   void dealDamage()
   {
      StatusManager.getInstance().health -= damageToTake; 
   }
}
