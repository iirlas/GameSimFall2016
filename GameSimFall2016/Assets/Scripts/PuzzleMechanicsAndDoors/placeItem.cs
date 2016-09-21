using UnityEngine;
using System.Collections;

public class placeItem : MonoBehaviour {

    //Script is placed where item should be placed in its final iteration.

    bool isPlaced;
    public Item itemThing;

	// Use this for initialization
	void Start () {
        isPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnEvent(BasicTrigger trigger)
    {
        if (trigger.message == "placeItem")
        {
            if (!isPlaced)
            {
                //if (Inventory.getInstance().Has();
                //{
                //    itemThing.transform.position = gameObject.transform.position;
                //    itemThing.gameObject.SetActive(true);
                //    Debug.Log("itemDropped");
                //    PlayerManager.getInstance().items.Remove(itemThing.name);
                //    Destroy(itemThing.GetComponent<Item>());
                    

                }
            }

        }
    }
//}
