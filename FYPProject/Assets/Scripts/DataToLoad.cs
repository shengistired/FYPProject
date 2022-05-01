using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
public class DataToLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] dataObject;
    private string lvlstring;


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
                    dataObject[index].SetActive(true);
                    RectTransform hasData = dataObject[index].GetComponent<RectTransform>().Find("HasData").GetComponent<RectTransform>();
                    RectTransform others = hasData.Find("Others").GetComponent<RectTransform>();
                    others.Find("SaveTime").GetComponent<TextMeshProUGUI>().text = "Save Time: " +  fileInfo.LastWriteTime.ToString();
                    

                }
                catch
                {

                }

            }
        }
    }
    public void loadData(string datafileName)
    {
       // DataPersistenceManager.fileName = datafileName;
        LevelLoader.Instance.LoadLevel("Map");
    }



}
