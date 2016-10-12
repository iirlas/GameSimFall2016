using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class DebugKeys : MonoBehaviour {

   public bool debugKeysEnabled = false;
   private Vector3 worldSpawn;
   private Player thePlayer;

   // Use this for initialization
   void Awake () {

      thePlayer = PlayerManager.getInstance().players.First(player => { return player != null && player is Girl; });
      worldSpawn = thePlayer.transform.position;

      if ( debugKeysEnabled )
        Debug.Log("DebugKeys is in use, look for the DebugKeys script under the Director is you wish to disable these!");
	}
	
   //==========================================================================
	// Update is called once per frame
	void Update () {

      //-----------------------------------------------------------------------
      // Load next scene or previous scene
      if ( Input.GetKeyDown(KeyCode.PageUp) )             
      {
         if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount)
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
         }
      }
      else if ( Input.GetKeyDown(KeyCode.PageDown) )
      {
         if (SceneManager.GetActiveScene().buildIndex > 0)
         {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
         }
      }

      //-----------------------------------------------------------------------
      // Bring animals back to kira
      if ( Input.GetKeyDown(KeyCode.Insert) )            
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
      if ( Input.GetKeyDown(KeyCode.Delete) )
      {
         EnemyManager.getInstance().killAllEnemies();
      }

      //-----------------------------------------------------------------------
      // Return to beginning of level
      if ( Input.GetKeyDown(KeyCode.Home) )
      {
         Player[] playerUnits = PlayerManager.getInstance().players;

         for ( int ix = 0; ix < playerUnits.Length; ix++)
         {
            playerUnits[ix].transform.position = this.worldSpawn;
         }
      }

      //-----------------------------------------------------------------------
      // Set Kira's health to 0
      if ( Input.GetKeyDown(KeyCode.End) )
      {
         StatusManager.getInstance().health = 0;
      }
	}
}
