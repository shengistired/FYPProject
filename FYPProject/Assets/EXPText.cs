using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EXPText : MonoBehaviour
{
    public PlayerStat stat;
    public Slider EXPBar;

    public TextMeshProUGUI currentEXPText;
    public TextMeshProUGUI neededEXPText;

    void FixedUpdate()
    {
        currentEXPText.text = stat.currentExp.ToString();
        neededEXPText.text = stat.expNeededToNextLevel.ToString();

        EXPBar.maxValue = stat.expNeededToNextLevel;
        EXPBar.value = stat.currentExp;
    }

}
