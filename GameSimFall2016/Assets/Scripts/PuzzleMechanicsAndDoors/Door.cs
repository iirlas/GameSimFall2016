using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Door
// Opens the door when BASIC TRIGGER is setup with message "doorOpen"
//========================================================================================================

public class Door : MonoBehaviour
{
    [Tooltip("Currently not implemented")]
   public int numOfPuzzles;
   public static int puzzlesSolved;

   // Use this for initialization
   void Start()
   {
      puzzlesSolved = 0;

   }

   // Update is called once per frame
   public void OnEvent(BasicTrigger trigger)
   {
      //if (trigger.message == "doorOpen")
      {
         gameObject.SetActive(false);
      }

   }


}
