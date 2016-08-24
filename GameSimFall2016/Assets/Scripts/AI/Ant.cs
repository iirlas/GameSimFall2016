//=============================================================================
// Ant.cs
// Ant will be one of the various enemies found within the game.
// The Ant will:
// - Deal five points of damage
// - Be defeated in two hits
// - Consistently follow player, stopping next to the player to attack.
//=============================================================================

using UnityEngine;
using System.Collections;

public class Ant : Enemy
{
   [Tooltip("Checkmark this box if you wish to provide custom values below.")]
   public bool overrideValues;  //If true, overwrites the default values for health, damage, speed
                                //and rotationspeed with values provided in the inspector
   public int antHealthCustom;
   public int antDamageCustom;
   public float antSpeedCustom;
   public float antRotationSpeedCustom;

   private int antHealthDefault = 2;
   private int antDamageDefault = 5;
   private float antSpeedDefault = 1;
   private float antRotationSpeedDefault = 1;

   float attackInterval = 1.0f;      // The number of seconds the player will be 
                                     // invulnerable for after being attacked.
   float timeSinceLastAttack = 0.0f; // The current time that has elapsed since 
                                     // this Ant has attacked.

   GameObject thePlayer;             // A refernce to the player.

   Vector3 outOfBounds = new Vector3(-1000, -1000, -1000);  // Magic position in the game world, where all enemies
                                                            // will be moved to when it is "killed".

   //=============================================================================
   // Initialize things here
   void Start()
   {
      if (overrideValues)  //if custom values are to be provided.
      {
         this.myHealth = antHealthCustom;
         this.myDamage = antDamageCustom;
         this.mySpeed = antSpeedCustom;
         this.myRotationSpeed = antRotationSpeedCustom;
      }
      else  //utilize default values.
      {
         this.myHealth = antHealthDefault;
         this.myDamage = antDamageDefault;
         this.mySpeed = antSpeedDefault;
         this.myRotationSpeed = antRotationSpeedDefault;
      }

      this.myType = enType.ANT;

      thePlayer = null;
      findThePlayer();
   }

   //=============================================================================
   // Update is called once per frame
   void Update()
   {
      switch (this.myState)
      {
         case enState.IDLE:
            //do nothing, just hang out m80
            break;
         case enState.TRACK:
            pursuePlayer();
            break;
         case enState.ATTACK:
            attackPlayer();
            break;
         case enState.MOVE:
            //do some patroling, maybe?
            break;
         case enState.DEAD:
            killAnt();
            break;
      }

      stateUpdate();
   }

   //=============================================================================
   // Updates the state of this ant, if needed.
   void stateUpdate()
   {
      switch (this.myState)
      {
         case enState.IDLE:
            //do nothing, just hang out m80
            break;
         case enState.TRACK:
            //check to see if the player has left aggression radius
            break;
         case enState.ATTACK:
            //check to see if the player has left aggression radius
            break;
         case enState.MOVE:
            //check to see if the player is near
            break;
         case enState.DEAD:
            //Ant is dead, object should be destroyed, if not already.
            break;
      }
   }

   //=============================================================================
   // If something enters the trigger box, do something base upon it's type.
   void OnTriggerStay(Collider other)
   {
      if (other.tag.Equals("Player"))
      {
         this.myState = enState.ATTACK;
      }
   }

   //=============================================================================
   // Looks for the player and stores a refernce to it, so it may be used later.
   void findThePlayer()
   {
      if (thePlayer == null)
      {
         thePlayer = GameObject.FindGameObjectWithTag("Player");
         if (thePlayer == null)
         {
            Debug.LogError("The player could not be found for " + this.name + ".  " + this.name + " requires there/n" +
                           "to be a player in the scene in order to function.");
         }
      }
   }

   //=============================================================================
   // Follow the player around, until the player enters the hitbox for attacking.
   void pursuePlayer()
   {
      if (isPlayerNearby())
      {
         this.GetComponent<Rigidbody>().MovePosition(this.transform.position - thePlayer.transform.position);
      }
   }

   //=============================================================================
   // Follow the player around, until the player enters the hitbox for attacking.
   bool isPlayerNearby()
   {
      bool withinX = false;
      bool withinY = false;
      bool withinZ = false;

      if (Mathf.Abs(this.transform.position.x - thePlayer.transform.position.x) <= 10)
      {
         withinX = true;
      }
      if (Mathf.Abs(this.transform.position.y - thePlayer.transform.position.y) <= 10)
      {
         withinY = true;
      }
      if (Mathf.Abs(this.transform.position.z - thePlayer.transform.position.z) <= 10)
      {
         withinZ = true;
      }

      return ((withinX == withinY) && (withinY == withinZ) && (withinZ == withinX));
   }

   //=============================================================================
   // Attack the player, and prevent damaging for a small period of time.
   void attackPlayer()
   {
      if (timeSinceLastAttack == 0.0f)
      {
         //do damage to player.
         Debug.Log(this.name + " has damaged the player.");

         timeSinceLastAttack += Time.deltaTime;
      }
      else
      {
         timeSinceLastAttack += Time.deltaTime;
      }

      if (timeSinceLastAttack >= attackInterval)
      {
         timeSinceLastAttack = 0;
      }
   }

   //=============================================================================
   // "Destroys" the ant and all assosciated game.
   // ALL enemies will be moved to a magic value, where they will be deactivated.
   void killAnt()
   {
      this.transform.position = outOfBounds;
      this.gameObject.SetActive(false);
   }

   //=============================================================================
   // Deal a single point of damage to the ant.
   void damageAnt()
   {
      this.myHealth -= 1;
   }

   //=============================================================================
   // Deal a specific amount of damage to the ant.  A negative number may be
   // passed to heal the ant by the passed amount.
   void damageAnt(int damage)
   {
      this.myHealth -= damage;
   }
}
