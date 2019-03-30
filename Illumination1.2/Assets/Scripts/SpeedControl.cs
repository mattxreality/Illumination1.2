﻿using System.Collections;
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

    [SerializeField] float initialSpeed = 10f;
    [SerializeField] float speedMultiplier = 1f;
    float speedFactor;
    public int countOfGatesActivated = 0;
    private float calculatedSpeed;

    [SerializeField] float coolDownValue = 10f; // how long after gate contact before speed decreases
    private float currCoolDownValue; // used for countdown and resetting lights & collision

    void Start()
    {

    }

    void Update()
    {
        SetSpeed();
    }

    void SpeedFactor() // lazy code, there is a better way I'm sure
    {
        if (countOfGatesActivated == 1)
        { speedFactor = speedMultiplier * 2; } // speed increased after 1 gate

        else if (countOfGatesActivated == 2)
        { speedFactor = speedMultiplier * 3; } // speed increased after 2 gates

        else if (countOfGatesActivated == 3)
        { speedFactor = speedMultiplier * 4; } // speed increased after 3 gates
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "accelerator")
        {
            // todo gate is counted twice, or there are two collider interactions
            countOfGatesActivated++;
            print("collided with accelerator gate");
            print("Count of gates activated = " + countOfGatesActivated);

            // FX when player passes through gate
            Instantiate(playerContact, transform.position, transform.rotation);

            countOfGatesActivated++; // increment count of gates activated by 1
            StartCoroutine(StartCountdown(coolDownValue)); // countdown to reset count of gates
        }
    }

    private void SetSpeed()
    {
        if (countOfGatesActivated < 1)
            calculatedSpeed = initialSpeed;
        else
            calculatedSpeed = countOfGatesActivated * speedMultiplier;
        
       
        // changes speed of BetterWaypointFollower
        // todo, speed reduction currently not working. 
        BetterWaypointFollower.instance.routeSpeed = calculatedSpeed; 
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
