using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BunnyBossUI : MonoBehaviour {

   private BunnyBoss myBoss;
   private Slider mySlider;
   private Camera cameraToLookAt;

	// Use this for initialization
	void Awake () {
      this.mySlider = this.GetComponentInChildren<Slider>();

	}
	
	// Update is called once per frame
	void Update () {
      this.updateSlider();
      this.lookAtCamera();
	}

   void updateSlider()
   {
      this.mySlider.value = this.myBoss.health;
   }

   void lookAtCamera()
   {
      this.transform.LookAt(this.transform.position + this.cameraToLookAt.transform.rotation * Vector3.forward,
                       this.cameraToLookAt.transform.rotation * Vector3.up);
   }
}