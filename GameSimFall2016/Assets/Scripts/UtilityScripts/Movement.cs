using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public enum Type
    {
        SINGLE,
        LOOP,
        ROCK,
    }

    public float movementSpeed = 5.0f;
    public bool  canRotate;
    public float rotationSmoothSpeed = 10.0f;


    public Type type;

    public bool ignoreY = false;

    public bool canMove;

    public MonoBehaviour eventScript;

    public Transform[] targetNodes;

    [HideInInspector]
    public new Transform transform;


    private int myIndex = 0;
    private int myIndexDirection = 1;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
        canMove = true;
        eventScript.SendMessage("OnMovementStart", this);
	}
	
	// Update is called once per frame
	public void Update () 
    {
        if (!canMove)
            return;

        Quaternion rotation = transform.rotation;
        Vector3 nextTarget = targetNodes[myIndex].position;

        if (ignoreY)
        {
            nextTarget.y = transform.position.y;
        }

        if (transform.position == nextTarget)
        {
            do
            {
                int nextIndex = myIndex + myIndexDirection;
                switch (type)
                {
                case Type.SINGLE:
                    if (nextIndex >= targetNodes.Length)
                    {
                        canMove = false;
                        nextIndex = targetNodes.Length - 1;
                        eventScript.SendMessage("OnMovementEnd");
                    }
                    break;

                case Type.LOOP:
                    nextIndex = nextIndex % targetNodes.Length;
                    if (myIndex == (targetNodes.Length - 1))
                    {
                        eventScript.SendMessage("OnMovementEnd");
                    }
                    break;

                case Type.ROCK:
                    if (nextIndex >= targetNodes.Length ||
                        nextIndex < 0)
                    {
                        myIndexDirection = -myIndexDirection;
                        nextIndex = Mathf.Clamp(nextIndex, 0, targetNodes.Length - 1);
                        eventScript.SendMessage("OnMovementEnd");
                    }
                    break;
                }
                myIndex = nextIndex;
            } while (!targetNodes[myIndex]);
            eventScript.SendMessage("OnNodeReached");
        }
        else
        {
            rotation = Quaternion.LookRotation(nextTarget - transform.position);
        }
        if ( canRotate )
        { 
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSmoothSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, nextTarget, movementSpeed * Time.deltaTime);
	}

    public void OnDrawGizmos()
    {
        foreach ( Transform node in targetNodes )
        {
            if (node)
            { 
                Gizmos.DrawIcon(node.position, "Marker.png");
            }
        }
    }


}