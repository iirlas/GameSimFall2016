using UnityEngine;
using System.Collections;

public class Slant : MonoBehaviour {
	public Transform start;
	public Transform end;
	public BasicTrigger basicTrigger;

	void Start ()
	{
		transform.position = start.position;
	}

	public void Toggle ()
	{
		if (transform.position == start.position)
		{
			ToEnd ();
		}
		else if (transform.position == end.position)
		{
			ToStart ();
		}
	}

	public void ToStart ()
	{
		transform.position = start.position;
		basicTrigger.OnActionEnd ();
	}

	public void ToEnd ()
	{
		transform.position = end.position;
		basicTrigger.OnAction ();
	}
}
