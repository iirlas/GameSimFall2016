using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(SphereCollider))]
public class LightCollider : MonoBehaviour {

    new private Light light;
    [HideInInspector]
    public SphereCollider sphereCollider;
    private Collider[] myHits;
    private Girl kira;
	public float amount = 1.0f;

	// Use this for initialization
	void Awake () {
        light = GetComponent<Light>();
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = light.range;
        kira = GameObject.FindObjectOfType<Girl>();
	}
	
	// Update is called once per frame
    void Update()
    {
    }

    void OnTriggerStay (Collider other)
    {
        if (kira != null &&  kira.transform == other.transform && StatusManager.getInstance().hasStatus(StatusManager.Status.FEAR))
        {
            StatusManager.getInstance().ToggleStatus(StatusManager.Status.FEAR);
        }
		StatusManager.getInstance ().fear -= amount;
    }

    void OnTriggerExit (Collider other)
    {
        if (kira != null && kira.transform == other.transform)
        {
            StatusManager.getInstance().playerStatus = StatusManager.Status.FEAR;
        }
    }

}
