using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedBoard : MonoBehaviour
{
    TextMeshProUGUI textSpeed; // declare object
    public Slider coolDownSlider;

    void Start()
    {
        GameObject go = GameObject.Find("TextSpeed"); // instantiate object
        textSpeed = go.GetComponent<TextMeshProUGUI>(); // define variable of object component

        coolDownSlider.value = 0f;
    }
    
    public void SpeedUpdate(float speed)
    {
        print("textSpeed = " + textSpeed.text);
        textSpeed.text = speed.ToString();
    }

    public void CoolDownSliderUpdate(float n)
    {
        coolDownSlider.value = n;
    }
}
