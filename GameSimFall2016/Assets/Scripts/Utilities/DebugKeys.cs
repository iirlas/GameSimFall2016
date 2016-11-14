using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DebugKeys : MonoBehaviour
{

   public bool debugKeysEnabled = false;
   private Vector3 worldSpawn;
   private Player thePlayer;

   private bool godMode = false;

   private ArrayList keys;

   // Use this for initialization
   void Awake()
   {
      if (debugKeysEnabled)
         Debug.Log("DebugKeys is in use, look for the DebugKeys script under the Director is you wish to disable these!");
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
         foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
         {
            if (Input.GetKeyDown(key))
            {
               this.keys.Add(key);
            }
         }
      }

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

      if (this.keys.Count >= 5)
      {
         if (this.keys[0].Equals(KeyCode.G) &&
             this.keys[1].Equals(KeyCode.N) &&
             this.keys[2].Equals(KeyCode.E) &&
             this.keys[3].Equals(KeyCode.X) &&
             this.keys[4].Equals(KeyCode.T))
         {
            loadNextScene();
            this.keys.Add(KeyCode.Colon);
         }
         else if (this.keys[0].Equals(KeyCode.G) &&
                  this.keys[1].Equals(KeyCode.P) &&
                  this.keys[2].Equals(KeyCode.R) &&
                  this.keys[3].Equals(KeyCode.E) &&
                  this.keys[4].Equals(KeyCode.V))
         {
            loadPrevScene();
            this.keys.Add(KeyCode.Colon);
         }
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
      if (this.keys.Count >= 5)
      {
         if (this.keys[0].Equals(KeyCode.I) &&
             this.keys[1].Equals(KeyCode.D) &&
             this.keys[2].Equals(KeyCode.D) &&
             this.keys[3].Equals(KeyCode.Q) &&
             this.keys[4].Equals(KeyCode.D))
         {
            this.godMode = !this.godMode;
            this.keys.Add(KeyCode.Colon);
         }
      }

      if (this.godMode)
      {
         StatusManager.getInstance().health = 100;
         StatusManager.getInstance().fear = 0;
      }

      //-----------------------------------------------------------------------
      // removes the first element of the keys array
      if (this.keys.Count > 5)
      {
         this.keys.RemoveAt(0);
      }
   }

   private void loadNextScene()
   {
      if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }

   private void loadPrevScene()
   {
      if (SceneManager.GetActiveScene().buildIndex > 0)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
      }
   }
}
