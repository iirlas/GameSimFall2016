using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMananger : Singleton<UIMananger>
{
   // the Health bar 
   public Slider myHealth;
   
   // the Fear bar
   public Slider myFear;

   // the Player icon
   public Image myKiraIcon;
   
   // the Bunny icon
   public Image myBunnyIcon;
   
   // the Bird icon
   public Image myBirdIcon;

   // the character indicator
   public Image myIndicator;

   // the rotational location of the player on the character indicator
   private const float PLAYERROTVAL = 42;

   // the rotational location of the bunny on the character indicator
   private const float BUNNYROTVAL = 0;

   // the rotational location of the bird on the character indicator
   private const float BIRDROTVAL = -42;

   //==========================================================================
   // Use this for initialization
   override protected void Init()
   {
      if (myHealth == null || myFear == null || myKiraIcon == null ||
          myBunnyIcon == null || myBirdIcon == null || myIndicator == null )
      {
         Debug.LogError("A reference was not set in the inspector for the UIManager, please check it out.");
      }

      myKiraIcon.enabled  = false;
      myBirdIcon.enabled  = false;
      myBunnyIcon.enabled = false;
   }

   //==========================================================================
   // Update is called once per frame
   void Update()
   {
      if (Input.GetKey(KeyCode.Semicolon))
      {
         StatusManager.getInstance().health -= 2;
      }
      else if (Input.GetKey(KeyCode.Quote))
      {
         StatusManager.getInstance().health += 2;
      }

      if (Input.GetKey(KeyCode.Comma))
      {
         StatusManager.getInstance().fear -= 2;
      }
      else if (Input.GetKey(KeyCode.Period))
      {
         StatusManager.getInstance().fear += 2;
      }

      if (StatusManager.getInstance().health > 0.0f)
      {
         updateHealthFearBars();
         updateCharacterIndicator();
      }
      else
      {
         myHealth.value = 0.0f;
         myFear.value = 0.0f;
         myIndicator.transform.rotation = Quaternion.Euler(0, 0, PLAYERROTVAL);
      }
   }

   //==========================================================================
   // 
   void updateHealthFearBars()
   {
      myHealth.value = StatusManager.getInstance().health;
      myFear.value = StatusManager.getInstance().fear;
      Debug.Log(StatusManager.getInstance().fear);
   }

   //==========================================================================
   // 
   void updateCharacterIndicator()
   {
      if (PlayerManager.getInstance().currentPlayer is Girl)
      {
         myKiraIcon.enabled = true;
         myBirdIcon.enabled = false;
         myBunnyIcon.enabled = false;
         myIndicator.transform.rotation = Quaternion.Euler(0,0, PLAYERROTVAL);
      }
      else if (PlayerManager.getInstance().currentPlayer is Bird)
      {
         myKiraIcon.enabled = false;
         myBirdIcon.enabled = true;
         myBunnyIcon.enabled = false;
         myIndicator.transform.rotation = Quaternion.Euler(0, 0, BIRDROTVAL);
      }
      else if (PlayerManager.getInstance().currentPlayer is Rabbit)
      {
         myKiraIcon.enabled = false;
         myBirdIcon.enabled = false;
         myBunnyIcon.enabled = true;
         myIndicator.transform.rotation = Quaternion.Euler(0, 0, BUNNYROTVAL);
      }
   }
}
