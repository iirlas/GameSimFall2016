using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Stores Item via Tags and the amount.
public class Inventory : ExplosiveSingleton<Inventory>
{
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
