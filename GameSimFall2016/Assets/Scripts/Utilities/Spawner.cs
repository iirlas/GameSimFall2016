//========================================================================================================
// Author:  Nathan C.
// Version: 0.1
// Spawner.cs will function as a object that will bring enemies into the game.  It can be set to infinitely
// spawn an enemy type.  This will cause up to 5 enemies to exist on screen, and after one dies, another
// will spawn.  It can also be set to a specific limit, where once the limit has been reached, the spawner
// will cease spawning enemies.
//
// In terms of using this class:
// Attach this class to an empty game object.  You will need a secondary game object which will be the
// desired spawn location of the enemy.  You may attach a box collider and rigid body to this gameObject.
//========================================================================================================

using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
   [Tooltip("The total number of enemies you want this spawner to spawn.")]
   public int totalNumberOfEnemies;

   [Tooltip("The amount of time that should elapse before another enemy is spawned.")]
   public float timeBetweenWaves;

   [Tooltip("The desired enemy type to be spawned.  Be sure to use a prefab, and not the model.")]
   public GameObject enemy;

   [Tooltip("If this spawner has a trigger collider that will be used to activate the spawner, check this tickbox.")]
   public bool usingTriggerBox;

   [Tooltip("The desired spawnpoint for this spawner.")]
   public GameObject spawnPoint;

   private float dTime;               //How much time has elapsed?

   private bool hasActivatedTrigger;  //Has the trigger to enable spawning of enemies been activated before?

   private bool isSpawningEnemies;    //Are we currently spawning enemies?

   private int numSpawnedEnemies;     //Count of the number of enemies spawned.

   private int killedEnemies;         //Count of the number of enemies killed.

   private ArrayList enemyList;       //ArrayList of the spawned enemies.

   //========================================================================================================
   // Use this for initialization
   void Start()
   {
      this.isSpawningEnemies = false;
      this.dTime = 0.0f;
      this.enemyList = new ArrayList();
      this.numSpawnedEnemies = 0;
      this.hasActivatedTrigger = false;
   }

   //========================================================================================================
   // Update is called once per frame
   void Update()
   {
      if (isSpawningEnemies)
      {
         // If we have reached the time to spawn an enemy, and we are still allowed to spawn an enemy...
         if (this.dTime >= timeBetweenWaves && this.numSpawnedEnemies < this.totalNumberOfEnemies)
         {
            this.dTime = 0.0f;             // reset the timer

            if (this.enemyList.Count < 5)  // If there are fewer than 5 enemies on screen...
            {
               Debug.Log("Spawning enemy");
               spawnEnemy();               // Spawn a new enemy
               this.numSpawnedEnemies += 1;// Increment count of spawned enemies
               Debug.Log(this.enemyList.Count);
            }

            this.dTime += Time.deltaTime;  // Increment the timer
         }
         else if (this.numSpawnedEnemies < this.totalNumberOfEnemies)  // not enough time has elapsed
         {
            this.dTime += Time.deltaTime;  // Increment the timer
         }
         else  //we've finished spawning enemies
         {
            isSpawningEnemies = false;
         }

         for (int ix = 0; ix < this.enemyList.Count; ix++)
         {
            if (((GameObject)this.enemyList[ix]).activeSelf == false)
            {
               this.enemyList.RemoveAt(ix);
               this.killedEnemies++;
            }
         }
      }
   }

   //========================================================================================================
   // When the player enters the trigger zone, enable spawning enemies if the option has been selected.
   void OnTriggerEnter(Collider other)
   {
      if (usingTriggerBox && hasActivatedTrigger == false)
      {
         if (other.tag.Equals("Player"))
         {
            this.setSpawningState(true);
         }
      }
   }

   //========================================================================================================
   // Use this method if you wish to manually control the spawning state of this Spawner. (recommended)
   public void setSpawningState(bool state)
   {
      if (this.isSpawningEnemies == false && state == true)
      {
         this.numSpawnedEnemies = 0;
      }

      this.isSpawningEnemies = state;
   }

   //========================================================================================================
   // Spawn an enemy
   void spawnEnemy()
   {
      Vector3 startPos = this.spawnPoint.transform.position;
      this.enemyList.Add(Instantiate(enemy, startPos, Quaternion.identity) as GameObject);
   }

   //========================================================================================================
   // Returns the number of living enemies out of the ones that are spawned.
   public int getRemainingEnemies()
   {
      return (totalNumberOfEnemies - killedEnemies);
   }

   //========================================================================================================
   // Returns whether or not this spawner is spawning enemies.
   public bool getSpawningStatus()
   {
      return this.isSpawningEnemies;
   }
}
