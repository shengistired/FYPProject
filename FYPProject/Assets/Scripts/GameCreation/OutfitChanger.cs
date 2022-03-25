using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutfitChanger : MonoBehaviour
{
    [SerializeField] private Image image;

    [Header("Sprite to body")]
    public List<Sprite> options = new List<Sprite>();
    private List<string> optionText = new List<string>();
    [SerializeField] private TMP_Text classText;

    private int currentOption = 0;
    private void Start()
    {
        optionText.Add("Mage uses magic to shoot fireball at the enemy");
        optionText.Add("Warrior uses sword to cut the defeat enemy");
        optionText.Add("Thief uses dagger to slice the enemy");
        optionText.Add("Archer uses bow and arrow to shoot the enemy");

        classText.text = "Mage uses magic to shoot fireball at the enemy";
    }
    public void nextOption()
    {
        currentOption++;
        if(currentOption >= options.Count)
        {
            currentOption = 0;
        }

        image.sprite = options[currentOption];
        classText.text = optionText[currentOption];
    }

    public void previousOption()
    {
        currentOption--;
        if (currentOption <= 0)
        {
            currentOption = options.Count - 1;
        }

        image.sprite = options[currentOption];
        classText.text = optionText[currentOption];
    }
}
