//=============================================================================
// Enemy.cs
// Generic enemy class that all future enemies will inherit from.  Enemy.cs
// will contain features that all enemies share, such as a value for health, 
// or a value for damage.
//=============================================================================

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

   //=============================================================================
   // Common states that most enemies, if not all, share.
   public enum enState
   {
      MOVE = 0,
      TRACK,
      ATTACK,
      IDLE,
      SPECIAL,
      DEAD
   }

   //=============================================================================
   // A list of the common enemy types that can be found in the world. 
   public enum enType
   {
      UNDEF = -1,
      FRIEND = 0,
      ANT = 1,
      SNAKE,
      PECCARY,
      SCORPION,
      CROCODILE,
      JAGUAR,
      MONKEY,
      BAT,
      SHARK,
      FLAPFLAP
   }

   protected enState myState;
   protected enType myType;

   protected int myHealth;   // The health of this enemy.
   protected int myDamage;   // The damage this enemy does to other GameObjects.

   protected float mySpeed;          // The base speed at which this enemy moves.
   protected float myRotationSpeed;  // The base speed at which this enemy will "turn".

   //=============================================================================
   // Default ctor
   public Enemy()
   {
      this.myState = enState.IDLE;
      this.myType = enType.UNDEF;

      this.myHealth = 1;
      this.myDamage = -1;
      this.mySpeed = 1;
      this.myRotationSpeed = 1;
   }

   //=============================================================================
   // Update is called once per frame
   void Update()
   {
      if (isDefeated())
      {
         //destroy this gameobj
      }
   }

   //=============================================================================
   // Check to see if the enemy has been defeated.  The enemy is defeated when
   //  it's remaining health is 0.
   bool isDefeated()
   {
      if (this.myHealth == 0)
      {
         return true;
      }

      return false;
   }
}
