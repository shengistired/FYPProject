using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Death_UI : MonoBehaviour, IDataPersistence
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
    private string gameMode;

    // public static bool respawnBool = false;
    void Start()
    {
        respawnBtn.onClick.AddListener(respawn);
        mainMenuBtn.onClick.AddListener(returnToMainMenu);

        if (difficultyMode == "")
        {
            difficultyMode = NewGame.difficultySelection;
            playerLife = NewGame.life;
        }

        if (gameMode == "")
        {
            gameMode = NewGame.modeSelection;
        }

       

        //respawnBool = false;

        //mainMenuPanel.gameObject.SetActive(false);
    }



    public void getPlayerLife(int life)
    {

        liveRemainingValue.GetComponent<TMPro.TextMeshProUGUI>().text = life.ToString();


    }

    public int calculateScore(bool gameCleared, int life)
    {
        int calculatedScore;
        int difficultyMultiplier;
        int lifeMultiplier;
        //Debug.Log("asdsadsad " + difficultyMode);

        lifeMultiplier = life;


        if (difficultyMode == "easy")
        {
            difficultyMultiplier = 1;
        }
        else if (difficultyMode == "normal")
        {
            difficultyMultiplier = 3;
        }
        else if (difficultyMode == "hard")
        {
            difficultyMultiplier = 10;
        }
        else
        {
            difficultyMultiplier = 1;
        }

        if (life == 0)
        {
            lifeMultiplier = 1;
        }

        //example game cleared in 2hours with 5 life left on hard difficuity
        //score =  ( 4320000 / 5) / 3
        //if player clear game calculate normal way , if not - 50000 score
        totalTime = TimeManager.AllSecond;
        if (totalTime <= 0)
        {
            totalTime = 1;
        }
        Debug.Log("playerLife " + life);
        Debug.Log(" total time " + totalTime + " lifeMultiplier " + lifeMultiplier + " difficultyMulti " + difficultyMultiplier);
        if (gameCleared == false && playerStat.life == 0)
        {
            //Debug.Log(" total time " + totalTime + " lifeMultiplier " + lifeMultiplier + " difficultyMulti " + difficultyMultiplier);
            calculatedScore = (totalTime / lifeMultiplier) / difficultyMultiplier;
            Debug.Log(" calculatedScore 4 is  " + calculatedScore);
            if (calculatedScore < 0)
            {
                calculatedScore = 1;
                scorePanel.gameObject.SetActive(true);
                respawnBtnPanel.gameObject.SetActive(false);
                scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedScore.ToString();
                //playerLife--;
                DataPersistenceManager.instance.SaveGame();
                //Debug.Log(" calculatedScore 1 is  " + calculatedScore);
                return calculatedScore;
            }
            else
            {
                //playerLife--;
                scorePanel.gameObject.SetActive(true);
                respawnBtnPanel.gameObject.SetActive(false);
                scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedScore.ToString();
                DataPersistenceManager.instance.SaveGame();
                //Debug.Log(" calculatedScore 1 is  " + calculatedScore);
                return calculatedScore;
            }



        }
        else if (gameCleared == true)
        {
            // if game won
            if (totalTime >= 500000)
            {
                totalTime = 450000;
            }

            calculatedScore = ((500000 - totalTime) * (difficultyMultiplier * lifeMultiplier));

            score = calculatedScore;
            Debug.Log(" total time " + totalTime + " playerLife " + life + " difficultyMulti " + difficultyMultiplier);

            //scorePanel.gameObject.SetActive(true);
            //respawnBtnPanel.gameObject.SetActive(false);
            //scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedScore.ToString();
            DataPersistenceManager.instance.SaveGame();
            //Debug.Log(" calculatedScore 2 is  " + calculatedScore);
            return calculatedScore;
        }

        else
        {
            calculatedScore = 1;
            scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = calculatedScore.ToString();
            return calculatedScore;
        }





    }



    public void ToggleDeathPanel(int life)
    {

        deathPanel.gameObject.SetActive(true);

        if (score == 0 && life == 0 && gameMode != "timer")
        {
            //score = gameClearedScore();
            Debug.Log("playerlife in toggledeathpanel" + life);
            gameCleared = false;
            score = calculateScore(false, life);
            scorePanel.gameObject.SetActive(true);
            respawnBtnPanel.gameObject.SetActive(false);

        }
        if (gameMode == "timer")
        {
            scoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
            respawnBtnPanel.gameObject.SetActive(false);
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
        //respawnBool = true;
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

    public void winGame()
    {
        score = calculateScore(true, playerStat.life);
        gameCleared = true;
        DataPersistenceManager.instance.SaveGame();
        Invoke("goWinScreen", 5);
    }

    private void goWinScreen()
    {
        LevelLoader.Instance.LoadLevel("WinScene");
    }

    public void LoadData(GameData data)
    {
        score = data.score;
        totalTime = data.allSecond;
        playerLife = data.life;
        difficultyMode = data.difficulty;
        //Debug.Log("I LOAD THIS " + data.difficulty);
        gameMode = data.mode;
        gameCleared = data.gameCleared;
    }

    public void SaveData(ref GameData data)
    {
        if (score != 0)
        {
            data.score = score;
            data.gameCleared = gameCleared;

        }

        //data.difficulty = difficultyMode;

    }

}
