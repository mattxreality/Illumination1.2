using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorLerp : MonoBehaviour
{
    // Access Light Component in this go.
    // Interpolate light color between two colors back and forth

    [SerializeField] float flickerDuration = 1.0f;
    [SerializeField] Color color0 = Color.red;
    [SerializeField] Color color1 = Color.yellow;

    [SerializeField] Light lightComp;

    void Start()
    {
        lightComp.GetComponent<Light>();
    }
    void Update()
    {
        SetLightColor();
    }

    private void SetLightColor()
    {
        // set light color
        float t = Mathf.PingPong(Time.time, flickerDuration) / flickerDuration;
        lightComp.color = Color.Lerp(color0, color1, t);
    }
}
