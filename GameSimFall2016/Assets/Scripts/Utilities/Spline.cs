using UnityEngine;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Spline : MonoBehaviour {
    public enum Type
    {
        LINEAR = 2,
        //QUADRATIC = 3,
        CUBIC = 4
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Spline))]
    public class SplineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Spline myScript = (Spline)target;
            for (int index = 0; index < myScript.knots.Count; index++)
            {
                if (myScript.knots[index] != null)
                {
                    myScript.knots[index].localPosition = EditorGUILayout.Vector3Field("Node:" + index, myScript.knots[index].localPosition);
                }
            }

            if (GUILayout.Button("Build"))
            {
                myScript.Build();
                SceneView.RepaintAll();
            }
        }
    }
#endif
    [Tooltip("Dictates the type of spline is created.")]
    public Type type = Type.CUBIC;
    [Tooltip("Dictates whether the spline is closed!")]
    public bool closed;
    [Tooltip("The Drawing length of each step going from 0 to knots.length")]
    public float drawLength = 0.1f;
    [Tooltip("Draws the position of each knot in this object hierarchy")]
    public bool drawKnots = true;

    [HideInInspector]
    public Transform this[int index]
    {
        get { return knots[index]; }
    }

    [HideInInspector]
    public int Length
    {
        get { return knots.Count; }
    }

    [HideInInspector]
    [SerializeField]
    private List<Transform> knots = new List<Transform>();

    // [0]a, [1]b, [2]c, [3]d   for
    //                          f(x) = a + bx + cx^2 + dx^3
    [HideInInspector]
    [SerializeField]
    private List<Vector3> coefficients = new List<Vector3>();

    void Awake ()
    {
    }

    private Vector3[] buildLinearSet ( int index )
    {
        int count = knots.Count;
        int indexNeg1 = Extension.mod(index - 1, count);
        int index1 = Extension.mod(index + 1, count);
        Vector3 derivative = (knots[index1].localPosition - knots[index].localPosition);

        return new Vector3[]
        {
            knots[index].position,
            derivative
        };
    }

    private Vector3[] buildQuadraticSet (int index)
    {
        return null;
    }

    private Vector3[] buildCubicSet (int index)
    {
        int count = knots.Count;
        int indexNeg1 = Extension.mod(index - 1, count);
        int index1 = Extension.mod(index + 1, count);
        int index2 = Extension.mod(index + 2, count);
        Vector3 derivative = (knots[index1].localPosition - knots[indexNeg1].localPosition);
        Vector3 derivative1 = (knots[index2].localPosition - knots[index].localPosition);

        return new Vector3[]
        {
            knots[index].position,
            derivative,
            3 * (knots[index1].localPosition - knots[index].localPosition) - 2 * derivative - derivative1,
            2 * (knots[index].localPosition - knots[index1].localPosition) + derivative + derivative1
        };
    }

    private Vector3[] buildSet ( int index )
    {
        switch (type)
        {
        case Type.LINEAR:
            return buildLinearSet(index);
        //case Type.QUADRATIC:
        //    return buildQuadraticSet(index);
        case Type.CUBIC:
            return buildCubicSet(index);
        }
        return null;
    }

    public void Build()
    {
        knots.Clear();
        for (int index = 0; index < transform.childCount; index++)
        {
            knots.Add(transform.GetChild(index));
        }

        coefficients.Clear();

        for (int index = 0, count = knots.Count; index < count; index++)
        {
            coefficients.AddRange(buildSet(index));
        }
    }
    
    public Vector3 Evaluate (float time)
    {
        int lowIndex = (int)time;
        Vector3 evaluation = Vector3.zero;

        if (!closed && lowIndex >= knots.Count - 1)
        {
            evaluation = knots[knots.Count - 1].localPosition;
        }
        else if ( coefficients.Count / knots.Count == (int)type )
        {
            // f(x) += ax^n   or   f(x) = a + bx + cx^2 + dx^3
            for (int count = 0; count < (int)type; count++)
            {
                int index = lowIndex % knots.Count * (int)type + count;
                evaluation += coefficients[index] * Mathf.Pow(time - lowIndex, count);
            }
        }

        return transform.rotation * (Vector3.Scale(evaluation, transform.localScale)) + transform.position;
    }


    public Quaternion LookAt ( Vector3 position, float time )
    {
        return LookAt(position, time, Vector3.up);
    }

    public Quaternion LookAt ( Vector3 position, float time, Vector3 upwards )
    {
        Vector3 result = Evaluate(time + Time.deltaTime);
        if ( result != position )
        {
            Quaternion rotation = Quaternion.LookRotation(result - position, upwards);
            return rotation;
        }
        return Quaternion.identity;
    }

    public void OnDrawGizmos ()
    {
        if (!drawKnots)
            return;

        Gizmos.color = Color.red;
        for (int index = 0; index < transform.childCount; index++)
        {
            Gizmos.DrawWireSphere(transform.GetChild(index).position, 0.1f);
        }
    }

    public void OnDrawGizmosSelected ()
    {
        if (coefficients.Count / knots.Count != (int)type)
            return;

        Vector3 lastEval = (knots.Count != 0) ? knots[0].position : Vector3.zero;
        Gizmos.color = Color.blue;
        for (float step = 0; step < knots.Count; step += drawLength)
        {
            Vector3 nextEval = Evaluate(step);
            Gizmos.DrawLine(lastEval, nextEval);
            lastEval = nextEval;
        }
    }
}
