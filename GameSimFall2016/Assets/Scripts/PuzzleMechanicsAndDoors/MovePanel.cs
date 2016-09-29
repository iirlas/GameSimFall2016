using UnityEngine;
using System.Collections;

//========================================================================================================
//                                         Move Panel
// This script "teleports" an item from its original spot to the designated end point on BASIC TRIGGER
// This script should be placed on the item at its original point.                                  
//========================================================================================================

public class MovePanel : MonoBehaviour
{
   [Tooltip("The object, invisible, at its final destination")]
   public GameObject endPoint;

   // Use this for initialization
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }


   void OnEvent(BasicTrigger trigger)
   {
      if (trigger.message == "movePanel")
      {
         this.gameObject.transform.position = endPoint.transform.position;

      }
   }
}
