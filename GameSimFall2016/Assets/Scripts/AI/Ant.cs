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

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public int antHealthCustom;   // the new health value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public int antDamageCustom;   // the new damage value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public float antSpeedCustom;  // the new speed value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public float antRotationSpeedCustom;   // the new rotational speed value to replace the default.

   private const int   ANTHEALTHDEFAULT = 2;          // default value for an Ant, provided by juan.
   private const int   ANTDAMAGEDEFAULT = 5;          // default value for an Ant, provided by juan.
   private const float ANTSPEEDDEFAULT = 1;           // default value for an Ant, provided by juan.
   private const float ANTROTATIONSPEEDDEFAULT = 1;   // default value for an Ant, provided by juan.

   private bool  isInAttackRadius = false;

   private const float ATTACKINTERVAL = 1.0f;  // The number of seconds the player will be invulnerable for after being attacked.
   private float timeSinceLastAttack = 0.0f;   // The time elapsed since this Ant has last attacked.


   private GameObject thePlayer;  // A refernce to the player.

   private Vector3 outOfBounds = new Vector3(-1000, -1000, -1000);  // Magic position in the game world, where all enemies
                                                                    // will be moved to when it is "killed".

   //=============================================================================
   // Initialize things here
   void Start()
   {
      if (overrideValues)  // If custom values are provided, assign them to this Ant.
      {
         this.myHealth = antHealthCustom;
         this.myDamage = antDamageCustom;
         this.mySpeed  = antSpeedCustom;
         this.myRotationSpeed = antRotationSpeedCustom;
      }
      else  // If custom values are not provided, utilize the default values for this Ant.
      {
         this.myHealth = ANTHEALTHDEFAULT;
         this.myDamage = ANTDAMAGEDEFAULT;
         this.mySpeed = ANTSPEEDDEFAULT;
         this.myRotationSpeed = ANTROTATIONSPEEDDEFAULT;
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
         default:
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
            //check to see if the player has entered the aggression radius
            if (isPlayerNearby())
            {
               this.myState = enState.TRACK;
            }
            break;
         case enState.TRACK:
            //check to see if the player has left aggression radius
            if (!isPlayerNearby())
            {
               this.myState = enState.IDLE;
            }
            break;
         case enState.ATTACK:
            if (isPlayerNearby() && !isInAttackRadius)
            {
               this.myState = enState.MOVE;
            }
            else if (!isPlayerNearby() && !isInAttackRadius)
            {
               this.myState = enState.IDLE;
            }
            break;
         case enState.MOVE:
            //if the Ant will patrol, patrol the ant around.
            break;
         case enState.DEAD:
            //Ant is dead, object should be destroyed, if not already.
            killAnt();
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
   // If something enters the trigger box, do something base upon it's type.
   void OnTriggerExit(Collider other)
   {
      if (other.tag.Equals("Player"))
      {
         this.myState = enState.TRACK;
      }
   }

   //=============================================================================
   // Check to see if the health of this Ant is 0, if so, change the state of this
   // Ant to enState.DEAD
   void checkAntHealth()
   {
      if (this.myHealth <= 0)
      {
         this.myState = enState.DEAD;
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
      //Vector3.MoveTowards(this.transform.position, thePlayer.transform.position, this.mySpeed * Time.deltaTime);
      //this.transform.position = thePlayer.transform.position;
      this.transform.LookAt(thePlayer.transform);

      if (Vector3.Distance(this.transform.position, thePlayer.transform.position) >= 0.2f)
      {
         this.transform.position += this.transform.forward * this.mySpeed * Time.deltaTime;
      }
   }

   //=============================================================================
   // Follow the player around, until the player enters the hitbox for attacking.
   bool isPlayerNearby()
   {
      bool withinX = false;
      bool withinY = false;
      bool withinZ = false;

      if (Mathf.Abs(this.transform.position.x - thePlayer.transform.position.x) <= 1.5)
      {
         withinX = true;
      }
      if (Mathf.Abs(this.transform.position.y - thePlayer.transform.position.y) <= 1.5)
      {
         withinY = true;
      }
      if (Mathf.Abs(this.transform.position.z - thePlayer.transform.position.z) <= 1.5)
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

      if (timeSinceLastAttack >= ATTACKINTERVAL)
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
