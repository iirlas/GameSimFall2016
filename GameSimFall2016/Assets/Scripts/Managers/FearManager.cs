using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FearManager : Singleton<FearManager>
{
   //public Slider fearBar;
   //public Image fearBarFill;
   //public Text fearText;

   public float fearMax = 110.0f;  // 10 over 100 to represent overflow
   public float fearMin = 0.0f;
   public float fearCurrent;
   public bool inDark = true;

   public HealthPlayer playerHealth;

   private string prefix = " ";

   //===========================================================================
   // Use this for initialization
   override protected void Init()
   {
      //this.fearCurrent = this.fearMin;
      //this.fearBarFill.color = Color.white;
      playerHealth = this.GetComponent<HealthPlayer>();
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      updateFearBar();
      if (inDark)
      {
         increaseStress();
      }
      else
      {
         decreaseStress();
      }
   }

   void FixedUpdate()
   {
      if (this.fearCurrent >= 100.0f)
      {
         damagePlayer();
      }
   }

   void LateUpdate()
   {
      fearCurrent = Mathf.Max(Mathf.Min(110, fearCurrent), 0);
      //inDark = true;
   }

   //==========================================================================
   // 
   void updateFearBar()
   {
      StatusManager.getInstance().fear = this.fearCurrent;

      //this.fearBar.value = this.fearCurrent;

      //if (this.fearBar.value < 100)
      //{
      //   this.fearText.text = (prefix + ((int)this.fearCurrent).ToString());
      //}
      //else
      //{
      //   this.fearText.text = (prefix + "Frightened");
      //}
   }

   //==========================================================================
   // 
   void damagePlayer()
   {
      this.playerHealth.modifyHealth(-1);
   }

   //==========================================================================
   // Adds fear to the player's fear bar
   public void modifyFear(float value)
   {
      this.fearCurrent += value;
      this.fearCurrent = Mathf.Clamp(this.fearCurrent, this.fearMin, this.fearMax);
      //if (this.fearCurrent > this.fearMax)
      //{
      //    this.fearCurrent = this.fearMax;
      //}
      //else if (this.fearCurrent < this.fearMin)
      //{
      //    this.fearCurrent = this.fearMin;
      //}
   }

   //==========================================================================
   // 
   private void trickleDownFear()
   {
      this.fearCurrent -= 1.0f * Time.deltaTime;
   }

   private void increaseStress()
   {
      this.fearCurrent += 1.0f * Time.deltaTime;
   }

   private void decreaseStress()
   {
      this.fearCurrent -= 1.0f * Time.deltaTime;
   }

   public void OnEvent(BasicTrigger trigger)
   {
      inDark = false;
      //GameObject.FindObjectOfType<Girl>().status &= ~Girl.Status.DARK;
   }

   public void OnEventEnd(BasicTrigger trigger)
   {
      inDark = true;
      //GameObject.FindObjectOfType<Girl>().status |= Girl.Status.DARK;
   }
}