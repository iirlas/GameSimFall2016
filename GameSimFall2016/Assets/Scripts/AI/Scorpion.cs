
//=============================================================================
// Author:  Nathan C.
// Version: 1.0
// Date:    09/07/2016
// Ownership belongs to all affiliates of Scott Free Games.
//=============================================================================

using UnityEngine;
using System.Collections;

//=============================================================================
// Scorpion.cs
// Scorpion will be one of the various enemies found within the game.
// The Scorpion will:
// - Deal ten points of damage overall, five initial, five over time.
// - Be defeated in four hits
// - Consistently follow player, stopping next to the player to attack.
public class Scorpion : Enemy
{
   //-----------------------------------------------------------------------------
   // Public Inspector-editable variables
   [Tooltip("Changing this value will change the detection radius of the Scorpion.")]
   public float detectionRadius; // How far out the Scorpion will search for the player.

   [Tooltip("Checkmark this box if you wish to provide custom values below.")]
   public bool overrideValues;  //If true, overwrites the default values for health, damage, speed
                                //and rotationspeed with values provided in the inspector
   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public int scorpionHealthCustom;   // the new health value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public int scorpionDamageCustom;   // the new damage value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public float scorpionSpeedCustom;  // the new speed value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public float scorpionRotationSpeedCustom;   // the new rotational speed value to replace the default.

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
   public int scorpionPoisonDamageCustom;

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"\nInstances refers to how many times the player will take poison damage")]
   public int scorpionPoisonInstancesCustom;

   [Tooltip("If you wish to override this value, checkmark \"Override Values\"\nIntervals refers to how often the player will take poison damage")]
   public float scorpionPoisonIntervalCustom;

   //-----------------------------------------------------------------------------
   // Default values for a Scorpion, provided by juan.
   private const int SCORPIONHEALTHDEFAULT = 4;
   private const int SCORPIONDAMAGEDEFAULT = 5;
   private const float SCORPIONSPEEDDEFAULT = 1;
   private const float SCORPIONROTATIONSPEEDDEFAULT = 1.0f;

   private const int SCORPIONPOISONDAMAGEDEFAULT = 1;
   private const int SCORPIONPOISONINSTANCESDEFAULT = 5;
   private const float SCORPIONPOISONINTERVALDEFAULT = 1.0f;

   private const float ATTACKINTERVAL = 1.0f;        // How often the Scorpion will attack

   //-----------------------------------------------------------------------------
   // Private member variable data.
   private float timeSinceLastAttack = 0.0f;   // The time elapsed since this Scorpion has last attacked.
   private bool  isInAttackRadius = false;     // Is the player within this Scorpion's attack radius?

   //-----------------------------------------------------------------------------
   // A reference to the player.
   protected GameObject thePlayer;

   //!~!~!~!~!~!~!~!~!~TODO!~!~!~!~!~!~!~!~!~!~!~!
   bool hasStatedWarning = false;
   //!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!~!

   //=============================================================================
   // Initialize things here
   void Start()
   {
      if (overrideValues)  // If custom values are provided, assign them to this Scorpion.
      {
         this.myHealth = scorpionHealthCustom;
         this.myDamage = scorpionDamageCustom;
         this.mySpeed = scorpionSpeedCustom;
         this.myRotationSpeed = scorpionRotationSpeedCustom;
      }
      else  // If custom values are not provided, utilize the default values for this Scorpion.
      {
         this.myHealth = SCORPIONHEALTHDEFAULT;
         this.myDamage = SCORPIONDAMAGEDEFAULT;
         this.mySpeed = SCORPIONSPEEDDEFAULT;
         this.myRotationSpeed = SCORPIONROTATIONSPEEDDEFAULT;

         this.scorpionPoisonDamageCustom = SCORPIONDAMAGEDEFAULT;
         this.scorpionPoisonInstancesCustom = SCORPIONPOISONINSTANCESDEFAULT;
         this.scorpionPoisonIntervalCustom = SCORPIONPOISONINTERVALDEFAULT;
      }

      this.myType = enType.SCORPION;
      findThePlayer();
      findThePlayerHealth();
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
            killScorpion();
            break;
         //-----------------------------------------------------------------------------
         default:
            break;
      }

      checkScorpionHealth();
      stateUpdate();
   }

   //=============================================================================
   // Updates the state of this Scorpion, if needed.
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
            //if the Scorpion will patrol, patrol the Scorpion around.
            break;
         //-----------------------------------------------------------------------------
         case enState.DEAD:
            //Scorpion is dead, object should be destroyed, if not already.
            killScorpion();
            break;
      }
   }

   //=============================================================================
   // If something enters the trigger box, do something based upon it's type.
   void OnTriggerEnter(Collider other)
   {
      if(other.tag.Equals("Projectile"))
      {
         damageScorpion();
      }
   }

   //=============================================================================
   // If something enters the trigger box, do something based upon it's type.
   void OnTriggerStay(Collider other)
   {
      if (other.transform.name.Equals("Kira") && this.myState != enState.ATTACK)
      {
         this.myState = enState.ATTACK;
      }
   }

   //=============================================================================
   // If something enters the trigger box, do something based upon it's type.
   void OnTriggerExit(Collider other)
   {
      if (other.tag.Equals("Player"))
      {
         this.myState = enState.TRACK;
      }
   }

   //=============================================================================
   // Looks for the player and stores a refernce to it, so it may be used later.
   void findThePlayer()
   {
      if (thePlayer == null)
      {
         thePlayer = GameObject.Find("Kira");
         if (thePlayer == null)
         {
            Debug.LogError("The player could not be found for " + this.name + ".  " + this.name + " requires there/n" +
                           "to be a player in the scene in order to function.");
         }
      }
   }

   //=============================================================================
   // Looks for the PlayerHealth and stores a refernce to it, so it may be used later.
   void findThePlayerHealth()
   {
      if (thePlayerHealth == null)
      {
         thePlayerHealth = GameObject.FindGameObjectWithTag("HealthManager");
         if (this.thePlayerHealth == null)
         {
            Debug.LogError("The PlayerHealth was not defined in the inspector for " + this.name + ".");
         }
      }
   }

   //=============================================================================
   // Check to see if the health of this Scorpion is 0, if so, change the state of this
   // Scorpion to enState.DEAD
   void checkScorpionHealth()
   {
      if (isDefeated())
      {
         this.myState = enState.DEAD;
      }
   }

   //=============================================================================
   // Follow the player around, until the player enters the hitbox for attacking.
   void pursuePlayer()
   {
      this.transform.LookAt(new Vector3(thePlayer.transform.position.x,
                                        this.transform.position.y,
                                        thePlayer.transform.position.z));

      if (Vector3.Distance(this.transform.position, thePlayer.transform.position) >= 0.1f)
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
         if (thePlayerHealth.GetComponent<HealthPlayer>().healthCurrent <= 0)
         {
            //do nothing
         }
         else
         {
            //do damage to player.
            Debug.Log(this.name + " has damaged the player.");
            thePlayerHealth.GetComponent<HealthPlayer>().modifyHealth(-5);
            thePlayerHealth.GetComponent<HealthPlayer>().poisonPlayer(this.scorpionPoisonDamageCustom,
                                                                      this.scorpionPoisonIntervalCustom,
                                                                      this.scorpionPoisonInstancesCustom);
         }

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
   // "Destroys" the Scorpion and all assosciated gameobjects.
   // ALL enemies will be moved to a magic value, where they will be deactivated.
   void killScorpion()
   {
      this.GetComponent<NavMeshAgent>().enabled = false;  // Disable the NavmeshAgent in order to prevent the Scorpion
                                                          // from clipping back onto the platform after being "killed".
      this.transform.position = OUTOFBOUNDS;              // Move this Scorpion out of bounds to the predefined location.
      this.gameObject.SetActive(false);                   // Disable this Scorpion, preventing interactability.
   }

   //=============================================================================
   // Deal a single point of damage to the Scorpion.
   void damageScorpion()
   {
      this.myHealth -= 1;
   }

   //=============================================================================
   // Deal a specific amount of damage to the Scorpion.  A negative number may be
   // passed to heal the Scorpion by the passed amount.
   void damageScorpion(int damage)
   {
      this.myHealth -= damage;
   }
}
