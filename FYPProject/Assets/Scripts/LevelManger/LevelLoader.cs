using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class LevelLoader : MonoBehaviour
{
    
    public static LevelLoader Instance { get; private set; }
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private TextMeshProUGUI loadingPercent;
    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float loadingTime = Mathf.Clamp01(operation.progress / 0.9f);

            loadingPercent.text = loadingTime * 100f + "%";
            loadingBar.value = loadingTime;

            yield return null;
        } 
    }

   
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /*
    public async void LoadScene(string sceneName)
    {

        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(100);
            loadingTime = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        loadingScreen.SetActive(false);

    }
    private void Update()
    {
        loadingBar.value = loadingTime;
        text.text = loadingTime.ToString();

    }
    */
}
