using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "light" | other.tag == "gate" | other.tag == "accelerator") { return; }

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
