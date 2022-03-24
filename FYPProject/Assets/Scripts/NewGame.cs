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
    [SerializeField] Color color;
    [SerializeField] Color colorOriginal;
    [SerializeField] TMP_Text errorMsg;

    private ColorBlock origin;
    private bool[] notEmptySize = { false, false, false };
    private bool[] notEmptyBiome = { false, false, false, false };
    private bool biome = false;
    private bool size = false;
    public static string worldsizeSelection;
    public static string biomeSelection;

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

    public void Submit()
    {

        foreach (bool check in notEmptySize)
        {
            if (check == true)
            {
                Debug.Log(notEmptySize[0]);
                size = true;
            }

        }
        foreach (bool check in notEmptyBiome)
        {
            if (check == true)
            {
                Debug.Log(notEmptyBiome[1]);

                biome = true;
            }

        }

        if (size == true && biome == true)
        {
            errorMsg.gameObject.SetActive(false);
            Debug.Log("Done");
            SceneManager.LoadScene("Map");

        }
        else
        {
            errorMsg.gameObject.SetActive(true);
        }


    }

    public string worldsizeSelected()
    {
        return worldsizeSelection;
    }

    public string biomeSelected()
    {
        return biomeSelection;
    }
}
