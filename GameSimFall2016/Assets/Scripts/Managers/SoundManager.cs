using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
   public AudioSource audioSource;
   public List<AudioClip> audioClips;

   // Use this for initialization
   override protected void Init()
   {

   }

   // Use this for post-initialization
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {

   }

   // 
   public void playEffect(string name)
   {

   }

   public void playMusic(string name)
   {

   }

   public void stop(string name)
   {

   }

   public void stopAll()
   {

   }
}
