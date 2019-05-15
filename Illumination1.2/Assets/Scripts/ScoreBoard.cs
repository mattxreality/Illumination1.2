using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    TextMeshProUGUI scoreText;
    [SerializeField] ParticleSystem fxScoreRays; //score FX
    [SerializeField] ParticleSystem fxScoreCircle; //score FX

    /* find object with particle system
     * in the parent heirarchy.
     * Play particle system each time an point is scored. Refine duration, speed, polish. 
     */


    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
       
    }

    public void ScoreHit(int scorePerHit)
    {
        score = score + scorePerHit;
        scoreText.text = score.ToString();
        fxScoreRays.Play();
        fxScoreCircle.Play();
    }
}
