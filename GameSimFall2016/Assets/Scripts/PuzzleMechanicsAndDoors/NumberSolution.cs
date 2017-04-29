using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NumberSolution : MonoBehaviour
{
	public Transform[] allPillars;
	public Transform[] solutionPillars;
	public List<Transform> pillars;
   	public UnityEngine.Events.UnityEvent OnComplete;
   	private int numDown;

   // Use this for initialization
   void Awake()
   {
		pillars = new List<Transform> ();
   }

   // Update is called once per frame
   void Update()
   {
      //checkWin(checkDown());

   }

	public void Check (Transform pillar)
	{
		bool success = (pillars.Count < 4);
		if (success)
		{
			pillars.Add (pillar);
			foreach (Transform item in pillars)
			{
				if (item.GetComponent<Animator> ().GetBool ("MoveDown"))
				{
					success &= solutionPillars.Contains (pillar);
				}
			}
		}

		if (pillars.Count == 4)
		{
			foreach (Transform item in allPillars)
			{
				item.GetComponent<Animator> ().SetBool ("MoveDown", success);
			}

			if (success)
			{
				OnComplete.Invoke ();
			}
			pillars.Clear ();
		}
	}

//   int checkDown()
//   {
//      numDown = 0;
//      for (int i = 0; i < allPillars.Length; i++)
//      {
//         if (allPillars[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NumberPillarDown"))
//         {
//            numDown++;
//         }
//      }
//
//      return numDown;
//   }
//
//   bool checkWin(int colNums)
//   {
//      int winner = 0;
//      if (colNums >= 4)
//      {
//         foreach (GameObject pillar in solutionPillars)
//         {
//            if (pillar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("NumberPillarDown"))
//            {
//               winner++;
//            }
//         }
//
//         if (winner >= 4)
//         {
//            for (int i = 0; i < allPillars.Length; i++)
//            {
//               if (allPillars[i].GetComponent<Animator>().GetBool("MoveDown") != true)
//                  allPillars[i].GetComponent<Animator>().SetBool("MoveDown", true);
//            }
//            return true;
//         }
//         else
//         {
//            for (int j = 0; j < allPillars.Length; j++)
//            {
//               if (allPillars[j].GetComponent<Animator>().GetBool("MoveDown") == true)
//                  allPillars[j].GetComponent<Animator>().SetBool("MoveDown", false);
//
//            }
//         }
//      }
//      return false;
//   }

   
}
