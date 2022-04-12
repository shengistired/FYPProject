using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseUI;
    public GameObject settingUI;

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && UI_Inventory.open == false)
        {
            checkState();

        }
    }
    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void checkState()
    {
        if (GameIsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
