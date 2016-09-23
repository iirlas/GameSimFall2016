using UnityEngine;
using System.Collections;

public class MovePanel : MonoBehaviour
{

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
