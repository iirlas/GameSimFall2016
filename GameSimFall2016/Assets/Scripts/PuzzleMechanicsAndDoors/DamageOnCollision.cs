using UnityEngine;
using System.Collections;

public class DamageOnCollision : MonoBehaviour
{
   private bool takeDamage;

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
      StatusManager.getInstance().health -= 5; 
   }
}
