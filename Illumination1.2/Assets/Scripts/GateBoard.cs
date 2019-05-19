using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateBoard : MonoBehaviour
{
    private int count;
    TextMeshProUGUI textGates;
    [SerializeField] ParticleSystem fxScoreRays; //score FX
    [SerializeField] ParticleSystem fxScoreCircle; //score FX

    void Start()
    {
        GameObject go2 = GameObject.Find("TextGates");
        textGates = go2.GetComponent<TextMeshProUGUI>();
    }

    public void GateHit(int accumulatedGateCount) // accessible outside of this class
    {
        count = accumulatedGateCount;
        textGates.text = count.ToString();
        if (count != 0) // FX when gate passed
        {
            fxScoreRays.Play();
            fxScoreCircle.Play();
        }
    }
}
