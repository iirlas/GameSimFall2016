using UnityEngine;
using System.Collections;
using System.Linq;

public class JaguarBoss : Enemy
{
    public enum jagAttack
    {
        SWIPE = 0,
        FIRE,
        POUNCE,
        RETURN
    }

    public GameObject fireAttack;
    public GameObject swipeAttack;
    public GameObject center;
    public GameObject target;

    public int maxHits = 3;
    public int swipeAttackDamage = 10;
    public int fireDamage = 25;
    public int pounceDamage = 15;
    public int contactDamage = 10;

    private jagAttack currentAttack;

    //--------------------------------------------------------------------------
    // Jaguar Boss States
    private bool hasAttacked;
    private bool isInCenterOfRoom;
    private bool isStunned;
    private bool isAwake;

    private float trackingTimer = 0.0f;
    private float attackLengthTimer = 0.0f;
    private float attackingTimer = 0.0f;
    private float stunTimer = 0.0f;

    private Player thePlayer;

    //==========================================================================
    // Use this for initialization
    void Start()
    {
        this.isAwake = false;
        this.myHealth = this.maxHits;
        this.myDamage = swipeAttackDamage;

        this.myType = enType.JAGUAR;
        this.myState = enState.IDLE;

        //this.mySpeed = 10.0f;
        this.myRotationSpeed = 2.0f;
        thePlayer = PlayerManager.getInstance().players.First(player => { return player != null && player is Girl; });
    }

    //==========================================================================
    // Update is called once per frame
    void Update()
    {
        // if the player hasn't triggered the boss, do nothing
        if (!isAwake)
        {
            return;
        }

        this.transform.LookAt(new Vector3(target.transform.position.x,
                                          this.transform.position.y,
                                          target.transform.position.z));

        // if the boss has been trigger, do the things
        if (this.isStunned)
        {
            this.stunTimer += Time.deltaTime;
            if (stunTimer >= 5.0f)
            {
                this.isStunned = false;
                this.stunTimer = 0.0f;
            }

            return;
        }

        if (this.currentAttack == jagAttack.RETURN)
        {
            this.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(this.center.transform.position);
            if (Vector3.Distance(this.transform.position, this.center.transform.position) < 0.2f)
            {
                this.isInCenterOfRoom = true;
                this.hasAttacked = true;
            }
        }

        if (this.hasAttacked && this.isInCenterOfRoom)
        {
            this.hasAttacked = false;
            chooseNewAttack();
        }
        if (this.isInCenterOfRoom && !this.hasAttacked)
        {
            attack();
        }

        if (StatusManager.getInstance().health <= 0)
        {
            this.resetBoss();
        }
    }

    //==========================================================================
    // Determine attack algorithm to use
    public void attack()
    {
        switch (currentAttack)
        {
            case jagAttack.FIRE:
                doFireAttack();
                break;
            case jagAttack.POUNCE:
                //doPounceAttack();
                break;
            case jagAttack.SWIPE:
                //doSwipeAttacK();
                break;
            default:
                Debug.LogError("This should never log");
                break;
        }
    }

    //=========================================================================
    // Do an attack using the fire breath
    private void doFireAttack()
    {
        if (this.fireAttack.activeSelf)
        {
            this.fireAttack.SetActive(true);
            this.target.transform.position = thePlayer.transform.position;
        }

        this.attackingTimer += Time.deltaTime;

        if (this.attackingTimer >= this.attackLengthTimer)
        {
            this.attackingTimer = 0.0f;
            this.hasAttacked = true;
        }

    }

    //==========================================================================
    // Returns the current health of the jaguar in integer form
    public int currentHealth()
    {
        return this.myHealth;
    }

    //==========================================================================
    // Randomly select a new attack type from the designated attacks
    private void chooseNewAttack()
    {
        this.currentAttack = ((jagAttack)((int)Random.Range(0f, 2.99f)));
        switch (this.currentAttack)
        {
            case jagAttack.FIRE:
                this.attackLengthTimer = 5.0f;
                break;
            case jagAttack.POUNCE:
                this.attackLengthTimer = 2.0f;
                break;
            case jagAttack.SWIPE:
                this.attackLengthTimer = 1.0f;
                break;
            default:
                this.attackLengthTimer = 0.0f;
                Debug.LogError("");
                break;
        }
    }

    //==========================================================================
    // Does damage to the Jaguar, silly.
    public void damageJaguarBoss()
    {
        this.myHealth--;
    }

    //==========================================================================
    // 
    public void triggerBoss()
    {
        this.isAwake = true;
    }

    //==========================================================================
    // 
    public void OnEvent(BasicTrigger trigger)
    {
        if (!this.isAwake)
        {
            this.triggerBoss();
        }
    }

    //==========================================================================
    // If they player dies, we need to reset the boss state
    public void resetBoss()
    {
        this.isAwake = false;
        this.myHealth = this.maxHits;
        this.myDamage = swipeAttackDamage;

        this.myType = enType.JAGUAR;
        this.myState = enState.IDLE;

        //this.mySpeed = 10.0f;
        this.myRotationSpeed = 2.0f;
    }
}
