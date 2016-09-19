﻿using UnityEngine;
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
   private bool cutsceneHasPlayed;
   private bool hasAttacked;
   private bool isInCenterOfRoom;

   private float trackingTimer;
   private float chargeTimer;
   private float fireTimer;
   private float swipreTimer;

   private GameObject[] waterfallArr;

   private GameObject thePlayer;
   private Random rand;

   //==========================================================================
   // Use this for initialization
   void Start()
   {
      this.rand = new Random();
      this.myHealth = this.maxHits;
      this.myDamage = swipeAttackDamage;

      this.myType = enType.JAGUAR;
      this.myState = enState.IDLE;

      this.waterfallArr = GameObject.FindGameObjectsWithTag("Waterfall");
      
      this.thePlayer = GameObject.Find("Kira");
      this.thePlayerHealth = GameObject.FindWithTag("HealthManager");
      
      if (thePlayerHealth == null)
      {
         Debug.LogError("The HealthManager could not be found for " + this.name + ".  " + this.name + " requires there\n" +
                        "to be a player in the scene in order to function.");
      }

      this.mySpeed = 10.0f;
      this.myRotationSpeed = 2.0f;
   }
           
   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      //do what jaguars do
      if (!cutsceneHasPlayed)
      {
         //play boss cutscene
      }

      // attack logic
      if (hasAttacked)
      {
         chooseNewAttack();
      }
      else // execute selected attack
      {
      }

   }

   public int currentHealth()
   {
      return this.myHealth;
   }

   private void chooseNewAttack()
   {
      this.currentAttack = ((jagAttack)((int)Random.Range(0f, 2.99f)));
   }

   //==========================================================================
   // Does damage to the Jaguar, silly.
   public void damageJaguarBoss()
   {
      this.myHealth--;
   }


}
