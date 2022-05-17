using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Start()
    {
        
        try
        {
            if(NewGame.modeSelection == "timer")
            {
                gameObject.SetActive(false);
            }
        }
        catch
        {

        }
        
    }
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnSecondChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnSecondChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {

        timeText.text = "Time : " + $"{TimeManager.Hour:00}:{TimeManager.Minute:00}:{TimeManager.Second:00}";
    }
}
