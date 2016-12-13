using UnityEngine;
using System.Collections;

abstract public class FreeSingleton<Type> : MonoBehaviour
    where Type : MonoBehaviour
{
    static private Type ourInstance;

    //------------------------------------------------------------------------------------------------
    static public Type getInstance()
    {
        if (ourInstance == null)
        {
            ourInstance = GameObject.FindObjectOfType<Type>();
            if (ourInstance == null)
            {
                GameObject gameObject = new GameObject( "Singleton - " + typeof(Type));
                ourInstance = gameObject.AddComponent<Type>();
            }
        }
        return ourInstance;
    }

    //------------------------------------------------------------------------------------------------
    void Awake()
    {
        ourInstance = getInstance();
        if (ourInstance != this)
        {
            throw new System.Exception("Singleton [" + this.name + "] conflicts with [" + ourInstance.name + "] !");
        }
        else
        {
            Init();
        }
    }

    protected abstract void Init();
}
