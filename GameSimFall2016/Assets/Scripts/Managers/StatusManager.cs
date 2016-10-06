using UnityEngine;
using System.Collections;

public class StatusManager : Singleton<StatusManager> {

    public enum Status
    {
        NONE =   0x00,  //0000
        FEAR =   0x01,  //0001
        POISON = 0x02,  //0010
        FIRE =   0x04,  //0100
        SPOOF =  0x08   //1000
    }

    public float isPoisoned;

    private float myHealth = 100;
    public float health {
        get { return myHealth; }
        set { myFear = Mathf.Clamp(value, 0, 100); }
    }

    private float myFear = 0;
    public float fear
    {
        get { return myFear; }
        set { myFear = Mathf.Clamp(value, 0, 120); }
    } 

    private Status myPlayerStatus = Status.NONE;
    public Status playerStatus
    {
        get { return myPlayerStatus; }
        set { SetStatus(value); }
    }

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
    }

    public bool hasStatus(Status status)
    {
        return (myPlayerStatus & status) == status;
    }

	// Update is called once per frame
    void Update()
    {
        if (fear > 0 && !hasStatus(Status.FEAR))
        {
            ToggleStatus(Status.FEAR);
        }

        if (fear >= 100 && !highFearEffect.activeSelf)
        {
            highFearEffect.SetActive(true);
        }
    }	

    void LateUpdate()
    {

    }
}
