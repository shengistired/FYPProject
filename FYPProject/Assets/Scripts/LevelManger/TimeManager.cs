using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour, IDataPersistence
{
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    public static Action OnSecondChanged;

    public static int Minute {get; private set; }
    public static int Hour {get; private set; }
    public static int Second {get; private set; }
    public static int AllSecond {get; private set; }

    public float minuteToRealTime = 0.5f;
    private float timer;


    void Update ()
    {
        timer -= Time.deltaTime * 5;

        if (timer <=0)
        {
            Second++;
            AllSecond++;
            OnSecondChanged?.Invoke();

            if (Second >= 60)
            {
                Minute++;
                Second = 0;
                OnMinuteChanged?.Invoke();
            }
            if(Minute >= 60)
            {
                Hour++;
                Minute = 0;
                OnHourChanged?.Invoke();
            }

            timer = minuteToRealTime;
        }
    }

    public void LoadData(GameData data)
    {
        Hour = data.hour;
        Minute = data.min;
        Second = data.second;
        timer = data.timer;
        AllSecond = data.allSecond;
    }

    public void SaveData(ref GameData data)
    {
        data.hour = Hour;
        data.min = Minute;
        data.allSecond = AllSecond;
        data.second = Second;
        data.timer = timer;
    }
}                                                      
