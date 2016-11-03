using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
   public AudioSource audioSource;
   public List<AudioClip> audioClips;

   private AudioClip getAudioClip(string name)
   {
       return audioClips.Find(audioClip => { return (audioClip.name.Equals(name)); });
   }
   
   // Use this for initialization
   override protected void Init()
   {
   }

   // 
   public void playEffect(string name)
   {

       AudioClip audioClip = getAudioClip(name);
       audioSource.PlayOneShot(audioClip);
   }

   public void playAtPosition(string name, Vector3 position)
   {
       AudioClip audioClip = getAudioClip(name);
       AudioSource.PlayClipAtPoint(audioClip, position);
   }

   public void playMusic(string name)
   {
       audioSource.clip = getAudioClip(name);
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
