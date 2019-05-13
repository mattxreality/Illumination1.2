using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;
    //public GameObject playerExplosion;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;

    ScoreBoard scoreBoard; // Declare class ScoreBoard variable

    void Start()
    {
        AddNonTriggerSphereCollider(); // use often when adding components to asset pack gameObjects
        scoreBoard = FindObjectOfType<ScoreBoard>(); // searches for the instance of ScoreBoard throughout my entire scene. Instantiate.
    }

    private void AddNonTriggerSphereCollider()
    {
        Collider sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = false;
    }

    void OnTriggerEnter(Collider other) // action for gameObject collision
    {
        if (other.tag == "projectile"){ProcessDestruction();}
    }

    void OnParticleCollision(GameObject other) // action for particle collision
    {
        print("Particles collided with enemy " + gameObject.name);
        ProcessDestruction();
    }

    private void ProcessDestruction()
    {
        if (explosion != null)
        {
            GameObject fx = Instantiate(explosion, transform.position, transform.rotation); // or for no rotation use 'Quaternion.identity'
            fx.transform.parent = parent; // set parent for all explosions to 'Spawn At Runtime'
        }
        scoreBoard.ScoreHit(scorePerHit);
        Destroy(gameObject);
    }
}
