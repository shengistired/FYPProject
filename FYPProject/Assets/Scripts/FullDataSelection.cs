using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class FullDataSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] dataObject;

    // Start is called before the first frame update
    void Start()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var file = info.GetFiles();
        foreach (FileInfo fileInfo in file)
        {
            string name = fileInfo.Name.Substring(1);

            if (name == "data.game")
            {
                try
                {

                    int index = int.Parse(fileInfo.Name.Substring(0, 1));
                    RectTransform hasData = dataObject[index].GetComponent<RectTransform>().Find("HasData").GetComponent<RectTransform>();
                    hasData.Find("SaveTime").GetComponent<TextMeshProUGUI>().text = "Save Time: " + fileInfo.LastWriteTime.ToString();
                    hasData.Find("PortalEntered").GetComponent<TextMeshProUGUI>().text = "Portal Entered: " + DataPersistenceManager.instance.AllData(dataObject[index].name).portalEntered;
                    hasData.Find("Biome").GetComponent<TextMeshProUGUI>().text = "Biome: " + DataPersistenceManager.instance.AllData(dataObject[index].name).biome;
                    hasData.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + DataPersistenceManager.instance.AllData(dataObject[index].name).difficulty;
                    hasData.Find("Image").GetComponent<RectTransform>().Find("Lv").GetComponent<TextMeshProUGUI>().text = "Lv. " + DataPersistenceManager.instance.AllData(dataObject[index].name).playerlevel;
                    //hasData.Find("Level").GetComponent<TextMeshProUGUI>().text = "Portal Entered: " + DataPersistenceManager.instance.AllData(dataObject[index].name).portalEntered;


                }
                catch
                {

                }

            }
        }
    }

}
