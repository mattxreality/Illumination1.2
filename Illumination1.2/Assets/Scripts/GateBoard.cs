using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateBoard : MonoBehaviour
{
    private int count;
    TextMeshProUGUI gateText;
    TextMeshProUGUI speedText; // declare object
    [SerializeField] ParticleSystem fxScoreRays; //score FX
    [SerializeField] ParticleSystem fxScoreCircle; //score FX

    /* find object with particle system
     * in the parent heirarchy.
     * Play particle system each time an point is scored. Refine duration, speed, polish. 
     */
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("TextSpeed"); // instantiate object
        speedText = go.GetComponent<TextMeshProUGUI>(); // define variable of object component
        GameObject go2 = GameObject.Find("TextGates");
        gateText = go2.GetComponent<TextMeshProUGUI>();

    }

    public void GateHit(int accumulatedGateCount) // accessible outside of this class
    {
        count = accumulatedGateCount;
        gateText.text = count.ToString();
        if (count != 0) // FX when gate passed
        {
            fxScoreRays.Play();
            fxScoreCircle.Play();
        }
    }
    public void SpeedUpdate(float speed)
    {
        speedText.text = speed.ToString();
    }
}
