using UnityEngine;
using System.Collections;

public class SplineFollower : MonoBehaviour {

    public enum PathType
    {
        SINGLE,
        LOOP,
        PINGPONG,
    }

    public Spline spline;

    public float speed = 1.0f;

    public PathType pathType = PathType.SINGLE;

    private float time = 0.0f;

    private float direction = 0;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = spline.Evaluate(time);
        transform.rotation = spline.LookAt(transform.position, time, transform.up);

        switch (pathType)
        {
        case PathType.SINGLE:
            if (time >= spline.Length)
            {
                time = spline.Length;
            }
            direction = Time.deltaTime;
            break;

        case PathType.LOOP:
            if (time >= spline.Length)
            {
                time = 0.0f;
            }
            direction = Time.deltaTime;
            break;

        case PathType.PINGPONG:
            if (time <= 0.0f)
            {
                direction = -Time.deltaTime;
                time = 0.0f;
            }
            else if (time >= spline.Length)
            {
                direction = Time.deltaTime;
                time = spline.Length;
            }
            break;
        }
        time += direction * speed;
	}
}
