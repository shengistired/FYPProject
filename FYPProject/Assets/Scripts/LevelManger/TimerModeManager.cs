using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TimerModeManager : MonoBehaviour
{
    private float Second = 20;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject timeObject;
    [SerializeField] private GameObject SurvivalPanel;
    [SerializeField] private GameObject portalEntered;
    [SerializeField] private Animator timeLeft;
    [SerializeField] private audio_manager audio_Manager;


    private bool isTimer = false;
    private void Start()
    {
        try
        {
            if (NewGame.modeSelection == "timer")
            {
                timeObject.SetActive(true);
                portalEntered.SetActive(false);
                isTimer = true;

                if (NewGame.difficultySelection == "easy")
                {
                    EnemyStat.lvl = 2;

                }

                if (NewGame.difficultySelection == "normal") 
                {
                    EnemyStat.lvl = 3;

                }

                if (NewGame.difficultySelection == "hard")
                {
                    EnemyStat.lvl = 4;

                }

            }
        }
        catch
        {

        }
    }
    private void Update()
    {
        // Debug.Log(Second);
        if (Second < 10)
        {
            timeText.color = Color.red;
            timeLeft.SetFloat("TimeLeft", Second);
            if (!audio_Manager.clockTicking.isPlaying)
            {
                audio_Manager.clockTicking_play();
            }
        }
        if (Second > 0)
        {
            timeText.text = ((int)Second).ToString();
            Second -= Time.deltaTime;
        }
        if(Second < 0.1)
        {
            SurvivalPanel.SetActive(true);
            audio_Manager.clockTicking_stop();
            Time.timeScale = 0;
        }

        if (isTimer)
        {
            LevelUp();
        }


    }
    private void LevelUp()
    {
        if ((int)Second == 50)
        {
            EnemyStat.lvl = 3;
        }        
        if ((int)Second == 40)
        {
            EnemyStat.lvl = 4;
        }        
        if ((int)Second == 30)
        {
            EnemyStat.lvl = 5;
        }       
        if ((int)Second == 20)
        {
            EnemyStat.lvl = 6;
        }        
        if ((int)Second == 10)
        {
            EnemyStat.lvl = 7;
        }


    }

    public void MainMenu()
    {
        SurvivalPanel.SetActive(false);
        Time.timeScale = 1;

        LevelLoader.Instance.LoadLevel("MainMenu");
    }
}
