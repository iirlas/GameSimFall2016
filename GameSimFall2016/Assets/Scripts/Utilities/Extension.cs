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

    public static bool isCloseTo(this Color color, Color other, float epsilon)
    {
        float c1 = ((Vector4)color).magnitude,
              c2 = ((Vector4)other).magnitude;

        return Mathf.Abs(c1 - c2) < epsilon;
    }

    public static int bitvalue(this Color color)
    {
        int r = (int)(color[0] * 256),
            g = (int)(color[1] * 256),
            b = (int)(color[2] * 256),
            a = (int)(color[3] * 256);

        return (r << 24) | (g << 16) | (b << 8) | (a);
    }

    public static Vector3 Inverse ( this Vector3 vector )
    {
        return new Vector3( 1.0f / vector.x, 1.0f / vector.y, 1.0f / vector.z );
    }

    public static Vector3 Pow(this Vector3 vector, int exponent)
    {
        return new Vector3(Mathf.Pow(vector.x, exponent), Mathf.Pow(vector.y, exponent), Mathf.Pow(vector.z, exponent));
    }

    public static float angle ( float y, float x )
    {
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        return (angle < 0 ? angle + 360 : angle);
    }

}