using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FearManager : MonoBehaviour
{

   public Slider fearBar;
   public Image fearBarFill;
   public Text fearText;

   public float fearMax = 110.0f;  // 10 over 100 to represent overflow
   public float fearMin = 0.0f;
   public float fearCurrent;

   public HealthPlayer playerHealth;

   private string prefix = "  ";

   //===========================================================================
   // Use this for initialization
   void Start()
   {
      this.fearCurrent = this.fearMin;
      this.fearBarFill.color = Color.white;
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      updateFearBar();
   }

   void FixedUpdate()
   {
      if (this.fearCurrent >= (this.fearMax - 10.0f))
      {
         damagePlayer();
      }
   }

   //==========================================================================
   // 
   void updateFearBar()
   {
      this.fearBar.value = this.fearCurrent;
      this.fearText.text = (prefix + ((int)this.fearCurrent).ToString());
   }

   //==========================================================================
   // 
   void damagePlayer()
   {
      this.playerHealth.modifyHealth(-1);
   }

   //==========================================================================
   // Adds fear to the player's fear bar
   public void addFear(float value)
   {
      this.fearCurrent += value;
   }

   //==========================================================================
   // 
   private void trickleDownFear()
   {
      this.fearCurrent -= 0.1f;
   }
}
