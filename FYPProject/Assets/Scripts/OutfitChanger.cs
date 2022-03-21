using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitChanger : MonoBehaviour
{
    [SerializeField] private Image image;

    [Header("Sprite to body")]
    public List<Sprite> options = new List<Sprite>();

    private int currentOption = 0;
    public void nextOption()
    {
        currentOption++;
        if(currentOption >= options.Count)
        {
            currentOption = 0;
        }

        image.sprite = options[currentOption];
    }

    public void previousOption()
    {
        currentOption--;
        if (currentOption <= 0)
        {
            currentOption = options.Count - 1;
        }

        image.sprite = options[currentOption];
    }
}
