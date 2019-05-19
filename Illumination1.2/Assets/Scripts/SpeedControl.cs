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

   
    GateBoard gateBoard; // UI Class
    SpeedBoard speedBoard; //UI Class

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
        print("starting corrCoolDownValue " + currCoolDownValue);
        currentSpeed = initialSpeed;
        print("currentSpeed = " + currentSpeed);
        BetterWaypointFollower.instance.routeSpeed = currentSpeed;
        gateBoard = FindObjectOfType<GateBoard>(); // searches for the instance of GateBoard throughout my entire scene. Instantiate.
        speedBoard = FindObjectOfType<SpeedBoard>();
        SetSpeed();
    }

    private void Update()
    {
        if (currCoolDownValue <= Mathf.Epsilon ) // check if countdown timer is finished
        {
            gatesActivatedReal = 0; // reset activated gate count
            gatesActivatedMultiplier = 1; // reset multiplier
            // print("SpeedControl Mathf.Epsilon used");
            SetSpeed();
            gateBoard.GateHit(gatesActivatedReal); // Updates UI counter
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "accelerator")
        {
            // todo gate is counted twice, or there are two collider interactions

            print("collided with accelerator gate");
            gatesActivatedReal++; // increment count of gates activated by 1
            gatesActivatedMultiplier++; // increments speed bonus factor by 1
            speedIncrease = gatesActivatedMultiplier * baseSpeedBonus;
            
            print("Current gateMultiplier = " + gatesActivatedMultiplier);
            SetSpeed();

            // FX when player passes through gate
            Instantiate(playerContact, transform.position, transform.rotation);

            StartCoroutine(StartCountdown(coolDownValue)); // countdown to reset count of gates

            gateBoard.GateHit(gatesActivatedReal); // Updates UI counter
            print("Count of gates activated = " + gatesActivatedReal);
        }
    }

    private void SpeedUIUpdate()
    {
        speedBoard.SpeedUpdate(currentSpeed);
    }

    private void SetSpeed()
    {
        if (gatesActivatedReal == 0)
        {   
            // todo add lerp to gragually change speed
            currentSpeed = initialSpeed;
            SpeedUIUpdate();
        }
        else
        {
            currentSpeed = currentSpeed + (initialSpeed * speedIncrease);
            print("CurrentSpeed = " + currentSpeed);
            SpeedUIUpdate();
            // changes speed of BetterWaypointFollower
            // todo, speed reduction currently not working. 
            BetterWaypointFollower.instance.routeSpeed = currentSpeed;
        }
    }


    private IEnumerator StartCountdown(float coolDownValue)
    {
        // count down
        currCoolDownValue = coolDownValue;
        print("coroutine initial set currCoolDownValue " + currCoolDownValue);
        while (currCoolDownValue > 0)
        {
            // Debug.Log("Countdown: " + currCoolDownValue);
            yield return new WaitForSeconds(1.0f);
            currCoolDownValue--;
            print("Cooldown timer value " + currCoolDownValue);
        }
    }
}
