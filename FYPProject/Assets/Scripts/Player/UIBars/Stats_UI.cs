using System;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;

public class Stats_UI : MonoBehaviour
{
    [SerializeField] private Stats_UI stats_UI;
    [SerializeField] private PlayerStat playerStat;
    public GameObject level_value;
    public GameObject stat_value;
    public GameObject str_value;
    public GameObject dex_value;
    public GameObject int_value;
    public GameObject luck_value;

    public Button str_add;
    public Button dex_add;
    public Button int_add;
    public Button luck_add;

    void Start()
    {
        str_add.onClick.AddListener(strengthAdd);
        dex_add.onClick.AddListener(dexAdd);
        int_add.onClick.AddListener(intAdd);
        luck_add.onClick.AddListener(luckAdd);
    }

    // void Update()
    // {
    //     str_add.onClick.AddListener(playerStat.addStrength);
    // }

    public void getStats(string level, string statPoints, string str, string intelligence, string dex, string luck)
    {
        level_value.GetComponent<TMPro.TextMeshProUGUI>().text = level;
        stat_value.GetComponent<TMPro.TextMeshProUGUI>().text = statPoints;
        str_value.GetComponent<TMPro.TextMeshProUGUI>().text = str;
        dex_value.GetComponent<TMPro.TextMeshProUGUI>().text = intelligence;
        int_value.GetComponent<TMPro.TextMeshProUGUI>().text = dex;
        luck_value.GetComponent<TMPro.TextMeshProUGUI>().text = luck;

    }

    void strengthAdd()
    {
        playerStat.addStrength();
    }

    void dexAdd()
    {
        playerStat.addDex();
    }

    void intAdd()
    {
        playerStat.addIntelligence();
    }

    void luckAdd()
    {
        playerStat.addLuck();
    }







}
