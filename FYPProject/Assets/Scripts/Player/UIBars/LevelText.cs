using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public Level level;
    public int currentxp;
    public Text Leveltext;

    //Start is called before the first frame update
    void Start()
    {
        currentxp = PlayerPrefs.GetInt("experience");
        level = new Level(PlayerPrefs.GetInt("Level"), OnLevelUp);
        PlayerPrefs.SetInt("experience", PlayerPrefs.GetInt("experience") + 100);
        Leveltext.text = PlayerPrefs.GetInt("Level").ToString();
    }
    public void OnLevelUp()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
    }

    //Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            level.AddExp(100);
            PlayerPrefs.SetInt("experience", PlayerPrefs.GetInt("experience") + 100);
            currentxp = PlayerPrefs.GetInt("experience");
            Leveltext.text = PlayerPrefs.GetInt("Level").ToString();
        }
    }
}