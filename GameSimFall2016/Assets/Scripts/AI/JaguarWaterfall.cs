using UnityEngine;
using System.Collections;

public class JaguarWaterfall : MonoBehaviour {

   public  bool isJaguarInWaterfall;
   private bool isWallActivated;
   private bool isWaterFlowing;
   private bool useWaterTimer;

   private float waterTimer;

   public GameObject myWall;

   private ParticleSystem myEmitter;

   public GameObject bossJaguar;

   private Vector3 myStartWallPos;
   private Vector3 myUpperWallPosMax;

   float activatedInterpolator = 0.0f;
   float deactivatedInterpolator = 0.0f;

   //==========================================================================
   // Use this for initialization
	void Start () {
      this.isJaguarInWaterfall = false;
      this.isWallActivated = false;
      this.isWaterFlowing = false;

      this.myEmitter = GetComponent<ParticleSystem>();
      this.myEmitter.Stop(); 

      if (this.myWall == null)
      {
         Debug.LogError("NRC:  A wall was not assigned to this waterfall, be sure to assign it one in the inspector.");
      }
      else
      {
         this.myWall.transform.position = this.myWall.transform.position;
         this.myUpperWallPosMax = new Vector3(this.myWall.transform.position.x,
                                              this.myWall.transform.position.y + 0.2f, 
                                              this.myWall.transform.position.z);
      }
	}
	
   //==========================================================================
	// Update is called once per frame
	void Update () {
      if (this.useWaterTimer)
      {
         this.waterTimer += Time.deltaTime;

         if (this.waterTimer >= 5.0f)
         {
            this.useWaterTimer = false;
            this.waterTimer = 0.0f;
         }
      }

      if (this.isWallActivated)
      {
         this.isWaterFlowing = true;
         this.activatedInterpolator += Time.deltaTime;
      
         if (this.myWall.transform.position.y < this.myUpperWallPosMax.y)
         {
            Vector3.Lerp(this.myWall.transform.position, this.myUpperWallPosMax, activatedInterpolator);
         }
      }
      else
      {
         this.isWaterFlowing = false;
         this.deactivatedInterpolator += Time.deltaTime;

         if (this.myWall.transform.position.y > this.myStartWallPos.y)
         {
            Vector3.Lerp(this.myWall.transform.position, this.myStartWallPos, deactivatedInterpolator);
         }
      }

      if (this.isWaterFlowing)
      {
         if (this.myEmitter.isStopped)
         {
            this.myEmitter.Play();
         }
         
         if (this.isJaguarInWaterfall)
         {
            this.bossJaguar.GetComponent<JaguarBoss>().damageJaguarBoss();
            Debug.Log(bossJaguar.GetComponent<JaguarBoss>().currentHealth());
            this.useWaterTimer = true;
         }
      }
      else
      {
         if (this.myEmitter.isPlaying)
         {
            this.myEmitter.Stop();
         }

      }

	}

   //==========================================================================
   // Move the wall object out of the way, and activate the flow of water.
   public void activateWaterFlow()
   {
      Debug.Log("Activating Water Flow");
      this.isWallActivated = true;
      this.activatedInterpolator = 0.0f;
   }

   //==========================================================================
   // Move the wall object back into place, and deactivate the flow of water.
   public void deactivateWaterFlow()
   {
      Debug.Log("Deactivating Water Flow");
      this.isWallActivated = false;
      this.deactivatedInterpolator = 0.0f;
   }

   //==========================================================================
   void OnTriggerEnter(Collider other)
   {
      if (other.tag.Equals("Boss"))
      {
         Debug.Log("Jaguar entered waterfall: " + this.gameObject.name);
         this.isJaguarInWaterfall = true;
      }
   }

   //==========================================================================
   void OnTriggerExit(Collider other)
   {
      if (other.tag.Equals("Boss"))
      {
         Debug.Log("Jaguar left waterfall: " + this.gameObject.name);
         this.isJaguarInWaterfall = false;
      }
   }
}
