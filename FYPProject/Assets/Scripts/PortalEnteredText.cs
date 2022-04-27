using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PortalEnteredText : MonoBehaviour, IDataPersistence
{
    private int portalCount = 0;
    private TextMeshProUGUI portalEnterText;

    public void OnPortalEnter()
    {
        portalCount ++;
        Debug.Log(portalCount);
        DataPersistenceManager.instance.SaveGame();
        //SceneManager.LoadScene("Map");

    }
    public void LoadData(GameData data)
    {
        portalCount = data.portalEntered;
    }

    public void SaveData(ref GameData data)
    {
        data.portalEntered = this.portalCount;
    }

    private void Awake()
    {

        portalEnterText = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        portalEnterText.text = portalCount.ToString();

    }


}
