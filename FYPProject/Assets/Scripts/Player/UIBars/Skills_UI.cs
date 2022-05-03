using System;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;

public class Skills_UI : MonoBehaviour
{
    [SerializeField] private Skills_UI skills_UI;
    [SerializeField] private PlayerStat playerStat;

    [Header("GameObjects")]
    public GameObject survivalSkillPage;
    //add combatskillpage when done
    public GameObject skillPoint_value;
    public GameObject healthRegen_value;
    public GameObject staminaRegen_value;
    public GameObject manaRegen_value;
    public GameObject hungerResist_value;
    public GameObject biomeResist_value;
    public GameObject swapSkillText;


    [Header("Buttons")]
    public Button swapSkillBtn;
    public Button healthRegen_add;
    public Button staminaRegen_add;
    public Button manaRegen_add;
    public Button hungerResist_add;
    public Button biomeResist_add;


    // //survival skill points 
    // [Header("For values")]
    // private int skillPointValue;
    // private int healthRegenValue;
    // private int staminaRegenValue;
    // private int manaRegenValue;
    // private int hungerResistValue;
    // private int biomeResistValue;

    //combat skill points
    //mage

    //warrior

    //archer

    //thief

    void Start()
    {
        swapSkillBtn.onClick.AddListener(swapBetweenCombatAndSurvival);
        healthRegen_add.onClick.AddListener(healthRegenAdd);
        staminaRegen_add.onClick.AddListener(staminaRegenAdd);
        manaRegen_add.onClick.AddListener(manaRegenAdd);
        hungerResist_add.onClick.AddListener(hungerResistAdd);
        biomeResist_add.onClick.AddListener(biomeResistAdd);


    }


    //to print value on to the UI
    public void getSkillPoint(string skillPoint, string healthRegen, string staminaRegen, string manaRegen, string hungerResist, string biomeResist)
    {
        skillPoint_value.GetComponent<TMPro.TextMeshProUGUI>().text = skillPoint;

        //survival skills
        healthRegen_value.GetComponent<TMPro.TextMeshProUGUI>().text = healthRegen;
        staminaRegen_value.GetComponent<TMPro.TextMeshProUGUI>().text = staminaRegen;
        manaRegen_value.GetComponent<TMPro.TextMeshProUGUI>().text = manaRegen;
        hungerResist_value.GetComponent<TMPro.TextMeshProUGUI>().text = hungerResist;
        biomeResist_value.GetComponent<TMPro.TextMeshProUGUI>().text = biomeResist;

        //class combat skill
    }

    void swapBetweenCombatAndSurvival()
    {
        //Debug.Log(survivalSkillPage.gameObject.activeSelf);
        if (survivalSkillPage.gameObject.activeSelf == true)
        {

            swapSkillText.GetComponent<Text>().text = "Survival Skills";
            survivalSkillPage.gameObject.SetActive(false);
            //check playerclass first and get specific class skill page
            //MageCombatSkillPage.gameObject.SetActive(true);
        }

        //go back to surivival skill page
        else if (survivalSkillPage.gameObject.activeSelf == false)
        {
            swapSkillText.GetComponent<Text>().text = "Combat Skills";
            survivalSkillPage.gameObject.SetActive(true);
        }


    }

    //survival skills
    void healthRegenAdd()
    {
        playerStat.addHealthRegenSkill();
    }

    void staminaRegenAdd()
    {

    }

    void manaRegenAdd()
    {

    }

    void hungerResistAdd()
    {

    }

    void biomeResistAdd()
    {

    }






}
