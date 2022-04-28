using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvlText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lvlText;
    [SerializeField] private PlayerStat stats;
    [SerializeField] private PlayerController player;
    void Update()
    {
        
        transform.position = player.transform.position;
        lvlText.text = stats.Playerlevel.ToString();
    }
}
