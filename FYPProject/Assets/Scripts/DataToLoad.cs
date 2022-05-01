using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DataToLoad : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject[] dataObject;
    private string lvlstring;
    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        throw new System.NotImplementedException();
    }

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




}
