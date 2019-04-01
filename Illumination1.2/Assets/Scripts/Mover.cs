using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // todo must normalize projectile speed, currently too slow at beginning, to fast later

    public float speed = 75f;
    public float speedIncrease = 1f;
    [SerializeField] float speedNormalizer = .8f;

    void Start()
    {
        // Start() is sufficient as this script controls projectiles that only
        // live for short periods of time. Each projectile will get the latest
        // speed info from SpeedControl().
        speedIncrease = SpeedControl.instance.speedIncrease; // external reference
        SetProjectileSpeed(speedIncrease);
       
    }

    public void SetProjectileSpeed(float speedIncrease)
    {
        print("projectile speedIncrease received = " + speedIncrease);
        GetComponent<Rigidbody>().velocity = transform.forward * speed * speedIncrease *speedNormalizer;
    }
}
