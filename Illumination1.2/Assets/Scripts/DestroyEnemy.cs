using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public GameObject explosion;
    //public GameObject playerExplosion;
    public int scoreValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "projectile")
        { 
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        /*
         * print("Object triggered something");
         * print("other game object = " + other.gameObject.name);
         * print("other game object tag = " + other.gameObject.tag);
        */

        Destroy(gameObject);
    }
    }
}
