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

   // 
   public void playEffect(string name)
   {
       
       AudioClip localAudioClip = audioClips.Find(audioClip => { return (audioClip.name.Equals(name)); });
       audioSource.PlayOneShot(localAudioClip);
   }

   public void playMusic(string name)
   {
       audioSource.clip = audioClips.Find(audioClip => { return (audioClip.name.Equals(name)); });
       audioSource.Play();
   }

   public void stop(string name)
   {
       audioSource.Stop();
   }

   public void stopAll()
   {
       audioSource.Stop();
   }
}
