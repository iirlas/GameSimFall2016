using UnityEngine;
using System.Collections;

public class NumberPlay : MonoBehaviour
{
	[HideInInspector]
	public Animator animator;

	public UnityEngine.Events.UnityEvent OnDown;

    // Use this for initialization
    void Start()
    {
		animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("Kira"))
        {

			if (animator.GetBool("MoveDown") != true)
            {
				this.animator.SetBool("MoveDown", true);
				OnDown.Invoke ();
            }
        }
    }
}
