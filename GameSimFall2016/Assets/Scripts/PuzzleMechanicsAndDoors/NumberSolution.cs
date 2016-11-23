using UnityEngine;
using System.Collections;

public class NumberSolution : MonoBehaviour
{
   public GameObject[] allPillars;
   public GameObject[] solutionPillars;
   public UnityEngine.Events.UnityEvent events = new UnityEngine.Events.UnityEvent();
   private int numDown;

   // Use this for initialization
   void Awake()
   {

   }

   // Update is called once per frame
   void Update()
   {

      checkWin(checkDown());

   }

   int checkDown()
   {
      numDown = 0;
      for (int i = 0; i < allPillars.Length; i++)
      {
         if (allPillars[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NumberPillarDown"))
         {
            numDown++;
         }
      }

      return numDown;
   }

   bool checkWin(int colNums)
   {
      int winner = 0;
      if (colNums == 4)
      {
         foreach (GameObject pillar in solutionPillars)
         {
            if (pillar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NumberPillarDown"))
            {
               winner++;
            }
         }

         if (winner == 4)
         {
            for (int i = 0; i < allPillars.Length; i++)
            {
               if (allPillars[i].GetComponent<Animator>().GetBool("MoveDown") != true)
                  allPillars[i].GetComponent<Animator>().SetBool("MoveDown", true);
            }
            events.Invoke();
            return true;
         }
         else
         {
            for (int j = 0; j < allPillars.Length; j++)
            {
               if (allPillars[j].GetComponent<Animator>().GetBool("MoveDown") == true)
                  allPillars[j].GetComponent<Animator>().SetBool("MoveDown", false);

            }
         }
      }
      return false;
   }

   
}
