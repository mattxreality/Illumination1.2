using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAt : MonoBehaviour
{

    public Camera my_camera;

    private void Update()
    {
        transform.LookAt(transform.position * 2 - my_camera.transform.position);
    }
}
