using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderNumber : MonoBehaviour{
    
    public Slider slider;
    Text sliderText;

    void Start()
    {
        sliderText = GetComponent<Text> ();
    }


    public void textUpdate (float value)
    {
        sliderText.text = Mathf.RoundToInt(slider.value) + "/" + Mathf.RoundToInt(slider.maxValue);
    }
}