using UnityEngine;
using System.Collections;

public class EndingChecker : MonoBehaviour {

   public GameObject doorGood;
   public GameObject doorMeh;
   private GameObject chosenDoor;
   private Vector3 destination;
   private bool opendoor;
   private float speed;

   //TAKE THIS OUT LATER
   public int numOfItems;

	// Use this for initialization
	void Awake () {
      opendoor = false;
      speed = 2f;


	
	}
	
	// Update is called once per frame
	void Update () {
      if (opendoor)
      {
         if (chosenDoor.transform.position.y < destination.y)
         {
            chosenDoor.transform.position = Vector3.Lerp(chosenDoor.transform.position,
                                                         destination,
                                                         speed * 3.0f * Time.deltaTime);

         }
         opendoor = false;
      }
	
	}


   void OnTriggerEnter(Collider other)
   {
      if (other.tag.Equals("Player"))
      {
         //ADD ME BACK IN LATER: if (Inventory.getInstance()["BunnyToken"] == 9)
         if(numOfItems == 9)
         {
            chosenDoor = doorGood;
            destination = new Vector3(doorGood.transform.position.x, 
                                      (doorGood.gameObject.GetComponent<Collider>().bounds.size.y + doorGood.transform.position.y),
                                      doorGood.transform.position.z);
            opendoor = true;
         }
         else
         {
            chosenDoor = doorMeh;
            destination = new Vector3(doorMeh.transform.position.x,
                                      (doorMeh.gameObject.GetComponent<Collider>().bounds.size.y + doorMeh.transform.position.y),
                                      doorMeh.transform.position.z);
            opendoor = true;
         }

      }
   }
}
