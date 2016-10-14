using UnityEngine;
using System.Collections;
using System.Linq;

public class Reticule : MonoBehaviour {

   private bool isTargeting;
   private Camera cameraToLookAt;
   private Player thePlayer;

   private readonly Vector3 cameraOutOfBounds = new Vector3(-1000f, -1000f, -1000f);

   void Awake()
   {

   }

   // Use this for initialization
   void Start() {
      cameraToLookAt = Camera.main;
      thePlayer = PlayerManager.getInstance().players.First(player => { return player != null && player is Girl; });
   }

   // Update is called once per frame
   void Update() {

      checkIfTargeting();

      if (isTargeting)
      {
         this.transform.position = this.thePlayer.gameObject.GetComponent<Girl>().target.position;
         lookAtPlayer();
      }
      else
      {
         this.transform.position = cameraOutOfBounds;
      }
   }

   void checkIfTargeting()
   {
      if (this.thePlayer.gameObject.GetComponent<Girl>().target != null)
      {
         isTargeting = true;
      }
      else
      {
         isTargeting = false;
      }
   }

   void lookAtPlayer()
   {
      transform.LookAt(transform.position + cameraToLookAt.transform.rotation * Vector3.forward,
                       cameraToLookAt.transform.rotation * Vector3.up);
   }
}
