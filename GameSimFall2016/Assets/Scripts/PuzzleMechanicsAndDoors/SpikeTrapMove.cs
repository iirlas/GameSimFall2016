using UnityEngine;
using System.Collections;

public class SpikeTrapMove : MonoBehaviour
{
   [Tooltip("The time in seconds to wait before beginning the animations of the spike trap.")]
   public float timeToStart = 0.0f;

   [Tooltip("The time in seconds to wait in between spike cycles.")]
   public float spikeInterval = 0.0f;

   [Tooltip("The trigger that this script willl trigger int he animator component.")]
   public string animTriggerName = "HolyHellChangeThis";

   private float timer = 0.0f;
   private Animator myAnim;
   private bool hasActivated;

   // Use this for initialization
   void Awake()
   {
      this.myAnim = this.GetComponent<Animator>();
      this.hasActivated = false;
   }

   // Update is called once per frame
   void FixedUpdate()
   {
      if (this.hasActivated == false)
      {
         if (this.timer >= this.timeToStart)
         {
            this.timer = 0.0f;
            this.hasActivated = true;
         }
         this.timer += Time.deltaTime;
      }

      if ( this.hasActivated )
      {
         this.myAnim.SetTrigger(animTriggerName);
         
         this.hasActivated = false;
         timeToStart = spikeInterval + 2.0f;
      }
   }
}
