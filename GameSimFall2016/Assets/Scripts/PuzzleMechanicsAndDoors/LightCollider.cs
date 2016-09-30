using UnityEngine;
using System.Collections;
using System.Linq;

public class LightCollider : MonoBehaviour {

    private Light light;
    public LayerMask layer;
    private Collider[] myHits;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        Collider[] hits = Physics.OverlapSphere(transform.position, light.range, layer);
        if (myHits != null)
        {
            foreach (var hit in myHits)
            {
                if (hit != null && !hits.Contains(hit))
                {
                    hit.transform.SendMessage("OnActionEnd", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        foreach (var hit in hits)
        {
            hit.transform.SendMessage("OnAction", SendMessageOptions.DontRequireReceiver);
        }
        myHits = hits;
	}
}
