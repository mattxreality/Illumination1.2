using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByProjectile : MonoBehaviour
{
    private GameObject childObject;
    [SerializeField] float coolDownValue = 30f;
    private float currCoolDownValue; // used for countdown and resetting lights & collision

    void Start()
    {
        childObject = transform.GetChild(0).gameObject;
        childObject.SetActive(false); // start inactive
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "projectile")
        {
            childObject.SetActive(true); // enable FX gameObject
            StartCoroutine(StartCountdown(coolDownValue)); // countdown to reset "FX" gameObject
        }
    }

    private IEnumerator StartCountdown(float coolDownValue)
    {
        // counts down based on coolDownValue in seconds
        currCoolDownValue = coolDownValue;
        while (currCoolDownValue > 0)
        {
            // Debug.Log("Countdown: " + currCoolDownValue);
            yield return new WaitForSeconds(1.0f);
            currCoolDownValue--;
        }
    }

    void Update()
    {
        if (currCoolDownValue < 1) // check if countdown timer is finished
        {
            childObject.SetActive(false); // disable FX
        }
    }
}
