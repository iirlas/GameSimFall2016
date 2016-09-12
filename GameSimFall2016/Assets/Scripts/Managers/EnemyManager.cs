using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
   [Tooltip("Enable if you wish to kill all enemies using the delete key")]
   public bool killKeyEnabled = true;

   //==========================================================================
   // Use this for initialization
   void Start()
   {
      if (killKeyEnabled)
      {
         Debug.LogWarning("NRC: A debug \"KILL\" key is in use, if this is the final build, remove it!  " + this.name + ".cs.");
      }
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Delete))
      {
         killAllEnemies();
      }
   }

   //==========================================================================
   // Kills all the enemies, what did ya think this did?
   // In reality this sets all enemies to be in the dead state, and the enemies
   // will move themselves to the "dead" zone.
   void killAllEnemies()
   {
      GameObject[] enArr = GameObject.FindGameObjectsWithTag("Enemy");

      for (int ix = 0; ix < enArr.Length; ix++)
      {
         enArr[ix].GetComponent<Enemy>().setState(Enemy.enState.DEAD);
      }
   }
}
