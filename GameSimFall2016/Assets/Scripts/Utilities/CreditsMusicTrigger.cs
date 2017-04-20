using UnityEngine;
using System.Collections;

public class CreditsMusicTrigger : MonoBehaviour
{

   public GameObject musicObject;
   public string songToPlay;

   // Use this for initialization
   void Start()
   {
   }

   // Update is called once per frame
   void Update()
   {
      try
      {
		if (SoundManager.isValid)
         {
			musicObject.SetActive(true);
			SoundManager.getInstance().playMusic(this.songToPlay);
         }
      }
      catch (System.Exception e)
      {
      }
   }
}
