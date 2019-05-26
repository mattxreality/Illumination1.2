using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    #region Projectile
    public GameObject shotGO;
    public Transform shotGOSpawn;
    [SerializeField] GameObject shotPS;
    ParticleSystem particleSystemShot;
    public float fireRate = 10f;
    private float nextFire;
    #endregion

    void Update()
    {
        // todo move input to player controller. Call projectile method from MAXRPlayerController.
        //if (Input.GetButton("Fire1") && Time.time > nextFire)
        //{
        //    nextFire = Time.time + fireRate;
        //    Instantiate(shotGO, shotGOSpawn.position, shotGOSpawn.rotation);
        //}

        //Oculus Touch Triggers
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) >= Mathf.Epsilon ||
            OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= Mathf.Epsilon &&
            Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shotGO, shotGOSpawn.position, shotGOSpawn.rotation);
        }

        PSShotFire();
    }

    private void PSShotFire()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            shotPS.GetComponent<ParticleSystem>().Play();
        }
        if (Input.GetButtonUp("Fire3"))
        {
            shotPS.GetComponent<ParticleSystem>().Stop();
        }
    }
}
