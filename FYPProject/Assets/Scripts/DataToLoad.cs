using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
public class DataToLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] dataObject;
    private void Start()
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

                    dataObject[index].GetComponent<Button>().interactable = true;
                    RectTransform hasData = dataObject[index].GetComponent<RectTransform>().Find("HasData").GetComponent<RectTransform>();
                    hasData.gameObject.SetActive(true);
                    Transform nodata = dataObject[index].GetComponent<RectTransform>().Find("NoData");
                    nodata.gameObject.SetActive(false);
                    hasData.Find("Image").GetComponent<RectTransform>().Find("Lv").GetComponent<TextMeshProUGUI>().text = "Lv." + DataPersistenceManager.instance.AllData(dataObject[index].name).playerlevel.ToString();
                    hasData.Find("SaveTime").GetComponent<TextMeshProUGUI>().text = "Save Time: " + fileInfo.LastWriteTime.ToString();
                    hasData.Find("Life").GetComponent<TextMeshProUGUI>().text = "Life: " + DataPersistenceManager.instance.AllData(dataObject[index].name).life;
                    hasData.Find("PortalEntered").GetComponent<TextMeshProUGUI>().text = "Portal Entered: " + DataPersistenceManager.instance.AllData(dataObject[index].name).portalEntered;
                    hasData.Find("Biome").GetComponent<TextMeshProUGUI>().text = "Biome: " + DataPersistenceManager.instance.AllData(dataObject[index].name).biome;
                    hasData.Find("Difficulty").GetComponent<TextMeshProUGUI>().text = "Difficulty: " + DataPersistenceManager.instance.AllData(dataObject[index].name).difficulty;
                    hasData.Find("Timer").GetComponent<TextMeshProUGUI>().text = "Timer: " + DataPersistenceManager.instance.AllData(dataObject[index].name).hour + " : " + DataPersistenceManager.instance.AllData(dataObject[index].name).min + " : " + DataPersistenceManager.instance.AllData(dataObject[index].name).second;
                    hasData.Find("Image").GetComponent<RectTransform>().Find("Lv").GetComponent<TextMeshProUGUI>().text = "Lv. " + DataPersistenceManager.instance.AllData(dataObject[index].name).playerlevel;

                    if (DataPersistenceManager.instance.AllData(dataObject[index].name).life == 0)
                    {
                        dataObject[index].GetComponent<Button>().interactable = false;
                    }


                }
                catch
                {

                }

            }
        }
    }
    public void loadData(string datafileName)
    {
        DataPersistenceManager.fileName = datafileName;
        LevelLoader.Instance.LoadLevel("Map");
    }

    public void deleteData(string name)
    {
        string filename = "/" + name;

        File.Delete(Application.persistentDataPath + filename);


    }



}
