using UnityEngine;
using System.Collections;

public class ActivateParticleEffect : MonoBehaviour
{
   private ParticleSystem myPartSys;
   private Animator myAnim;

   // Use this for initialization
   void Awake()
   {
      myAnim = GetComponent<Animator>();
      myPartSys = GetComponent<ParticleSystem>();
   }

   // Update is called once per frame
   void Update()
   {
      if (myAnim.GetBool("playAnimation") == true)
      {
         myPartSys.Play();
         myAnim.SetBool("playAnimation", false);
      }
   }
}
