using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PortalEnteredText : MonoBehaviour, IDataPersistence
{
    public int portalCount = 0;
    private TextMeshProUGUI portalEnterText;

    [HideInInspector]
    public static bool newPortal;
    public bool worldRegenerated = false;
    public void OnPortalEnter()
    {
        portalCount++;
        Debug.Log(portalCount);
        worldRegenerated = true;
        DataPersistenceManager.instance.SaveGame();
        //SceneManager.LoadScene("Map");

        newPortal = true;
    }
    public void diedOnBoss()
    {
        portalCount--;
        DataPersistenceManager.instance.SaveGame();
    }
    public void LoadData(GameData data)
    {
        portalCount = data.portalEntered;
    }

    public void SaveData(ref GameData data)
    {
        data.portalEntered = this.portalCount;
        data.worldRegenerated = worldRegenerated;
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
