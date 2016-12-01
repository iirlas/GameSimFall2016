//=============================================================================
// Author:  Nathan C.
// Version: 1.0
// Date:    08/26/2016
// Ownership belongs to all affiliates of Scott Free Games.
// Bunny.cs will represent one of the enemies in the game.
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Linq;

public class Bunny : Enemy
{
    //-----------------------------------------------------------------------------
    // Public Inspector-editable variables
    [Tooltip("Changing this value will change the detection radius of the Bunny.")]
    public float detectionRadius = 5;

    [Tooltip("Checkmark this box if you wish to provide custom values below.")]
    public bool overrideValues;

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public int bunnyHealthCustom;

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public int bunnyDamageCustom;

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public float bunnySpeedCustom;

    [Tooltip("If you wish to override this value, checkmark \"Override Values\"")]
    public float bunnyRotationSpeedCustom;

    //-----------------------------------------------------------------------------
    // Default values for an Bunny, provided by juan.
    private const int ANTHEALTHDEFAULT = 2;
    private const int ANTDAMAGEDEFAULT = 5;
    private const float ANTSPEEDDEFAULT = 2;
    private const float ANTROTATIONSPEEDDEFAULT = 1;
    private const float ATTACKINTERVAL = 1.0f;

    private Vector3 startPos;
    private Vector3 targetDestination;
    private bool targetIsPlayer;

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
        if (overrideValues)  // If custom values are provided, assign them to this Bunny.
        {
            this.myHealth = bunnyHealthCustom;
            this.myDamage = bunnyDamageCustom;
            //this.mySpeed = bunnySpeedCustom;
            this.myRotationSpeed = bunnyRotationSpeedCustom;
        }
        else  // If custom values are not provided, utilize the default values for this Bunny.
        {
            this.myHealth = ANTHEALTHDEFAULT;
            this.myDamage = ANTDAMAGEDEFAULT;
            this.myRotationSpeed = ANTROTATIONSPEEDDEFAULT;
        }

        //this.GetComponent<NavMeshAgent>().speed = this.mySpeed;
        this.myType = enType.ANT;

    }

    //=============================================================================
    // Post Initialization things here
    void Start()
    {
        thePlayer = PlayerManager.getInstance().players.First(player => { return player != null && player is Girl; });
        this.startPos = this.transform.position;
    }

    //=============================================================================
    // Update is called once per frames
    void Update()
    {
        switch (this.myState)
        {
        case enState.IDLE:
            break;
        case enState.TRACK:
            pursueTarget();
            break;
        case enState.ATTACK:
            attackPlayer();
            break;
        case enState.MOVE:
            break;
        case enState.DEAD:
            killBunny();
            break;
        default:
            break;
        }

        checkBunnyHealth();
        stateUpdate();
    }

    //=============================================================================
    // Sets the detection radius of this bunny to the passed value.
    public void setDetectionRadius(float radius)
    {
        this.detectionRadius = radius;
    }

    //=============================================================================
    // Updates the state of this bunny, if needed.
    void stateUpdate()
    {
        switch (this.myState)
        {
        //-----------------------------------------------------------------------------
        case enState.IDLE:
            //check to see if the player has entered the aggression radius
            if (isPlayerNearby())
            {
                this.targetIsPlayer = true;
                this.targetDestination = thePlayer.transform.position;
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
            SoundManager.getInstance().playEffect("AntSplat");
        }
    }

    //=============================================================================
    // If something enters the trigger box, do something based upon it's type.
    void OnTriggerStay(Collider other)
    {
        if (other.transform.name.Equals("Kira") && this.myState != enState.ATTACK)
        {
            this.myState = enState.ATTACK;
            this.targetIsPlayer = false;
            this.targetDestination = startPos;

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
            this.myState = enState.DEAD;
        }
    }

    //=============================================================================
    // Follow the player around, until the player enters the hitbox for attacking.
    void pursueTarget()
    {
        this.transform.LookAt(new Vector3(targetDestination.x,
                                          this.transform.position.y,
                                          targetDestination.z));

        if (this.targetIsPlayer == false)
        {
            if (Vector3.Distance(this.targetDestination, this.transform.position) <= 0.2f)
            {
                Debug.Log("Targeting Player");
                this.targetDestination = thePlayer.transform.position;
                this.targetIsPlayer = true;
            }
        }
        else
        {
            this.targetDestination = thePlayer.transform.position;
        }

        this.GetComponent<NavMeshAgent>().SetDestination(targetDestination);
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
                SoundManager.getInstance().playEffect("Ant_Attack_01");
                StatusManager.getInstance().health -= 5;
                StatusManager.getInstance().fear += 5;
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
    // "Destroys" the bunny and all assosciated gameobjects.
    // ALL enemies will be moved to a magic value, where they will be deactivated.
    void killBunny()
    {

        this.GetComponent<NavMeshAgent>().enabled = false;  // Disable the NavmeshAgent in order to prevent the Bunny
        // from clipping back onto the platform after being "killed".
        this.transform.position = OUTOFBOUNDS;              // Move this Bunny out of bounds to the predefined location.
        this.gameObject.SetActive(false);                   // Disable this Bunny, preventing interactability.
    }

    //=============================================================================
    // Deal a single point of damage to the bunny.
    void damageBunny()
    {
        this.myHealth -= 1;

    }

    //=============================================================================
    // Deal a specific amount of damage to the bunny.  A negative number may be
    // passed to heal the bunny by the passed amount.
    void damageBunny(int damage)
    {
        this.myHealth -= damage;
    }

}
