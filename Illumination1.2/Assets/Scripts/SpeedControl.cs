using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* I wanted this script to cause the speed to change for my player, but I don't
 * know how to change the speed of the waypoint follower from this script. 
 * 
 * For future reference, I would like to make this change the speed of my player
 * and don't yet know how to call external scripts or make changes to external scripts
 * yet. ... Sadness.... This was a major game mechanic I wanted to explore.
 * 
 * My other idea is to use free flying, and not use the waypoint follower. I could
 * use this script then, because I would control forward velocity from this script.
 */

public class SpeedControl : MonoBehaviour
{
    [SerializeField] ParticleSystem playerContact;

    [SerializeField] float initialSpeed = 10f;
    [SerializeField] float speedMultiplier = 1f;
    float speedFactor;
    public int countOfGatesActivated = 0;
    public float currentSpeed;

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
            countOfGatesActivated++;
            print("collided with accelerator gate");
            print("Count of gates activated = " + countOfGatesActivated);

            Instantiate(playerContact, transform.position, transform.rotation);

            countOfGatesActivated++; // increment count of gates activated by 1
            StartCoroutine(StartCountdown(coolDownValue)); // countdown to reset count of gates
        }
    }

    private void SetSpeed()
    {
        if (countOfGatesActivated < 1)
            currentSpeed = initialSpeed;
        else
            currentSpeed = countOfGatesActivated * speedMultiplier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "gate")
        {

        }
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
