using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;
    //public GameObject playerExplosion;
    [SerializeField] Transform parent;

    void Start()
    {
        AddNonTriggerSphereCollider(); // use often when adding components to asset pack gameObjects
    }

    private void AddNonTriggerSphereCollider()
    {
        Collider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
    }

    void OnTriggerEnter(Collider other) // action for gameObject collision
    {
        if (other.tag == "projectile")
        {
            ProcessDestruction();
        }
    }

    private void ProcessDestruction()
    {
        if (explosion != null)
        {
            GameObject fx = Instantiate(explosion, transform.position, transform.rotation); // or for no rotation use 'Quaternion.identity'
            fx.transform.parent = parent; // set parent for all explosions to 'Spawn At Runtime'
            // this is an 'ok' approach, but I'd rather use the 'Destroy by Time' script on each explosion
        }
        /*
         * print("Object triggered something");
         * print("other game object = " + other.gameObject.name);
         * print("other game object tag = " + other.gameObject.tag);
        */

        Destroy(gameObject);
    }

    void OnParticleCollision(GameObject other) // action for particle collision
    {
        print("Particles collided with enemy " + gameObject.name);
        ProcessDestruction();
    }
}
