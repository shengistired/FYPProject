using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class TimerModeManager : MonoBehaviour
{
    private float Second = 60;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject timeObject;
    [SerializeField] private GameObject SurvivalPanel;
    [SerializeField] private GameObject portalEntered;
    [SerializeField] private Animator timeLeft;
    [SerializeField] private audio_manager audio_Manager;

    private int enemyLevel = 0;
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
                    enemyLevel = 2;

                }

                if (NewGame.difficultySelection == "normal") 
                {
                    enemyLevel = 3;

                }

                if (NewGame.difficultySelection == "hard")
                {
                    enemyLevel = 4;

                }
                EnemyStat.lvl = enemyLevel;

            }
        }
        catch
        {

        }
    }
    private void Update()
    {
        try
        {
            if(NewGame.modeSelection == "timer")
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
                if (Second < 0.1)
                {
                    SurvivalPanel.SetActive(true);
                    audio_Manager.clockTicking_stop();
                    Time.timeScale = 0;

                    if (Second == -1)
                    {
                        SurvivalPanel.SetActive(false);
                        audio_Manager.clockTicking_stop();
                        timeText.text = "";


                    }
                }

                if (isTimer)
                {
                    LevelUp();
                }
            }

        }
        catch
        {

        }



    }
    private void LevelUp()
    {
        if ((int)Second == 50)
        {
            EnemyStat.lvl = enemyLevel + 2;
        }        
        if ((int)Second == 40)
        {
            EnemyStat.lvl = enemyLevel + 4;
        }
        if ((int)Second == 30)
        {
            EnemyStat.lvl = enemyLevel + 6;
        }
        if ((int)Second == 20)
        {
            EnemyStat.lvl = enemyLevel + 8;
        }
        if ((int)Second == 10)
        {
            EnemyStat.lvl = enemyLevel + 10;
        }
        Debug.Log(EnemyStat.lvl);

    }

    public void MainMenu()
    {
        Second = -1;
        LevelLoader.Instance.LoadLevel("MainMenu");
        NewGame.modeSelection = null;
    }
}
