using UnityEngine;
using System.Collections;

public class TargetLightActivation : MonoBehaviour
{

   public Light[] puzzleBoxes;

   //public AudioSource lightSound1;
   //public AudioSource lightSound2;
   int soundNumber = 1;

   // Use this for initialization
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   public void OnEvent(BasicTrigger trigger)
   {
      //if (trigger.message == "Light")
      {
         Debug.Log("hitPanel");
         foreach (Light boxLight in puzzleBoxes)
         {
            Debug.Log(boxLight.intensity);
            if (boxLight.intensity < 1)
            {
               boxLight.intensity = 1;
               //if (this.lightSound1 != null && this.lightSound2 != null)
               //{
               //   //play sound
               //   if (this.soundNumber == 1)
               //   {
               //      this.lightSound1.Play();
               //   }
               //   else if (this.soundNumber == 2)
               //   {
               //      this.lightSound2.Play();
               //   }
               //}
            }
            else {

               boxLight.intensity = 0;

               Debug.Log(boxLight.intensity);
            }
         }
      }
   }
}
