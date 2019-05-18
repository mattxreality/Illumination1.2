using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateBoard : MonoBehaviour
{
    private int count;
    TextMeshProUGUI gateText;
    [SerializeField] ParticleSystem fxScoreRays; //score FX
    [SerializeField] ParticleSystem fxScoreCircle; //score FX

    /* find object with particle system
     * in the parent heirarchy.
     * Play particle system each time an point is scored. Refine duration, speed, polish. 
     */
    // Start is called before the first frame update
    void Start()
    {
        gateText = GetComponent<TextMeshProUGUI>();
        gateText.text = count.ToString();
    }

    public void GateHit(int accumulatedGateCount) // accessible outside of this class
    {
        count = count + accumulatedGateCount;
        gateText.text = count.ToString();
        fxScoreRays.Play();
        fxScoreCircle.Play();
    }
}
