using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

	public string ObjectName;
	private GameObject myObject;

	// Use this for initialization
	void Start () {
		
		myObject = GameObject.Find(ObjectName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Destroy ()
	{
		Destroy (myObject);
	}
}
