using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : FreeSingleton<Inventory>
{
    new static protected bool isCreatedWhenMissing
    {
        get { return false; }
    }

    private Dictionary<Tag, int> myItems;

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

    override protected void Init()
    {
        myItems = new Dictionary<Tag, int>();
    }

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

    public bool Has(Tag tag)
    {
        return myItems.ContainsKey(tag);
    }


}
