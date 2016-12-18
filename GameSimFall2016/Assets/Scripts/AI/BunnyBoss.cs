using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// The AI for the bunnyBoss fight.
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

    public Timer timer = new Timer();

    //------------------------------------------------------------------------------------------------
    private Animator myAnimator;
    public Animator animator
   {
      get
      {
         if ( myAnimator == null )
         {
            myAnimator = GetComponent<Animator>();
         }
         return myAnimator;
      }
   }

    //------------------------------------------------------------------------------------------------
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

    //------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update () 
    {
        Quaternion rotation = Quaternion.identity;

        if (actionFlag) 
        {
            if (time < spline.Length) // Moves the boss alone the spline.
            {
                time += Time.deltaTime;//
                transform.position = spline.Evaluate(time);
                rotation = spline.LookAt(transform.position, time);
            }
            else // Ends the boss's movement
            {
                transform.position = spline[2].position;
                time = 0;
                spline[0].position = transform.position;
                NewTarget();
                spline.Build();
                actionFlag = false;
                timer.start();
                //animator.speed = 1;
            }
        }
        else
        {
           if (time > 5.0f)
           {
              time = 0;
              actionFlag = !actionFlag;
              animator.SetTrigger("jump");
              //animator.speed = spline.Length * Time.deltaTime;
            }
            else if (timer.elapsedTime() > 1.0f) // The actionable phases for the boss.
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
               timer.reset();
            }
            // rotates the Boss to face the player.
            Vector3 direction = PlayerManager.getInstance().currentPlayer.transform.position - transform.position;
            rotation = Quaternion.LookRotation(direction);
            direction.y = 0;
            time += Time.deltaTime;
        }

      transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 5);

        if (Input.GetKey(KeyCode.Delete)) // Debug key for quickly ending the boss fight
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

    //------------------------------------------------------------------------------------------------
    // Spawns a darkOrb in front of the boss.
    // The darkOrb will be directed towards the player by the forceFactor.
    void Shoot (float forceFactor)
    {
        GameObject darkOrb = Instantiate(darkOrbPrefab, orbSpawner.position, Quaternion.identity) as GameObject;
        darkOrb.GetComponent<Rigidbody>().velocity = (PlayerManager.getInstance().currentPlayer.transform.position - orbSpawner.position) * forceFactor;
    }

    //------------------------------------------------------------------------------------------------
    // Spawns a enemy bunny at the bunnySpawner's position.  
    // The bunny will attack the player using the Ant's movement patterns.
    // The death of the bunny will correlate damage to the main boss.
    void Summon ()
    {
      if (bunnys.Sum( bunny => { return (bunny.gameObject.activeSelf) ? (1) : (0); } ) < 4)
      {
         GameObject bunnyObj = Instantiate(bunnyPrefab, bunnySpawner.position, Quaternion.identity) as GameObject;
         bunnys.Add(bunnyObj.GetComponent<Bunny>());
      }
    }

    //------------------------------------------------------------------------------------------------
    // Set a new end point for the spline for the Boss to follow when jumping.
    // The target is selected between the 7 columns, if the starting and ending point are equal, 
    // then a new target is selected.
    void NewTarget()
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
