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

   private bool openDoor;
   private float speed;
   private Vector3 start;
   private Vector3 destination;

   //public AudioSource OpenStoneDoorSoundEffect;
   //bool playDoorOpenSoundOnce = false;

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

   void Update()
   {
      if (openDoor)
      {
         if (transform.position.y < destination.y)
         {
            transform.position = Vector3.Lerp(transform.position,
                                      destination,
                                      speed * 3.0f * Time.deltaTime);
            //if (this.playDoorOpenSoundOnce == false && this.OpenStoneDoorSoundEffect != null)
            //{
            //   this.OpenStoneDoorSoundEffect.Play();
            //   this.playDoorOpenSoundOnce = true;
            //}

         }
      }
      else if (transform.position.y > start.y)
      {
         transform.position = Vector3.Lerp(transform.position,
                         start,
                         speed * 3.0f * Time.deltaTime);
      }
   }

   // Update is called once per frame
   public void OnEvent(BasicTrigger trigger)
   {
      //if (trigger.message == "doorOpen")
      {
         openDoor = true;

      }

   }

   public void OnEventEnd(BasicTrigger trigger)
   {
      //if (trigger.message == "doorOpen")
      {
         openDoor = false;

      }

   }


}
