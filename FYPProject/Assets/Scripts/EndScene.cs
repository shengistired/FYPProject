
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class EndScene : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Button mainMenuBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private TextMeshProUGUI scoreValue;

    private int score;


    void Start()
    {
        Debug.Log("what is my score " + score);
        mainMenuBtn.onClick.AddListener(mainMenu);
        quitBtn.onClick.AddListener(quitGame);
        scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }

    private void mainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void quitGame()
    {
        Application.Quit();
    }





    public void LoadData(GameData data)
    {
        score = data.score;
        Debug.Log("what is my score " + score);
    }

    public void SaveData(ref GameData data)
    {
        score = data.score;
    }

}
