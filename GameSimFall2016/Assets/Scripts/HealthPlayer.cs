using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//========================================================================================================
//                                              HEALTH PLAYER
//  This script has a slider that controls the health of the player. Currently a static variable (healthChange)
//  can be accessed to control change in health (negative numbers for damage, positive for healing).
//
//  Additionally, public values exist for the Slider and the healthMax (temporary, for ease of testing)
//
// public Slider healthBar: hook up the UI slider component in this slot
// public int healthMax: hook up the maximum health here. NOTE: this should eventually not be as public as it is.
// public static int healthChange: this number is for changing the value and is accessible by all other classes.
// private int healthCurrent: stores the current health for display inside the slider
// 
//
//========================================================================================================
public class HealthPlayer : MonoBehaviour {

    public Slider healthBar; //hook the healthbar to this slider spot
    public int healthMax;

    public static int healthChange;

    private int healthCurrent;
    

	// initialises health change, healthCurrent
    // sets maxValue of slider to healthMax
    // sets minValue to 0
	void Start () {

        healthChange = 0;
        healthCurrent = healthMax;
        healthBar.maxValue = healthMax;
        healthBar.minValue = 0;

	
	}
	

    // This updates the health and checks if she should be dead.
    // if dead, debug log tells you she's dead.
	void Update () {
        if (healthChange != 0)
        {
            healthCurrent += healthChange;
            if (healthCurrent > 1)
            {
                healthBar.value = healthBar.minValue;
                Debug.Log("Player is Dead.");
            } else
            {
                healthBar.value = healthCurrent;
            }
            

        }

	
	}
}
