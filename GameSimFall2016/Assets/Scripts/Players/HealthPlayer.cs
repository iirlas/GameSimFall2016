//========================================================================================================
//                                              HEALTH PLAYER
//  This script has a slider that controls the health of the player. Currently a static variable (healthChange)
//  can be accessed to control change in health (negative numbers for damage, positive for healing).
//
//  Additionally, public values exist for the Slider and the healthMax (temporary, for ease of testing)
//
// public Slider healthBar: hook up the UI slider component in this slot
// public int healthMax: hook up the maximum health here. NOTE: this should eventually not be as public as it is.
// public static int healthChange: this number is for changing the value and is accessible by all other classes.
// private int healthCurrent: stores the current health for display inside the slider
// 
//========================================================================================================

//========================================================================================================
// NRC:
// Attach this script to an empty game object, which will be refered to as the "director"
//========================================================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{

   public Slider healthBar; //hook the healthbar to this slider spot
   public int healthMax;

   public static int healthChange;

   public int healthCurrent;

   private bool isDead = false;

   private bool isPoisoned;        // whether or not the player is poison
   private int poisonDamage;      // how much damage the player will be poisoned for
   private int poisonTicks;       // the remaining poison hits the player will take
   private float poisonInterval;  // how often the player will take poison damage
   private float poisonTimer;     // the time elapsed since the player last took poison damage.

   //==========================================================================
   // initialises health change, healthCurrent
   // sets maxValue of slider to healthMax
   // sets minValue to 0
   void Start()
   {

      //this.healthChange = 0;
      this.healthCurrent = healthMax;
      this.healthBar.maxValue = healthMax;
      this.healthBar.minValue = 0;

      this.isPoisoned = false;
      this.poisonDamage = 0;
      this.poisonTicks = 0;
      this.poisonInterval = 0.0f;
      this.poisonTimer = 0.0f;
   }

   //==========================================================================
   // This updates the health and checks if she should be dead.
   // if dead, debug log tells you she's dead.
   void Update()
   {
      if (!isDead)
      {
         if (this.healthCurrent < 1)
         {
            this.healthBar.value = healthBar.minValue;
            Debug.Log("Player is Dead.");
            isDead = true;
         }
         else
         {
            this.healthBar.value = this.healthCurrent;
            if (isPoisoned)
            {
               applyPoisonDamage();
            }
         }
      }
   }

   //==========================================================================
   // Modify the health of the player, value can be positive or negative.
   // If the value is positive, the player gains health.
   // If the value is negative, the player loses health.
   public void modifyHealth(int value)
   {
      this.healthCurrent += value;
   }

   //==========================================================================
   // Notify the player to begin taking poision damage, while setting
   // values for the HealthPlayer manager to deal poision damage.
   public void poisonPlayer(int poisonDamage, float interval, int instances)
   {
      this.poisonDamage = poisonDamage;
      this.poisonInterval = interval;
      this.poisonTicks = instances;
   }

   //==========================================================================
   // Deals automated damage to the player, hereby refered to as poison.
   public void applyPoisonDamage()
   {
      if (this.poisonTicks <= 0)
      {
         this.isPoisoned = false;
      }
      else
      {
         if (this.poisonTimer >= this.poisonInterval)
         {
            this.poisonTimer = 0.0f;
            modifyHealth(-(this.poisonDamage));
         }
         else
         {
            this.poisonInterval += Time.deltaTime;
         }
      }
   }

}
