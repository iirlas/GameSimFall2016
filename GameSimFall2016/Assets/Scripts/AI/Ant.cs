//=============================================================================
// Author:  Nathan C.
// Version: 1.0
// Date:    08/26/2016
// Ownership belongs to all affiliates of Scott Free Games.
//=============================================================================

using UnityEngine;
using System.Collections;

//=============================================================================
// Ant.cs
// Ant will be one of the various enemies found within the game.
// The Ant will:
// - Deal five points of damage
// - Be defeated in two hits
// - Consistently follow player, stopping next to the player to attack.
public class Ant : Enemy
{
   //-----------------------------------------------------------------------------
   // Public Inspector-editable variables
   [Tooltip("Changing this value will change the detection radius of the Ant.")]
   public float detectionRadius; // How far out the Ant will search for the player.

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

   //-----------------------------------------------------------------------------
   // Default values for an Ant, provided by juan.
   private const int ANTHEALTHDEFAULT = 2;
   private const int ANTDAMAGEDEFAULT = 5;
   private const float ANTSPEEDDEFAULT = 1;
   private const float ANTROTATIONSPEEDDEFAULT = 1;
   private const float ATTACKINTERVAL = 1.0f;       // The number of seconds the player will be invulnerable for after being attacked.

   //-----------------------------------------------------------------------------
   // Private member variable data.
   private float timeSinceLastAttack = 0.0f;   // The time elapsed since this Ant has last attacked.
   private bool isInAttackRadius = false;     // Is the player within this Ant's attack radius?
   private GameObject thePlayer;               // A refernce to the player.

   //!~!~!~!~!~!~!~!~!~TODO!~!~!~!~!~!~!~!~!~!~!~!
   bool hasStatedWarning = false;
   //!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!

   //=============================================================================
   // Initialize things here
   void Start()
   {
      if (overrideValues)  // If custom values are provided, assign them to this Ant.
      {
         this.myHealth = antHealthCustom;
         this.myDamage = antDamageCustom;
         this.mySpeed = antSpeedCustom;
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
   // Update is called once per frames
   void Update()
   {
      //!~!~!~!~!~!~!~!~!~!~TODO!~!~!~!~!~!~!~!~!~!~!
      //!~!~!~ Testing DEATH state transitions ~!~!~!
      //!~!~!~!~!~DELETE BEFORE FINAL BUILD~!~!~!~!~!
      if (!hasStatedWarning)
      {
         hasStatedWarning = true;
         Debug.LogWarning("A debug key is in use, if this is the final build, remove it!\n" +
                          this.name + ".cs.  Be sure to also remove the variable \"hasStatedWarning\".  Click for more details.\n" +
                          "Ctrl+F and search for \"TODO\" to find statements in question.");
      }
      if (Input.GetKeyDown(KeyCode.Delete))
      {
         this.myState = enState.DEAD;
      }
      //!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!


      switch (this.myState)
      {
         //-----------------------------------------------------------------------------
         case enState.IDLE:
            //do nothing, just hang out m80
            break;
         //-----------------------------------------------------------------------------
         case enState.TRACK:
            pursuePlayer();
            break;
         //-----------------------------------------------------------------------------
         case enState.ATTACK:
            attackPlayer();
            break;
         //-----------------------------------------------------------------------------
         case enState.MOVE:
            //do some patroling, maybe?
            break;
         //-----------------------------------------------------------------------------
         case enState.DEAD:
            killAnt();
            break;
         //-----------------------------------------------------------------------------
         default:
            break;
      }

      checkAntHealth();
      stateUpdate();
   }

   //=============================================================================
   // Updates the state of this ant, if needed.
   void stateUpdate()
   {
      switch (this.myState)
      {
         //-----------------------------------------------------------------------------
         case enState.IDLE:
            //check to see if the player has entered the aggression radius
            if (isPlayerNearby())
            {
               this.myState = enState.TRACK;
            }
            break;
         //-----------------------------------------------------------------------------
         case enState.TRACK:
            //check to see if the player has left aggression radius
            if (!isPlayerNearby())
            {
               this.myState = enState.IDLE;
            }
            break;
         //-----------------------------------------------------------------------------
         case enState.ATTACK:
            //check to see if the player has left the attack radius
            if (isPlayerNearby() && !isInAttackRadius)
            {
               this.myState = enState.MOVE;
            }
            // check to see if the player has left the aggression radius
            else if (!isPlayerNearby() && !isInAttackRadius)
            {
               this.myState = enState.IDLE;
            }
            break;
         //-----------------------------------------------------------------------------
         case enState.MOVE:
            //if the Ant will patrol, patrol the ant around.
            break;
         //-----------------------------------------------------------------------------
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
      if (isDefeated())
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
      this.transform.LookAt(new Vector3(thePlayer.transform.position.x,
                                        this.transform.position.y,
                                        thePlayer.transform.position.z));

      if (Vector3.Distance(this.transform.position, thePlayer.transform.position) >= 0.2f)
      {
         this.transform.position += this.transform.forward * this.mySpeed * Time.deltaTime;
      }
   }

   //=============================================================================
   // Returns whether or not the player is within aggression radius.
   bool isPlayerNearby()
   {
      bool withinX = false;
      bool withinY = false;
      bool withinZ = false;

      if (Mathf.Abs(this.transform.position.x - thePlayer.transform.position.x) <= this.detectionRadius)
      {
         withinX = true;
      }
      if (Mathf.Abs(this.transform.position.y - thePlayer.transform.position.y) <= this.detectionRadius)
      {
         withinY = true;
      }
      if (Mathf.Abs(this.transform.position.z - thePlayer.transform.position.z) <= this.detectionRadius)
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
   // "Destroys" the ant and all assosciated gameobjects.
   // ALL enemies will be moved to a magic value, where they will be deactivated.
   void killAnt()
   {
      this.GetComponent<NavMeshAgent>().enabled = false;  // Disable the NavmeshAgent in order to prevent the Ant
                                                          // from clipping back onto the platform after being "killed".
      this.transform.position = OUTOFBOUNDS;              // Move this Ant out of bounds to the predefined location.
      this.gameObject.SetActive(false);                   // Disable this Ant, preventing interactability.
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
