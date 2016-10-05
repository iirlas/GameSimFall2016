using UnityEngine;
using System.Collections;

public class SpoopManager : MonoBehaviour
{
   float idleTime;
   AudioSource myAudio;

   // Use this for initialization
   void Awake()
   {
      idleTime = 0.0f;
      myAudio = this.GetComponent<AudioSource>();
   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
      {
         idleTime += Time.deltaTime;
      }
      else
      {
         idleTime = 0.0f;
      }

      if (idleTime >= 10.0f)
      {
         GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().setSpoopActive(true);
         myAudio.Play();
      }
      else if (myAudio.isPlaying)
      {
         GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthPlayer>().setSpoopActive(false);
         myAudio.Stop();
      }
   }
}
