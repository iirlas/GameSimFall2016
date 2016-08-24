using UnityEngine;
using System.Collections;

abstract public class Singleton <Type> : MonoBehaviour
    where Type : MonoBehaviour
{
    static private Type ourInstance;

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

    void Awake ()
    {
        if ( ourInstance != null )
        {
            throw new System.Exception("Singleton [" + this.name + "] conflicts with [" + ourInstance.name + "] !");
        }
        else
        {
            ourInstance = this as Type;
            Init();
        }
    }

    protected abstract void Init();
}
