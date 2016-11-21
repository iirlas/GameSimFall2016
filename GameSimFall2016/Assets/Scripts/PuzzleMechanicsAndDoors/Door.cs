using UnityEngine;
using System.Collections;

//========================================================================================================
//                                              Door
// Opens the door when BASIC TRIGGER is setup with message "doorOpen"
//========================================================================================================0

public class Door : MonoBehaviour
{
   [Tooltip("Currently not implemented")]
   public int numOfPuzzles;
   public static int puzzlesSolved;

   private bool openDoor;
   private float speed;
   private Vector3 start;
   private Vector3 destination;

   bool onlyOnce = true;

   //=============================================================================
   // Use this for initialization
   void Awake()
   {
      puzzlesSolved = 0;
      openDoor = false;
      speed = 2f;
      destination = new Vector3(this.transform.position.x,
                               (this.gameObject.GetComponent<Collider>().bounds.size.y + this.transform.position.y),
                                this.transform.position.z);
      start = transform.position;
   }

   //=============================================================================
   // Update is called once per frame
   void Update()
   {
      if (openDoor)
      {
         if (transform.position.y < destination.y)
         {
            transform.position = Vector3.Lerp(transform.position,
                                      destination,
                                      speed * 3.0f * Time.deltaTime);
            if (onlyOnce)
            {
               SoundManager.getInstance().playEffect("DoorStoneOpen");
               this.onlyOnce = false;
            }

         }
      }
      else if (transform.position.y > start.y)
      {
         transform.position = Vector3.Lerp(transform.position,
                         start,
                         speed * 3.0f * Time.deltaTime);
      }
   }

   //=============================================================================
   // 
   public void OnEvent(BasicTrigger trigger)
   {
      openDoor = true;
   }

   //=============================================================================
   // 
   public void OnEventEnd(BasicTrigger trigger)
   {
      openDoor = false;
   }
}
