using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PercentageSlider : MonoBehaviour{
    
    public Slider slider;
    Text percentageText;

    void Start()
    {
        percentageText = GetComponent<Text> ();
    }


    public void textUpdate (float value)
    {
        percentageText.text = Mathf.RoundToInt((slider.value/slider.maxValue) * 100) + "%";
    }
}
