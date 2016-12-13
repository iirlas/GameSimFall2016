using UnityEngine;
using System.Collections;

abstract public class Singleton <Type> : MonoBehaviour
    where Type : MonoBehaviour
{
    static private Type ourInstance;

    //------------------------------------------------------------------------------------------------
    //hide the property using new keyword to 
    //create the singleton when one is not avaliable
    static protected bool isCreatedWhenMissing
    {
        get { return false; }
    }

    //------------------------------------------------------------------------------------------------
    static public Type getInstance()
    {
        if ( ourInstance == null )
        {
            ourInstance = GameObject.FindObjectOfType<Type>();
            if ( ourInstance == null )
            {
                throw new System.Exception("Singleton [" + typeof(Type) + "] not in the current scene!");
            }
        }
        return ourInstance;
    }

    //------------------------------------------------------------------------------------------------
    void Awake()
    {
        ourInstance = getInstance();
        if ( ourInstance != this )
        {
            throw new System.Exception("Singleton [" + this.gameObject.name + "] conflicts with [" + ourInstance.gameObject.name + "] !");
        }
        else
        {
            Init();
        }
    }

    protected abstract void Init();
}
