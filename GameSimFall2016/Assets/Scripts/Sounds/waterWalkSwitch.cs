using UnityEngine;
using System.Collections;

public class waterWalkSwitch : MonoBehaviour {

	public Material waterMaterial;
	void Start () {
	
	}
	

	void Update () {
	
	}


	void OnTriggerEnter(Collider col){

		Debug.Log ("Enters Collider");// is working jsut need to determine the name of the next level of the object then to get the different materials.

		if(col.gameObject.tag.Equals("LevelFloor")){
			Debug.Log ("Made it to the Level Floor");
			if (col.GetComponent<MeshRenderer>().material == waterMaterial) {
				Debug.Log ("Stepping on the water");
			}
		}

	}
}
