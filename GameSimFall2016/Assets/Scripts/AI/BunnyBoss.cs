using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Add phases
[RequireComponent(typeof(Rigidbody))]
public class BunnyBoss : MonoBehaviour {

    public  float health = 100.0f;
    private float time = 0;

    private Transform target
    {
        get { return spline[2]; }
    }

    private bool actionFlag = true;

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

    public Spline spline;

    public GameObject darkOrbPrefab;
    public GameObject bunnyPrefab;

	// Use this for initialization
	void Awake () 
    {
        bloodEffects = GetComponentsInChildren<ParticleSystem>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        orbSpawner = transform.FindChild("OrbSpawner");
        bunnySpawner = GameObject.Find("BunnySpawner").transform;
        spline[0].position = transform.position;
        NewTarget();
        spline.Build();
    }
		
	// Update is called once per frame
	void Update () 
    {
        Quaternion rotation = Quaternion.identity;

        if (actionFlag)
        {
            if (time < spline.Length)
            {
                time += Time.deltaTime;//
                transform.position = spline.Evaluate(time);
                rotation = spline.LookAt(transform.position, time);
            }
            else
            {
                transform.position = spline[2].position;
                time = 0;
                spline[0].position = transform.position;
                NewTarget();
                spline.Build();
                actionFlag = false;
            }
        }
        else if ( time > 2.0f )
        {
            switch ((int)(Random.value * 2))
            {
            case 0:
                Shoot(2.0f);
                break;

            case 1:
                Summon();
                break;

            }
            time = 0;
            actionFlag = !actionFlag;
        }
        else
        {
            Vector3 direction = PlayerManager.getInstance().currentPlayer.transform.position - transform.position;
            direction.y = 0;
            rotation = Quaternion.LookRotation(direction);
            time += Time.deltaTime;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);

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
            foreach ( Bunny bunny in bunnys )
            {
                Destroy(bunny.gameObject);
            }
            Destroy(gameObject);
        }
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
        Vector3 offset = new Vector3(0, nodes[seed].GetComponent<Collider>().bounds.size.y + 2.5f, 0);
        while ((nodes[seed].position + offset) == spline[0].position)
        {
            seed = Random.Range(0, nodes.Length - 1);
            offset = new Vector3(0, nodes[seed].GetComponent<Collider>().bounds.size.y + 2.5f, 0);
        }
        target.position = nodes[seed].position + offset;
    }
}
