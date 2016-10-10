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
      StatusManager.getInstance().health -= damageToTake; 
   }
}
