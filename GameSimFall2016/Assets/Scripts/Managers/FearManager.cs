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

   private string prefix = " ";

   //===========================================================================
   // Use this for initialization
   void Start()
   {
      this.fearCurrent = this.fearMin;
      this.fearBarFill.color = Color.white;
      playerHealth = this.GetComponent<HealthPlayer>();
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      updateFearBar();
      if (Input.GetKeyDown(KeyCode.Period))
      {
         increaseStress();
      }
      else if (Input.GetKeyDown(KeyCode.Comma))
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

   //==========================================================================
   // 
   void updateFearBar()
   {
      this.fearBar.value = this.fearCurrent;
      if (this.fearBar.value < 100)
      {
         this.fearText.text = (prefix + ((int)this.fearCurrent).ToString());
      }
      else
      {
         this.fearText.text = (prefix + "Frightened");
      }
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
   }

   //==========================================================================
   // 
   private void trickleDownFear()
   {
      this.fearCurrent -= 1.0f;
   }

   private void increaseStress()
   {
      this.fearCurrent += 1.0f;
   }

   private void decreaseStress()
   {
      this.fearCurrent -= 1.0f;
   }
}
