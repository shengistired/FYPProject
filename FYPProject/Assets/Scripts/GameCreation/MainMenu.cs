using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueText;
    private void Start()
    {
        Color originalColor = continueText.color;

        if (!DataPersistenceManager.instance.hasGameData())
        {
            continueButton.interactable = false;
            continueButton.GetComponent<EventTrigger>().enabled = false;
            originalColor.a = .6f;
            continueText.color = originalColor;
            
        }   
        
        
    }
    public void continueClick()
    {
        LevelLoader.Instance.LoadLevel("Map");
        //LevelLoader.Instance.LoadScene("Map");
        //SceneManager.LoadSceneAsync("Map");

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }
}
