using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOut : MonoBehaviour {

   public float fadeInTime = 1.0f;
   public float fadeOutWaitTime = 1.0f;
   public float fadeOutTime = 1.0f;

   private float timer = 0.0f;

   private bool isFadingIn = false;
   private bool isWaiting = false;
   private bool isFadingOut = false;

   private Image myImage;

	// Use this for initialization
	void Start () {
      this.myImage = this.GetComponent<Image>();
      this.isFadingIn = true;
	}
	
	// Update is called once per frame
	void Update () {
      if ( this.isFadingIn )
      {
         timer += Time.deltaTime;

         Color temp = this.myImage.color;
         temp.a = Mathf.Lerp(temp.a, 0, timer / fadeInTime);
         this.myImage.color = temp;
         
         if (this.timer >= fadeInTime)
         {
            this.isFadingIn = false;
            this.isWaiting = true;
            this.timer = 0.0f;
         }
      }
      if ( this.isWaiting )
      {
         this.timer += Time.deltaTime;
         if (this.timer >= this.fadeOutWaitTime)
         {
            this.isWaiting = false;
            this.isFadingOut = true;
            this.timer = 0.0f;
         }
      }
      if ( this.isFadingOut )
      {
         timer += Time.deltaTime;

         Color temp = this.myImage.color;
         temp.a = Mathf.Lerp(temp.a, 255, timer / fadeInTime);
         this.myImage.color = temp;

         if (this.timer >= fadeInTime)
         {
            this.isFadingIn = false;
            this.isWaiting = false;
            this.timer = 0.0f;
         }
      }
	}
}
