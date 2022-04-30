using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class NewGame : MonoBehaviour
{
    [SerializeField] Button small;
    [SerializeField] Button medium;
    [SerializeField] Button large;

    [SerializeField] Button random;
    [SerializeField] Button forest;
    [SerializeField] Button desert;
    [SerializeField] Button snow;

    [SerializeField] Button easy;
    [SerializeField] Button normal;
    [SerializeField] Button hard;

    [SerializeField] Color color;
    [SerializeField] Color colorOriginal;
    [SerializeField] TMP_Text errorMsg;

    private ColorBlock origin;
    private bool[] notEmptySize = { false, false, false };
    private bool[] notEmptyBiome = { false, false, false, false };
    private bool[] notEmptyDifficulty = { false, false, false};
    private bool biome = false;
    private bool size = false;
    private bool difficulty = false;
    public static string worldsizeSelection;
    public static string biomeSelection;
    public static string difficultySelection;
    public static int life;
    public static string playerClass;

    // Start is called before the first frame update
    void Awake()
    {
        origin = small.GetComponent<Button>().colors;
        origin.normalColor = colorOriginal;

    }

    // Update is called once per frame

    public void smallClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = colorNew;
        medium.GetComponent<Button>().colors = origin;
        large.GetComponent<Button>().colors = origin;

        notEmptySize[0] = true;
        notEmptySize[1] = false;
        notEmptySize[2] = false;

        worldsizeSelection = "small";

    }
    public void mediumClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = origin;
        medium.GetComponent<Button>().colors = colorNew;
        large.GetComponent<Button>().colors = origin;

        notEmptySize[0] = false;
        notEmptySize[1] = true;
        notEmptySize[2] = false;

        worldsizeSelection = "medium";

    }
    public void largeClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = origin;
        medium.GetComponent<Button>().colors = origin;
        large.GetComponent<Button>().colors = colorNew;

        notEmptySize[0] = false;
        notEmptySize[1] = false;
        notEmptySize[2] = true;

        worldsizeSelection = "large";


    }

    public void randomClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = colorNew;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = origin;

        notEmptyBiome[0] = true;
        notEmptyBiome[1] = false;
        notEmptyBiome[2] = false;
        notEmptyBiome[3] = false;

        biomeSelection = "random";

    }
    public void forestClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = colorNew;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = origin;

        notEmptyBiome[0] = false;
        notEmptyBiome[1] = true;
        notEmptyBiome[2] = false;
        notEmptyBiome[3] = false;


        biomeSelection = "forest";

    }
    public void snowClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = colorNew;

        notEmptyBiome[0] = false;
        notEmptyBiome[1] = false;
        notEmptyBiome[2] = true;
        notEmptyBiome[3] = false;

        biomeSelection = "snow";


    }
    public void desertClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = colorNew;
        snow.GetComponent<Button>().colors = origin;

        notEmptyBiome[0] = false;
        notEmptyBiome[1] = false;
        notEmptyBiome[2] = false;
        notEmptyBiome[3] = true;

        biomeSelection = "desert";


    }
    public void easyClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        easy.GetComponent<Button>().colors = colorNew;
        normal.GetComponent<Button>().colors = origin;
        hard.GetComponent<Button>().colors = origin;


        notEmptyDifficulty[0] = true;
        notEmptyDifficulty[1] = false;
        notEmptyDifficulty[2] = false;
        life = 5;
        difficultySelection = "easy";


    }
    public void normalClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        easy.GetComponent<Button>().colors = origin;
        normal.GetComponent<Button>().colors = colorNew;
        hard.GetComponent<Button>().colors = origin;


        notEmptyDifficulty[0] = false;
        notEmptyDifficulty[1] = true;
        notEmptyDifficulty[2] = false;
        life = 3;

        difficultySelection = "normal";


    }
    public void hardClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        easy.GetComponent<Button>().colors = origin;
        normal.GetComponent<Button>().colors = origin;
        hard.GetComponent<Button>().colors = colorNew;

        notEmptyDifficulty[0] = false;
        notEmptyDifficulty[1] = false;
        notEmptyDifficulty[2] = true;

        life = 5;
        difficultySelection = "hard";


    }
    public void newGameClick()
    {

        foreach (bool check in notEmptySize)
        {
            if (check == true)
            {
                size = true;
            }

        }
        foreach (bool check in notEmptyBiome)
        {
            if (check == true)
            {

                biome = true;
            }

        }
        foreach (bool check in notEmptyDifficulty)
        {
            if (check == true)
            {

                difficulty = true;
            }

        }

        if (size == true && biome == true && difficulty == true)
        {
            errorMsg.gameObject.SetActive(false);
            DataPersistenceManager.instance.NewGame();
            LevelLoader.Instance.LoadLevel("Map");

            //SceneManager.LoadSceneAsync("Map");

        }
        else
        {
            errorMsg.gameObject.SetActive(true);
        }


    }


}
