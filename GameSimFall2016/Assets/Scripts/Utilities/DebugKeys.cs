using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DebugKeys : MonoBehaviour
{

   public bool debugKeysEnabled = false;
   public bool cheatCodesEnabled = true;
   private Vector3 worldSpawn;
   private Player thePlayer;

   private bool godMode = false;
   private bool soulsMode = false;
   private bool speedMode = false;

   private ArrayList keys;

   // Use this for initialization
   void Awake()
   {
      if (debugKeysEnabled)
         Debug.Log("DebugKeys is in use, look for the DebugKeys script under the Director is you wish to disable these!");
      if (cheatCodesEnabled)
         Debug.Log("CheatCodes are in use");
      this.keys = new ArrayList();
   }
   

   void Start()
   {
      thePlayer = PlayerManager.getInstance().players.First(player => { return player != null || player is Girl; });
      worldSpawn = thePlayer.transform.position;
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      if (Input.anyKeyDown)
      {
         foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
         {
            if (Input.GetKeyDown(key))
            {
               this.keys.Add(key);
            }
         }

         //=======================================================================
         // If cheat codes are enabled, use these codes
         if (this.keys.Count >= 5 && cheatCodesEnabled)
         {
            // load next scene
            if (getCode(KeyCode.G, KeyCode.N, KeyCode.E, KeyCode.X, KeyCode.T))
            {
               loadNextScene();
               this.keys.Add(KeyCode.Colon);
               print("Loading next scene");
            }
            // load previous scene
            if (getCode(KeyCode.G, KeyCode.P, KeyCode.R, KeyCode.E, KeyCode.V))
            {
               loadPrevScene();
               this.keys.Add(KeyCode.Colon);
               print("Loading previous scene");
            }
            // god mode
            if (getCode(KeyCode.I, KeyCode.D, KeyCode.D, KeyCode.Q, KeyCode.D))
            {
               this.godMode = !this.godMode;
               this.keys.Add(KeyCode.Colon);
               print("God Mode Enabled");
            }
            // souls mode
            if (getCode(KeyCode.S, KeyCode.O, KeyCode.U, KeyCode.L, KeyCode.S))
            {
               this.soulsMode = !this.soulsMode;
               this.keys.Add(KeyCode.Colon);
               print("Souls mode enabled");
            }

            //-----------------------------------------------------------------------
            // removes the first element of the keys array
            if (this.keys.Count > 5)
            {
               this.keys.RemoveAt(0);
            }
         }
      }

      //=======================================================================
      // If debug/developer keys are enabled, do these debug keys.  These are
      // quick alternatives to cheat codes.
      if (debugKeysEnabled)
      {
         //-----------------------------------------------------------------------
         // Load next scene or previous scene
         if (Input.GetKeyDown(KeyCode.PageUp))
         {
            loadNextScene();
         }
         else if (Input.GetKeyDown(KeyCode.PageDown))
         {
            loadPrevScene();
         }

         //-----------------------------------------------------------------------
         // Bring animals back to kira
         if (Input.GetKeyDown(KeyCode.Insert))
         {
            Player[] playerUnits = PlayerManager.getInstance().players;

            for (int ix = 0; ix < playerUnits.Length; ix++)
            {
               if (!(playerUnits[ix] is Girl))
               {
                  playerUnits[ix].transform.position = thePlayer.transform.position;
               }
            }
         }

         //-----------------------------------------------------------------------
         // Kill all active enemies
         if (Input.GetKeyDown(KeyCode.Delete))
         {
            EnemyManager.getInstance().killAllEnemies();
         }

         //-----------------------------------------------------------------------
         // Return to beginning of level
         if (Input.GetKeyDown(KeyCode.Home))
         {
            Player[] playerUnits = PlayerManager.getInstance().players;

            for (int ix = 0; ix < playerUnits.Length; ix++)
            {
               playerUnits[ix].transform.position = this.worldSpawn;
            }
         }

         //-----------------------------------------------------------------------
         // Set Kira's health to 0
         if (Input.GetKeyDown(KeyCode.End))
         {
            StatusManager.getInstance().health = 0;
         }

         //-----------------------------------------------------------------------
         // IDDQD
         if (Input.GetKeyDown(KeyCode.Equals))
         {
            this.godMode = !this.godMode;
         }
      }
   }

   //==========================================================================
   // 
   private void updateStatus()
   {
      if (this.godMode)
      {
         StatusManager.getInstance().health = 100;
         StatusManager.getInstance().fear = 0;
      }
      if (this.soulsMode)
      {
         StatusManager.getInstance().fear = 100;
      }
   }

   //==========================================================================
   // Loads the next scene
   private void loadNextScene()
   {
      if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }

   //==========================================================================
   // Loads the previous scene
   private void loadPrevScene()
   {
      if (SceneManager.GetActiveScene().buildIndex > 0)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
      }
   }

   //==========================================================================
   // Check whether or not the keys array contains the code passed. 
   // If code passed matches the keys in the array, return true, the code was
   // successful.  Else, return false, the code didn't match.
   private bool getCode(KeyCode keyOne, KeyCode keyTwo, KeyCode keyThree, KeyCode keyFour, KeyCode keyFive)
   {
      if (this.keys[0].Equals(keyOne) &&
          this.keys[1].Equals(keyTwo) &&
          this.keys[2].Equals(keyThree) &&
          this.keys[3].Equals(keyFour) &&
          this.keys[4].Equals(keyFive))
      {
         return true;
      }
      else
      {
         return false;
      }
   }
}
