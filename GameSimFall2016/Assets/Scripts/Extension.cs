using UnityEngine;
using System.Collections;

public static class Extension
{
    //Extension for GameObject
    public static Type GetChildComponentWithTag<Type>(this GameObject gameObject, string tag)
    {
        Type[] items = gameObject.GetComponentsInChildren<Type>();
        foreach (Type item in items )
        {
            Component component = item as Component;
            if (component != null && component.tag == tag)
            {
                return item;
            }
        }
        return default(Type);
    }
   
    public static float angle ( float y, float x )
    {
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return (angle < 0 ? angle + 360 : angle);
    }
}