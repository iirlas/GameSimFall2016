using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class Spline : MonoBehaviour, IEnumerable<Transform> {
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

        Transform changingKnot = null;
        public void OnSceneGUI()
        {
            Spline spline = target as Spline;

            if ( spline.transform.hasChanged )
            {
                spline.Build();
            }

            if ( spline.knots.Count > 0 )
            {
                Handles.Label(spline.knots[0].position + Vector3.up, "Start");
                Handles.Label(spline.knots[spline.knots.Count - 1].position + Vector3.up, "End");
            }

            for (int index = 0; index < spline.knots.Count; index++)
            {
                EditorGUI.BeginChangeCheck();
                Vector3 newPosition = Vector3.zero;
                Quaternion newRotation = Quaternion.identity;

                if ( Tools.current == Tool.Move )
                {
                    newPosition = Handles.PositionHandle(spline.knots[index].position, spline.knots[index].rotation);
                }
                if ( Tools.current == Tool.Rotate )
                {
                    newRotation = Handles.RotationHandle(spline.knots[index].rotation, spline.knots[index].position);
                }

                if ( EditorGUI.EndChangeCheck() )
                {
                    changingKnot = spline.knots[index];
                    Undo.RecordObject(spline.knots[index], spline.knots[index].name + " Changed");
                    if (Tools.current == Tool.Move)
                    {
                        spline.knots[index].position = newPosition;
                    }
                    if (Tools.current == Tool.Rotate)
                    {
                        spline.knots[index].rotation = newRotation;
                    }
                    spline.Build();
                }
                else
                {
                    changingKnot = null;
                    Repaint();
                }
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Spline myScript = (Spline)target;

            if ( changingKnot != null )
            {
                GUILayout.Label("Changing " + changingKnot.name);
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
    public Transform Start
    {
        get { return knots.First(); }
    }

    [HideInInspector]
    public Transform End
    {
        get { return knots.Last(); }
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
        //Undo.undoRedoPerformed += Build;
    }

    private Vector3[] buildLinearSet ( int index )
    {
        int count = knots.Count;
        int indexNeg1 = Extension.mod(index - 1, count);
        int index1 = Extension.mod(index + 1, count);
        Vector3 derivative = (knots[index1].position - knots[index].position);

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
        Quaternion rotation = Quaternion.Inverse(knots[index].rotation) * knots[index1].rotation;
        Vector3 derivative = (knots[index1].position - knots[indexNeg1].position);
        Vector3 derivative1 =(knots[index2].position - knots[index].position);

        return new Vector3[]
        {
            knots[index].position,
            derivative,
            3 * (knots[index1].position - knots[index].position) - 2 * derivative - derivative1,
            2 * (knots[index].position - knots[index1].position) + derivative + derivative1
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
            evaluation = knots[knots.Count - 1].position;
        }
        else if ( knots.Count >= 2 && coefficients.Count / knots.Count == (int)type )
        {
            // f(x) = ax^0 + ax^n...
            if ( !closed && ( lowIndex == 0 || lowIndex == knots.Count - 2))
            {
                int index = (lowIndex % knots.Count) + 1;
                evaluation = Vector3.Lerp(knots[lowIndex].position, knots[index].position, time - lowIndex);
            }
            else
            {
                for (int count = 0; count < (int)type; count++)
                {
                    int index = lowIndex % knots.Count * (int)type + count;
                    evaluation += coefficients[index] * Mathf.Pow(time - lowIndex, count);
                }
            }
        }
        return evaluation;
    }


    public Quaternion LookAt ( Vector3 position, float time )
    {
        Vector3 result = Evaluate(time + Time.deltaTime);
        if (result != position)
        {
            int lowIndex = (int)time;
            int index = (int)Mathf.Clamp(time + 1, 0, knots.Count - 1);
            Vector3 upwards = Vector3.Lerp(knots[lowIndex].up, knots[index].up, time - lowIndex);
            Quaternion rotation = Quaternion.LookRotation(result - position, upwards);
            return rotation;
        }
        return Quaternion.identity;
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

    public IEnumerator<Transform> GetEnumerator()
    {
        return ((IEnumerable<Transform>)knots).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<Transform>)knots).GetEnumerator();
    }

    void OnDrawGizmos ()
    {
        if (!drawKnots)
            return;

        Gizmos.color = Color.red;
        for (int index = 0; index < transform.childCount; index++)
        {
            Gizmos.DrawWireSphere(transform.GetChild(index).position, 0.1f);
        }
    }

    void OnDrawGizmosSelected ()
    {
        if (knots.Count < 2 || coefficients.Count / knots.Count != (int)type)
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
