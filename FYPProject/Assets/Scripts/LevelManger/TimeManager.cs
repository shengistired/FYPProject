using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;

    public static int Minute {get; private set; }
    public static int Hour {get; private set; }

    public float minuteToRealTime = 0.5f;
    private float timer;


    void start ()
    {
        Minute = 0;
        Hour = 0;
        timer = minuteToRealTime;
    }

    void Update ()
    {
        timer -= Time.deltaTime * 5;

        if (timer <=0)
        {
            Minute++;
            OnMinuteChanged?.Invoke();

            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }
}                                                      
