using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
    class AudioClipInfo
    {
        public AudioClip clip;
        public float duration;
        public float index;
        public Transform transform;
    }

   public AudioSource audioSource;
   public List<AudioClip> audioClips;
   
   private Dictionary<string, AudioClipInfo> loopingClips = new Dictionary<string, AudioClipInfo>();

    //------------------------------------------------------------------------------------------------
   private AudioClip getAudioClip(string name)
   {
       return audioClips.Find(audioClip => { return (audioClip.name.Equals(name)); });
   }

    //------------------------------------------------------------------------------------------------
   void Update()
   {
        foreach ( var clipKeyPair in loopingClips )
        {
            if (clipKeyPair.Value.index > clipKeyPair.Value.duration)
            {
                AudioSource.PlayClipAtPoint(clipKeyPair.Value.clip, clipKeyPair.Value.transform.position);
                clipKeyPair.Value.index = 0;
            }
            else
            {
                clipKeyPair.Value.index += Time.deltaTime;
            }

        }
    }

    //------------------------------------------------------------------------------------------------
    // Use this for initialization
    override protected void Init()
   {
        
   }

    // 

    //------------------------------------------------------------------------------------------------
    public void playEffect(string name)
   {

       AudioClip audioClip = getAudioClip(name);
       audioSource.PlayOneShot(audioClip);
   }

    //------------------------------------------------------------------------------------------------
   public void playAtPosition(string name, Vector3 position)
   {
       AudioClip audioClip = getAudioClip(name);
       AudioSource.PlayClipAtPoint(audioClip, position);
   }

    //------------------------------------------------------------------------------------------------
    public void loopAtPosition(string name, Transform transform)
    {
        AudioClipInfo clipInfo = new AudioClipInfo();
        clipInfo.clip = getAudioClip(name);
        clipInfo.duration = clipInfo.clip.length;
        clipInfo.index = 0;
        clipInfo.transform = transform;
        loopingClips.Add(name, clipInfo);
    }

    //------------------------------------------------------------------------------------------------
    public void stopLooping (string name)
    {
        loopingClips.Remove(name);
    }

    //------------------------------------------------------------------------------------------------
    public void playMusic(string name)
   {
       audioSource.clip = getAudioClip(name);
       audioSource.Play();
   }

    //------------------------------------------------------------------------------------------------
    public void stop(string name)
   {
       audioSource.Stop();
   }

    //------------------------------------------------------------------------------------------------
    public void stopAll()
   {
       audioSource.Stop();
   }
}
