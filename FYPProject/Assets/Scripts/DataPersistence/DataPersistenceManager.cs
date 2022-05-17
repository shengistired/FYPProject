using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    public static string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        instance = this;
        if(fileName == null)
        {
            fileName = "0data.game";
        }
        //this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

    }


    public void NewGameCreation()
    {
        List<string> listFiles = new List<string>();
        listFiles.Add("0data.game");
        listFiles.Add("1data.game");
        listFiles.Add("2data.game");
        listFiles.Add("3data.game");
       // List<string> emptyFiles = new List<string>();
        try {
            var info = new DirectoryInfo(Application.persistentDataPath);
            var file = info.GetFiles();
            foreach (FileInfo f in file)
            {
                if (listFiles.Contains(f.Name))
                {
                    listFiles.Remove(f.Name);
                }

            }
            fileName = listFiles[0];
            this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.gameData = new GameData();
        }
        catch
        {

        }

    }
    public void NewTimerGame()
    {
        fileName = "timerMode.game";
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gameData = new GameData();
    }

    public void OverWriteData(string name)
    {
        fileName = name;
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        
        try{
            this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
            this.gameData = fileDataHandler.Load();
            /*
            if(this.gameData == null)
            {
                Debug.Log("No data found");
                return;
            }
            */
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }
        catch
        {

        }



    }

    public GameData AllData(string name)
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var file = info.GetFiles();
        GameData data = new GameData();

        FileDataHandler fileDataHandlerLoad = new FileDataHandler(Application.persistentDataPath, name);
        data = fileDataHandlerLoad.Load();


        return data;

            }
    public void SaveGame()
    {
        if(this.gameData == null)
        {
            Debug.Log("No Data was found, a new game needs to be started before data");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);
        
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded Called");
        try
        {
            Debug.Log("OnSceneLoaded Called" + scene.name);

        }
        catch
        {
            Debug.Log("load nothing");
        }
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    private void OnSceneUnloaded(Scene scene)
    {
        try
        {
            Debug.Log("OnSceneUnloaded Called" + scene.name);

        }
        catch
        {
            Debug.Log("Unload nothing");
        }
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
        if(NewGame.modeSelection == "timer")
        {         
            File.Delete(Application.persistentDataPath + "/timerMode.game");
        }
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    public bool hasGameData()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var file = info.GetFiles();
        bool hasData = false;
        foreach (FileInfo f in file)
        {
            if (f.Name == "0data.game")
            {
                hasData = true;


            }
            if (f.Name == "1data.game")
            {
                hasData = true;

            }
            if (f.Name == "2data.game")
            {
                hasData = true;

            }
            if (f.Name == "3data.game")
            {
                hasData = true;

            }
        }
        //return gameData != null;
        return hasData;
    }

    public bool fullSaveData()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var file = info.GetFiles();
        List<bool> hasData = new List<bool>();
        bool fullSaveData = false;
        foreach (FileInfo f in file)
        {
            if (f.Name == "0data.game")
            {
                hasData.Add(true);

            }
            if (f.Name == "1data.game")
            {
                hasData.Add(true);

            }
            if (f.Name == "2data.game")
            {
                hasData.Add(true);

            }
            if (f.Name == "3data.game")
            {
                hasData.Add(true);

            }
        }
        if(hasData.Count == 4)
        {
            fullSaveData = true;
        }
        //return gameData != null;
        return fullSaveData;
    }
}
