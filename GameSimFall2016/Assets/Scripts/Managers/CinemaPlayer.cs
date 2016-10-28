using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CinemaPlayer : Singleton<CinemaPlayer>
{

   public MovieTexture video;
   public bool playOnAwake;

   private UnityEvent onPlay;
   private UnityEvent onPause;
   private UnityEvent onStop;

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

   // Plays the video
   public void play()
   {
   }

   // Pause the video
   public void pause()
   {
   }

   // Stop the video
   public void stop()
   {
   }
}
