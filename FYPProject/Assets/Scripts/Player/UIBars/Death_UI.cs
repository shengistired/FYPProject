using System.Runtime.Serialization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Death_UI : MonoBehaviour
{

    [SerializeField] private PlayerStat playerStat;
    [SerializeField] public TerrainGeneration terrainGeneration;
    [SerializeField] private PortalEnteredText portalEnteredText;
    [SerializeField] private TimeManager timeManager;

    public GameObject deathPanel;
    public GameObject liveRemainingValue;
    public GameObject scoreValue;
    public GameObject respawnBtnPanel;
    public GameObject mainMenuPanel;
    public GameObject scorePanel;

    public Button respawnBtn;
    public Button mainMenuBtn;

    private int portalCount;
    private int score;
    private int totalTime;
    private int playerLife;
    private bool gameCleared;
    private string difficultyMode;
    void Start()
    {
        respawnBtn.onClick.AddListener(respawn);
        mainMenuBtn.onClick.AddListener(returnToMainMenu);

        if(difficultyMode == "")
        {
            difficultyMode = NewGame.difficultySelection;
        }


        //mainMenuPanel.gameObject.SetActive(false);
    }



    public void getPlayerLife(int life)
    {
        if (life == 0)
        {
            liveRemainingValue.GetComponent<TMPro.TextMeshProUGUI>().text = "1";
        }
        liveRemainingValue.GetComponent<TMPro.TextMeshProUGUI>().text = life.ToString();

    }

    public int calculateScore()
    {
        int calculatedScore;
        int difficultyMultiplier;
        playerLife = playerStat.life;
        Debug.Log("asdsadsad " + difficultyMode);

        if (difficultyMode == "easy")
        {
            difficultyMultiplier = 1;
        }
        else if (difficultyMode == "normal")
        {
            difficultyMultiplier = 2;
        }
        else if (difficultyMode == "hard")
        {
            difficultyMultiplier = 3;
        }
        else
        {
            difficultyMultiplier = 1;
        }

        if (playerLife == 0)
        {
            playerLife = 1;

        }

        //example game cleared in 2hours with 5 life left on hard difficuity
        //score = 100000 - ( 4320000 / 5) / 3
        //if player clear game calculate normal way , if not - 50000 score
        totalTime = TimeManager.AllSecond;

        if (gameCleared == false)
        {
            calculatedScore = 50000 - (totalTime / playerLife) / difficultyMultiplier;
            return calculatedScore;
        }

        calculatedScore = 100000 - (totalTime / playerLife) / difficultyMultiplier;
        Debug.Log(" total time " + totalTime + " playerLife " + playerLife + " difficultyMulti " + difficultyMultiplier);

        scorePanel.gameObject.SetActive(true);
        respawnBtnPanel.gameObject.SetActive(false);
        scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedScore.ToString();
        DataPersistenceManager.instance.SaveGame();
        return calculatedScore;
    }

    public int gameClearedScore()
    {
        score = calculateScore();
        scorePanel.gameObject.SetActive(true);
        respawnBtnPanel.gameObject.SetActive(false);
        gameCleared = true;
        DataPersistenceManager.instance.SaveGame();
        return score;
    }

    public void ToggleDeathPanel()
    {
        Debug.Log(" total time " + totalTime + " playerLife " + playerLife);
        deathPanel.gameObject.SetActive(true);

        if (score == 0 && playerStat.life == 0)
        {
            //score = gameClearedScore();
            score = calculateScore();
            scorePanel.gameObject.SetActive(true);
            respawnBtnPanel.gameObject.SetActive(false);
            gameCleared = false;
        }
        Time.timeScale = 0;
    }



    void respawn()
    {
        if (terrainGeneration.seed == 1006)
        {
            portalEnteredText.diedOnBoss();
        }
        deathPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Time.timeScale = 1;
        //SceneManager.LoadScene("Map", LoadSceneMode.Single);
        LevelLoader.Instance.LoadLevel("Map");
    }

    void returnToMainMenu()
    {
        deathPanel.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Time.timeScale = 1;
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        LevelLoader.Instance.LoadLevel("MainMenu");
    }

    public void LoadData(GameData data)
    {
        score = data.score;
        //totalTime = data.allSecond;
        //playerLife = data.life;
        difficultyMode = data.difficulty;
        Debug.Log("I LOAD THIS " + data.difficulty);
        gameCleared = data.gameCleared;
    }

    public void SaveData(ref GameData data)
    {
        if (score == 0)
        {
            data.score = score;
            data.gameCleared = gameCleared;
        }

        //data.difficulty = difficultyMode;

    }

}
