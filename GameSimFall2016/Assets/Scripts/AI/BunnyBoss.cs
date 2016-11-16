using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Add phases
[RequireComponent(typeof(Rigidbody))]
public class BunnyBoss : MonoBehaviour {

    public  float health = 100.0f;
    private float time = 0;

    private Transform target;

    private Transform orbSpawner;
    private Transform bunnySpawner;

    private List<Bunny> bunnys = new List<Bunny>();

    [HideInInspector]
    public Rigidbody rigidbody;

    [HideInInspector]
    public Collider collider;

    [HideInInspector]
    public ParticleSystem[] bloodEffects;

    public BasicTrigger trigger;

    public Transform[] nodes;


    public GameObject darkOrbPrefab;
    public GameObject bunnyPrefab;

    public AnimationCurve xCurve;
    public AnimationCurve yCurve;
    public AnimationCurve zCurve;


	// Use this for initialization
	void Awake () 
    {
        bloodEffects = GetComponentsInChildren<ParticleSystem>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        orbSpawner = transform.FindChild("OrbSpawner");
        bunnySpawner = GameObject.Find("BunnySpawner").transform;
        NewTarget();
    }
		
	// Update is called once per frame
	void Update () 
    {
        Vector3 direction = Vector3.zero;
        Quaternion rotation = Quaternion.identity;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, collider.bounds.size.y))
        {
            time += Time.deltaTime;//

            if (time > 1.0f)
            {
                NewTarget();
                time = 0.0f;
                switch ((int)(Random.value * 2))
                {
                case 0:
                    Shoot(2.0f);
                    break;

                case 1:
                    Summon();
                    break;

                }
                Jump(15.0f);
            }
            direction = (PlayerManager.getInstance().currentPlayer.transform.position - transform.position);
            direction.y = 0;
        }
        else
        {
            direction = (target.position - transform.position);
            direction.y = 0;
            if ( direction.magnitude > 1.0f )
            {
                rigidbody.velocity += direction.normalized;
            }
            else
            {
                rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, Vector3.up * rigidbody.velocity.y, time);
            }
        }
        if ( direction != Vector3.zero )
        {
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, time);
        }

        if (Input.GetKey(KeyCode.Delete))
        {
            health = 0;
        }

        bunnys.RemoveAll(bunny => { 
            if ( !bunny.gameObject.activeSelf )
            {
                foreach (ParticleSystem effect in bloodEffects)
                {
                    effect.Play();
                }
                Destroy(bunny.gameObject);
                health -= 5.0f;
                return true;
            }
            return false;
        });

        if ( health <= 0 )
        {
            trigger.OnAction();
            Destroy(gameObject);
        }
	}

    void Jump (float forceFactor)
    {
        rigidbody.AddForce(Vector3.up * forceFactor, ForceMode.Impulse);

    }

    void Shoot (float forceFactor)
    {
        GameObject darkOrb = Instantiate(darkOrbPrefab, orbSpawner.position, Quaternion.identity) as GameObject;
        darkOrb.GetComponent<Rigidbody>().velocity = (PlayerManager.getInstance().currentPlayer.transform.position - orbSpawner.position) * forceFactor;
    }

    void Summon ()
    {
      if (bunnys.Sum( bunny => { return (bunny.gameObject.activeSelf) ? (1) : (0); } ) < 4)
      {
         GameObject bunnyObj = Instantiate(bunnyPrefab, bunnySpawner.position, Quaternion.identity) as GameObject;
         bunnys.Add(bunnyObj.GetComponent<Bunny>());
      }
    }

    void NewTarget ()
    {
        int seed = Random.Range(0, nodes.Length - 1);
        while (target != null && nodes[seed] == target)
        {
            seed = Random.Range(0, nodes.Length - 1);
        }
        target = nodes[seed];
    }
}
