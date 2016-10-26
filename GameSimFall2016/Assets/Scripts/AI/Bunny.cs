//=============================================================================
// Author:  Nathan C.
// Version: 1.0
// Date:    08/26/2016
// Ownership belongs to all affiliates of Scott Free Games.
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Linq;

//=============================================================================
// Bunny.cs
// Bunny will be one of the various enemies found within the game.
// The Bunny will:
// - Deal five points of damage
// - Be defeated in two hits
// - Consistently follow player, stopping next to the player to attack.
public class Bunny : Enemy
{
    //-----------------------------------------------------------------------------
    // Public Inspector-editable variables
    [Tooltip("Changing this value will change the detection radius of the Bunny.")]
    private float detectionRadius; // How far out the Bunny will search for the player.

    [Tooltip("Checkmark this box if you wish to provide custom values below.")]
    public bool overrideValues;  //If true, overwrites the default values for health, damage, speed
    //and rotationspeed with values provided in the inspector
    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public int BUNNYHealthCustom;   // the new health value to replace the default.

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public int BUNNYDamageCustom;   // the new damage value to replace the default.

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public float BUNNYSpeedCustom;  // the new speed value to replace the default.

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public float BUNNYRotationSpeedCustom;   // the new rotational speed value to replace the default.

    //-----------------------------------------------------------------------------
    // Default values for an Bunny, provided by juan.
    private const int BUNNYHEALTHDEFAULT = 2;
    private const int BUNNYDAMAGEDEFAULT = 5;
    private const float BUNNYSPEEDDEFAULT = 1;
    private const float BUNNYROTATIONSPEEDDEFAULT = 1;
    private const float ATTACKINTERVAL = 1.0f;        // How often the Bunny will attack

    //[HideInInspector]
    //public bool is;

    [HideInInspector]
    public NavMeshAgent agent;

    // Bunny Sounds 
    public AudioSource BUNNYWalking;
    public AudioSource BUNNYSplat;
    public AudioSource BUNNYAttack;


    //-----------------------------------------------------------------------------
    // Private member variable data.
    private float timeSinceLastAttack = 0.0f;   // The time elapsed since this Bunny has last attacked.
    private bool isInAttackRadius = false;     // Is the player within this Bunny's attack radius?

    //-----------------------------------------------------------------------------
    // A reference to the player.
    protected Player thePlayer;

    //=============================================================================
    // Initialize things here
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (overrideValues)  // If custom values are provided, assign them to this Bunny.
        {
            this.myHealth = BUNNYHealthCustom;
            this.myDamage = BUNNYDamageCustom;
            this.mySpeed = BUNNYSpeedCustom;
            this.myRotationSpeed = BUNNYRotationSpeedCustom;
        }
        else  // If custom values are not provided, utilize the default values for this Bunny.
        {
            this.myHealth = BUNNYHEALTHDEFAULT;
            this.myDamage = BUNNYDAMAGEDEFAULT;
            this.mySpeed = BUNNYSPEEDDEFAULT;
            this.myRotationSpeed = BUNNYROTATIONSPEEDDEFAULT;
        }

