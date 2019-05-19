using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedBoard : MonoBehaviour
{
    TextMeshProUGUI textSpeed; // declare object

    void Start()
    {
        GameObject go = GameObject.Find("TextSpeed"); // instantiate object
        textSpeed = go.GetComponent<TextMeshProUGUI>(); // define variable of object component
    }
    
    public void SpeedUpdate(float speed)
    {
        print("textSpeed = " + textSpeed.text);
        textSpeed.text = speed.ToString();
    }
}
