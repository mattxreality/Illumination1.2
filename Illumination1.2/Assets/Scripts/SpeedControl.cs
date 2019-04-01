using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script changes the speed of the BetterWaypointFollower script.  
 * 
 * 
 * My other idea is to use free flying, and not use the waypoint follower. I could
 * use this script then, because I would control forward velocity from this script.
 * 
 *  to-do
 *  1) Improve the SpeedFactor()
 *  2) Slow speed after countdown
 * 
 */

public class SpeedControl : MonoBehaviour
{
    [SerializeField] ParticleSystem playerContact;

    [SerializeField] float initialSpeed = 10f; // starting speed
    [SerializeField] float baseSpeedBonus = .1f; // percentage bonus for each gate
    [SerializeField] float speedMultiplier = 1.2f;
    float speedFactor;
    public float speedIncrease;
    int gatesActivatedReal;
    int gatesActivatedMultiplier = 1;
    float currentSpeed;

    [SerializeField] float coolDownValue = 10f; // how long after gate contact before speed decreases
    private float currCoolDownValue; // used for countdown and resetting lights & collision

    public static SpeedControl instance;

    void Awake()
    {
        // check if this is the only instance. If not, destroy this instance.
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gatesActivatedReal = 0;
        print("Initial Speed = " + currentSpeed);
        currentSpeed = initialSpeed;
        BetterWaypointFollower.instance.routeSpeed = currentSpeed;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "accelerator")
        {
            // todo gate is counted twice, or there are two collider interactions

            gatesActivatedReal++; // increment count of gates activated by 1
            gatesActivatedMultiplier++; // increments speed bonus factor by 1
            speedIncrease = gatesActivatedMultiplier * baseSpeedBonus;
            print("collided with accelerator gate");
            print("Count of gates activated = " + gatesActivatedReal);
            print("Current gateMultiplier = " + gatesActivatedMultiplier);
            SetSpeed();

            // FX when player passes through gate
            Instantiate(playerContact, transform.position, transform.rotation);

            StartCoroutine(StartCountdown(coolDownValue)); // countdown to reset count of gates
            
        }
    }

    private void SetSpeed()
    {
        if (currCoolDownValue < 1) // check if countdown timer is finished
        {
            gatesActivatedReal = 1; // reset activated gate count
        }
        if (gatesActivatedReal < 1)
            // todo add lerp to gragually change speed
            currentSpeed = initialSpeed;
        else
            currentSpeed = currentSpeed + (initialSpeed * speedIncrease);
        print("CurrentSpeed = " + currentSpeed);
        // changes speed of BetterWaypointFollower
        // todo, speed reduction currently not working. 
        BetterWaypointFollower.instance.routeSpeed = currentSpeed;
    }


    private IEnumerator StartCountdown(float coolDownValue)
    {
        // counts down based on 'Light Duration" value
        currCoolDownValue = coolDownValue;
        while (currCoolDownValue > 0)
        {
            // Debug.Log("Countdown: " + currCoolDownValue);
            yield return new WaitForSeconds(1.0f);
            currCoolDownValue--;
        }
    }
}
