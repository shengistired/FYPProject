using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private ColorBlock origin;
    // Start is called before the first frame update
    void Awake()
    {
        origin = small.GetComponent<Button>().colors;
        origin.normalColor = colorOriginal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void smallClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = colorNew;
        medium.GetComponent<Button>().colors = origin;
        large.GetComponent<Button>().colors = origin;


    }
    public void mediumClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = origin;
        medium.GetComponent<Button>().colors = colorNew;
        large.GetComponent<Button>().colors = origin;

    }
    public void largeClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        small.GetComponent<Button>().colors = origin;
        medium.GetComponent<Button>().colors = origin;
        large.GetComponent<Button>().colors = colorNew;

    }

    public void randomClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = colorNew;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = origin;


    }
    public void forestClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = colorNew;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = origin;

    }
    public void snowClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = origin;
        snow.GetComponent<Button>().colors = colorNew;

    }
    public void desertClick()
    {
        var colorNew = origin;

        colorNew.normalColor = color;

        random.GetComponent<Button>().colors = origin;
        forest.GetComponent<Button>().colors = origin;
        desert.GetComponent<Button>().colors = colorNew;
        snow.GetComponent<Button>().colors = origin;

    }


}
