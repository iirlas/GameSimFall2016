using UnityEngine;
using System.Collections;

public class StatusManager : Singleton<StatusManager> {

    public enum Status
    {
        NONE =   0x00,  //0000
        FEAR =   0x01,  //0001
        POISON = 0x02,  //0010
        FIRE =   0x04,  //0100
        SPOOK =  0x08   //1000
    }

    private float myFear = 0.0f;
    public float fear
    {
        get { return myFear; }
        set
        {
            myFear = Mathf.Clamp(value, 0, 120.0f);
            if (myFear > 0)
            {
                playerStatus |= Status.FEAR;
            }
            else
            {
                playerStatus &= ~Status.FEAR;
            }
            updateEffects();
        }
    }

    [HideInInspector]
    public bool isPoisoned
    {
        get { return hasStatus(Status.POISON); }
        set
        {
            if (value)
            {
                playerStatus |= Status.POISON;
            }
            else
            {
                playerStatus &= ~Status.POISON;
            }
            updateEffects();
        }
    }

    [HideInInspector]
    public bool onFire
    {
        get { return hasStatus(Status.FIRE); }
        set 
        {
            if ( value )
            {
                playerStatus |= Status.FIRE;
            }
            else
            {
                playerStatus &= ~Status.FIRE;
            }
            updateEffects();
        }
    }

    [HideInInspector]
    public bool isSpooked
    {
        get { return hasStatus(Status.SPOOK); }
        set
        {
            if (value)
            {
                playerStatus |= Status.SPOOK;
            }
            else
            {
                playerStatus &= ~Status.SPOOK;
            }
            updateEffects();
        }
    }

    private float myHealth = 100.0f;
    public float health {
        get { return myHealth; }
        set { myHealth = Mathf.Clamp(value, 0, 100.0f); }
    }

    private float myStamina;
    public float stamina
    {
        get { return myStamina; }
        set { myStamina = Mathf.Clamp(value, 0, 100.0f); }
    }

    private Status myPlayerStatus = Status.NONE;
    public Status playerStatus
    {
        get { return myPlayerStatus; }
        set { SetStatus(value); updateEffects(); }
    }

    [Range(0.0f, 1.0f)]
    public float fearDamage;

    [Range(0.0f, 1.0f)]
    public float poisonDamage;

    [Range(0.0f, 1.0f)]
    public float fireDamage;

    [Range(0.0f, 1.0f)]
    public float spookDamage;


    public GameObject fearEffect;
    public GameObject highFearEffect;
    public GameObject poisonEffect;
    public GameObject fireEffect;


    //public GameObject 

	// Use this for initialization
	override protected void Init () {
        playerStatus = Status.FEAR;
	}
    /// <summary>
    /// this is a function to switch state., Use this to toggle player's particle effects
    /// </summary>
    /// <param name="s"></param>
    public void ToggleStatus (Status status)
    {
        if (status == Status.NONE)
        {
            myPlayerStatus = Status.NONE;
        }
        else
        {
            myPlayerStatus ^= status;
        }
        updateEffects();
    }

    /// <summary>
    /// this is a function to set state., Use this to sets the player's particle effects
    /// </summary>
    /// <param name="s"></param>
    public void SetStatus(Status status)
    {
        if (status == Status.NONE)
        {
            myPlayerStatus = Status.NONE;
        }
        else
        {
            myPlayerStatus |= status;
        }
        updateEffects();
    }

    public void updateEffects ()
    {
        fearEffect.SetActive(hasStatus(Status.FEAR));
        poisonEffect.SetActive(hasStatus(Status.POISON));
        fireEffect.SetActive(hasStatus(Status.FIRE));
        highFearEffect.SetActive(fear >= 100 && !highFearEffect.activeSelf);
    }

    public bool hasStatus(Status status)
    {
        return (myPlayerStatus & status) == status;
    }

	// Update is called once per frame
    void Update()
    {
        //fear accumulation
        fear += (hasStatus(Status.FEAR) ? fearDamage : -fearDamage) * Time.deltaTime;

        //fear damage
        health -= (fear >= 100 ? fearDamage : 0.0f) * Time.deltaTime;

        //poison damage
        health -= (isPoisoned ? poisonDamage : 0.0f) * Time.deltaTime;

        //fire damage
        health -= (onFire ? fireDamage : 0.0f) * Time.deltaTime;

        //spook damage
        health -= (isSpooked ? spookDamage : 0) * Time.deltaTime;
    }
}