        this.detectionRadius = 40;
        this.myType = enType.ANT;

    }

    void Start()
    {
        thePlayer = PlayerManager.getInstance().players.First(player => { return player != null && player is Girl; });

    }

    //=============================================================================
    // Update is called once per frames
    void Update()
    {
        switch (this.myState)
        {
        //-----------------------------------------------------------------------------
        case enState.IDLE:
            //do nothing, just hang out m80
            break;
        //-----------------------------------------------------------------------------
        case enState.TRACK:
            pursuePlayer();
            break;
        //-----------------------------------------------------------------------------
        case enState.ATTACK:
            attackPlayer();
            break;
        //-----------------------------------------------------------------------------
        case enState.MOVE:
            //do some patroling, maybe?
            break;
        //-----------------------------------------------------------------------------
        case enState.DEAD:

            killBunny();
            break;
        //-----------------------------------------------------------------------------
        default:
            break;
        }

        checkBunnyHealth();
        stateUpdate();
    }

    //=============================================================================
    // Updates the state of this BUNNY, if needed.
    void stateUpdate()
    {
        switch (this.myState)
        {
        //-----------------------------------------------------------------------------
        case enState.IDLE:
            //check to see if the player has entered the aggression radius
            if (isPlayerNearby())
            {
                this.myState = enState.TRACK;
            }
            break;
        //-----------------------------------------------------------------------------
        case enState.TRACK:
            //check to see if the player has left aggression radius
            if (!isPlayerNearby())
            {
                this.myState = enState.IDLE;
            }
            break;
        //-----------------------------------------------------------------------------
        case enState.ATTACK:
            //check to see if the player has left the attack radius
            if (isPlayerNearby() && !isInAttackRadius)
            {
                this.myState = enState.MOVE;
            }
            // check to see if the player has left the aggression radius
            else if (!isPlayerNearby() && !isInAttackRadius)
            {
                this.myState = enState.IDLE;
            }
            break;
        //-----------------------------------------------------------------------------
        case enState.MOVE:
            //if the Bunny will patrol, patrol the BUNNY around.
            break;
        //-----------------------------------------------------------------------------
        case enState.DEAD:
            //Bunny is dead, object should be destroyed, if not already.

            killBunny();
            break;
        }
    }

    //=============================================================================
    // If something enters the trigger box, do something based upon it's type.
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Projectile"))
        {
            damageBunny();

        }
    }

    //=============================================================================
    // If something enters the trigger box, do something based upon it's type.
    void OnTriggerStay(Collider other)
    {
        if (other.transform.name.Equals("Kira") && this.myState != enState.ATTACK)
        {
            this.myState = enState.ATTACK;
            if (this.BUNNYAttack.isPlaying == false)
            {
                this.BUNNYAttack.Play();
            }
        }
    }

    //=============================================================================
    // If something enters the trigger box, do something based upon it's type.
    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            this.myState = enState.TRACK;
        }
    }

    //=============================================================================
    // Check to see if the health of this Bunny is 0, if so, change the state of this
    // Bunny to enState.DEAD
    void checkBunnyHealth()
    {
        if (isDefeated())
        {
            if (this.BUNNYSplat.isPlaying == false)
            {
                this.BUNNYSplat.Play();
            }
            this.myState = enState.DEAD;
            this.BUNNYWalking.Stop();

        }
    }

    //=============================================================================
    // Follow the player around, until the player enters the hitbox for attacking.
    void pursuePlayer()
    {
        if (!agent.isOnNavMesh)
            return;

        //Plays BUNNY walking sound if it is not player already.
        if (this.BUNNYWalking.isPlaying == false)
        {
            this.BUNNYWalking.Play();
        }

        if (Vector3.Distance(this.transform.position, thePlayer.transform.position) >= 0.1f)
        {
            agent.destination = thePlayer.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    //=============================================================================
    // Returns whether or not the player is within aggression radius.
    bool isPlayerNearby()
    {
        bool withinX = false;
        bool withinY = false;
        bool withinZ = false;

        if (Mathf.Abs(this.transform.position.x - thePlayer.transform.position.x) <= this.detectionRadius)
        {
            withinX = true;
        }
        if (Mathf.Abs(this.transform.position.y - thePlayer.transform.position.y) <= this.detectionRadius)
        {
            withinY = true;
        }
        if (Mathf.Abs(this.transform.position.z - thePlayer.transform.position.z) <= this.detectionRadius)
        {
            withinZ = true;
        }

        return ((withinX == withinY) && (withinY == withinZ) && (withinZ == withinX));
    }

    //=============================================================================
    // Attack the player, and prevent damaging for a small period of time.
    void attackPlayer()
    {
        if (timeSinceLastAttack == 0.0f)
        {
            if (StatusManager.getInstance().health < 1)
            {
            }
            else
            {
                //do damage to player.
                Debug.Log(this.name + " has damaged the player.");
                //thePlayerHealth.GetComponent<HealthPlayer>().modifyHealth(-5);
                StatusManager.getInstance().health -= 5;
                StatusManager.getInstance().fear += 10;
            }

            timeSinceLastAttack += Time.deltaTime;
        }
        else
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (timeSinceLastAttack >= ATTACKINTERVAL)
        {
            timeSinceLastAttack = 0;
        }
    }

    //=============================================================================
    // "Destroys" the BUNNY and all assosciated gameobjects.
    // ALL enemies will be moved to a magic value, where they will be deactivated.
    void killBunny()
    {

        this.GetComponent<NavMeshAgent>().enabled = false;  // Disable the NavmeshAgent in order to prevent the Bunny
        // from clipping back onto the platform after being "killed".
        this.transform.position = OUTOFBOUNDS;              // Move this Bunny out of bounds to the predefined location.
        this.gameObject.SetActive(false);                   // Disable this Bunny, preventing interactability.
    }

    //=============================================================================
    // Deal a single point of damage to the BUNNY.
    void damageBunny()
    {
        this.myHealth -= 1;

    }

    //=============================================================================
    // Deal a specific amount of damage to the BUNNY.  A negative number may be
    // passed to heal the BUNNY by the passed amount.
    void damageBunny(int damage)
    {
        this.myHealth -= damage;
    }

}
