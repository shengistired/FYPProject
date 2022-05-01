using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one data persistence manager in the scene,Destroying the latest one");
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

    }


    public void NewGame()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var file = info.GetFiles();
        foreach(FileInfo f in file)
        {
            if (f.Name == "0data.game")
            {
                fileName = "1data.game";

            }
            if(f.Name == "1data.game")
            {
                fileName = "2data.game";

            }
            if (f.Name == "2data.game")
            {
                fileName = "3data.game";

            }
            if (f.Name == "3data.game")
            {
                fileName = "0data.game";

            }
        }
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gameData = new GameData(); 
    }
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        /*
        if(this.gameData == null)
        {
            Debug.Log("No data found");
            return;
        }
        */
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }


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
       // Debug.Log("OnSceneLoaded Called");
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    private void OnSceneUnloaded(Scene scene)
    {
       // Debug.Log("OnSceneLoaded Called");

        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
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
                hasData=true;

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
}
