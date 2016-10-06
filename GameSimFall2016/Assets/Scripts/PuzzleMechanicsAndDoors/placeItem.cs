using UnityEngine;
using System.Collections;

//========================================================================================================
//                                               Place Item
// MUST BE PLACED ON AN ITEM DEFINED AS ITEM/INVENTORY GOOD
//Script is placed where item should be placed in its final iteration.                                              
//========================================================================================================

public class placeItem : MonoBehaviour {

    [Tooltip("Has the item been placed?")]
    bool isPlaced;
    [Tooltip("The gameObject with Item Script Attached.")]
    public Item itemThing;

	// Use this for initialization
	void Awake () {
        isPlaced = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnEvent(BasicTrigger trigger)
    {
        //if (trigger.message == "placeItem")
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
