using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Stores Item via Tags and the amount.
public class Inventory : Singleton<Inventory>
{
	#if UNITY_EDITOR
	[UnityEditor.CustomEditor(typeof(Inventory))]
	public class InventoryEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();


			Inventory myScript = (Inventory)target;
			if (myScript.myItems != null && myScript.myItems.Count != 0) {
				List<Tag> keys = new List<Tag> (myScript.myItems.Keys);
				foreach (var key in keys) {
					GUILayout.BeginHorizontal ();
					GUILayout.Label ("Item: " + key);
					GUILayout.Label ("Count: ");
					myScript.myItems[key] = int.Parse (GUILayout.TextField (myScript.myItems[key].ToString ()));	
					GUILayout.EndHorizontal ();
				}
			}
		}
	}
	#endif //UNITY_EDITOR

    private Dictionary<Tag, int> myItems;

    //------------------------------------------------------------------------------------------------
    //Retrieves the item count given the item name
    public int this[Tag tag]
    {
        get 
        { 
            if ( myItems.ContainsKey(tag) )
            {
                return myItems[tag];
            }
            else
            {
                return 0;
            }
        }
    }

    //------------------------------------------------------------------------------------------------
    override protected void Init()
    {
        myItems = new Dictionary<Tag, int>();
    }

    //------------------------------------------------------------------------------------------------
    //Adds an item
    public void Add(Tag tag)
    {
        if (myItems.ContainsKey(tag))
        {
            myItems[tag]++;
        }
        else
        {
            myItems.Add(tag, 1);
        }
    }

    //------------------------------------------------------------------------------------------------
    //Removes an item
    public void Remove(Tag tag)
    {
        if (myItems.ContainsKey(tag))
        {
            if (myItems[tag] > 0)
            {
                myItems[tag]--;
            }
            else
            {
                myItems.Remove(tag);
            }
        }
    }

    //------------------------------------------------------------------------------------------------
    //Retrieves if an item exists.
    public bool Has(Tag tag)
    {
        return myItems.ContainsKey(tag);
    }
}


