using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject explosion;
    //public GameObject playerExplosion;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;
    [SerializeField] float hits = 8;
    float initialHits;
    ScoreBoard scoreBoard; // Declare class ScoreBoard variable
    public Slider healthBar;
    void Start()
    {
        // todo make health show only upon hit
        healthBar.gameObject.SetActive(false);
        initialHits = hits;
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
        if (other.tag == "projectile")
        {
            if (!healthBar.IsActive()) { healthBar.gameObject.SetActive(true);}

            hits--;
            healthBar.value = hits/initialHits;
            if (hits <= 0)
            {
                ProcessDestruction();
            }
                
        }
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
        
        Destroy(gameObject);
        scoreBoard.ScoreHit(scorePerHit);
    }
}
