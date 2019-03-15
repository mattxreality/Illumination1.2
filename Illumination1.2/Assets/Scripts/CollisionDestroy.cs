using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) // detects collider contact of GameObject
    {
        Destroy(this.gameObject);
        print("Object triggered something");
        print("other game object = " + collision.gameObject.name);
    }



    //void OnTriggerEnter(Collider other)
    //{
       

    //}
}
