using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Death_UI : MonoBehaviour
{

    [SerializeField] private PlayerStat playerStat;
    [SerializeField] public TerrainGeneration terrainGeneration;
    [SerializeField] private PortalEnteredText portalEnteredText;
    public GameObject deathPanel;
    public GameObject liveRemainingValue;
    public GameObject respawnBtnPanel;
    public GameObject mainMenuPanel;


    public Button respawnBtn;
    public Button mainMenuBtn;

    private int portalCount;


    void Start()
    {
        respawnBtn.onClick.AddListener(respawn);
        mainMenuBtn.onClick.AddListener(returnToMainMenu);

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

    public void ToggleDeathPanel()
    {
        deathPanel.gameObject.SetActive(true);
        if (playerStat.life == 0)
        {
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




}
