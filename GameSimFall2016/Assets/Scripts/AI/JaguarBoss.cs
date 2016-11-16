using UnityEngine;
using System.Collections;

public class JaguarBoss : Enemy
{
   public enum jagAttack
   {
      SWIPE = 0,
      FIRE,
      POUNCE
   }

   public int maxHits = 3;
   public int swipeAttackDamage = 10;
   public int fireDamage = 25;
   public int pounceDamage = 15;
   public int contactDamage = 10;

   private jagAttack currentAttack;

   //--------------------------------------------------------------------------
   // Jaguar Boss States
   private bool hasAttacked;
   private bool isInCenterOfRoom;
   private bool isStunned;
   private bool isAwake;

   private float trackingTimer;
   private float attackLengthTimer;
   private float attackingTimer;
   private float stunTimer;

   //private GameObject thePlayer;

   //==========================================================================
   // Use this for initialization
   void Start()
   {
      this.isAwake = false;
      this.myHealth = this.maxHits;
      this.myDamage = swipeAttackDamage;

      this.myType = enType.JAGUAR;
      this.myState = enState.IDLE;

      this.mySpeed = 10.0f;
      this.myRotationSpeed = 2.0f;
   }
           
   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      // if the player hasn't triggered the boss, do nothing
      if (!isAwake)
      {
         return;
      }

      // if the boss has been trigger, do the things
      if (isStunned)
      {
         stunTimer += Time.deltaTime;
         if (stunTimer >= 5.0f)
            isStunned = false;
      }
      else if (hasAttacked)
      {
         hasAttacked = false;
         chooseNewAttack();
      }

      if (attackLengthTimer >= this.attackingTimer)
      {
         this.attackingTimer = 0.0f;
         hasAttacked = true;
      }

      if ( StatusManager.getInstance().health <= 0)
      {
         this.resetBoss();
      }
   }

   //==========================================================================
   // Returns the current health of the jaguar in integer form
   public int currentHealth()
   {
      return this.myHealth;
   }

   //==========================================================================
   // Randomly select a new attack type from the designated attacks
   private void chooseNewAttack()
   {
      this.currentAttack = ((jagAttack)((int)Random.Range(0f, 2.99f)));
      switch (this.currentAttack)
      {
         case jagAttack.FIRE:
            this.attackLengthTimer = 5.0f;
            break;
         case jagAttack.POUNCE:
            this.attackLengthTimer = 2.0f;
            break;
         case jagAttack.SWIPE:
            this.attackLengthTimer = 1.0f;
            break;
         default:
            this.attackLengthTimer = 0.0f;
            Debug.LogError("");
            break;
      }
   }

   //==========================================================================
   // Does damage to the Jaguar, silly.
   public void damageJaguarBoss()
   {
      this.myHealth--;
   }

   //==========================================================================
   // 
   public void triggerBoss()
   {
      this.isAwake = true;
   }

   //==========================================================================
   // If they player dies, we need to reset the boss state
   public void resetBoss()
   {
      this.isAwake = false;
      this.myHealth = this.maxHits;
      this.myDamage = swipeAttackDamage;

      this.myType = enType.JAGUAR;
      this.myState = enState.IDLE;

      this.mySpeed = 10.0f;
      this.myRotationSpeed = 2.0f;
   }
}
