using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Spline : MonoBehaviour {
    public enum Type
    {
        LINEAR = 2,
        QUADRATIC = 3,
        CUBIC = 4
    }

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

    [Tooltip("Dictates the type of spline is created.")]
    public Type type = Type.CUBIC;
    [Tooltip("Dictates whether the spline is closed!")]
    public bool closed;
    [Tooltip("The Drawing length of each step going from 0 to knots.length")]
    public float drawLength = 0.1f;

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

    public void Build()
    {
        knots.Clear();
        for (int index = 0; index < transform.childCount; index++)
        {
            knots.Add(transform.GetChild(index));
        }

        coefficients.Clear();

        Vector3[] localCoefficients = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
        for (int index = 0, count = knots.Count; index < count; index++)
        {
            // connect the end to the beginning.
            int index1 = (index + 1) % count;
            int index2 = (index + 2) % count;

            Vector3 derivative = (knots[index1].position - knots[index].position);
            Vector3 derivative1 = (knots[index2].position - knots[index1].position);

            //a
            localCoefficients[0] = knots[index].position;

            //b
            localCoefficients[1] = derivative;

            if ( type == Type.QUADRATIC )
            {
                //c
                localCoefficients[2] = 2 * (knots[index].position - knots[index1].position) +
                                       derivative + derivative1;
            }

            if ( type == Type.CUBIC )
            {
                //c
                localCoefficients[2] = 3 * (knots[index1].position - knots[index].position) - 
                                       2 * derivative - derivative1;
                //d
                localCoefficients[3] = 2 * (knots[index].position - knots[index1].position) +
                                       derivative + derivative1;
            }

            for ( int j = 0; j < localCoefficients.Length; j++ )
            {
                coefficients.Add(localCoefficients[j]);
            }
        }
    }
    
    public Vector3 Evaluate (float time)
    {
        int lowIndex = (int)time;
        Vector3 evaluation = Vector3.zero;

        if (!closed && lowIndex >= knots.Count - 1)
        {
            return knots[knots.Count - 1].position;
        }

        // f(x) += ax^n   or   f(x) = a + bx + cx^2 + dx^3
        for (int count = 0; count < (int)Type.CUBIC; count++ )
        {
            int index = lowIndex % knots.Count * (int)Type.CUBIC + count;
            evaluation += coefficients[index] * Mathf.Pow(time - lowIndex, count);
        }

        return evaluation;
    }



    public Quaternion LookAt ( Vector3 position, float time )
    {
        Vector3 result = Evaluate(time + Time.deltaTime);
        if ( result != position )
        {
            Quaternion rotation = Quaternion.LookRotation(result - position);
            return rotation;
        }
        return Quaternion.identity;
    }

    public void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.blue;
        for (float index = 0; index < knots.Count; index += drawLength)
        {
            Gizmos.DrawLine(Evaluate(index), Evaluate(index + drawLength));
        }
    }
}
