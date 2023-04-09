using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")] 
    [SerializeField] private bool disbleDataPersistence = false;
    [SerializeField] private bool initializeDayaIfNull = false;
    [SerializeField] private bool overrideSelectedProfile = false;
    [SerializeField] private string testSelectedProfileId = "test";
    [Header("File Storage Config")] 
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    [Header("Auto Saving Configuration")] 
    [SerializeField] private float autoSaveTimeSeconds = 300f;
    
    private GameData _gameData;
    private List<IDataPersistence> dataPersistencesObjects;

    private FileDataHandler _dataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
            DontDestroyOnLoad(this.gameObject);

        if (disbleDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }
        
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,useEncryption);
        
        InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();

        if (autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }

    public void DeleteProfileData(string profileId)
    {
        _dataHandler.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
        this.selectedProfileId = _dataHandler.GetMostRecentlyUpdatedProfileId();
        if (overrideSelectedProfile)
        {
            this.selectedProfileId = testSelectedProfileId;
            Debug.LogWarning("Override slected profile id with test id: " + testSelectedProfileId);
        }
    }
    public void NewGame()
    {
        this._gameData = new GameData();
        Debug.Log("New all data");
    }

    public void LoadGame()
    {
        if (disbleDataPersistence)
        {
            return;
        }
        this._gameData = _dataHandler.Load(selectedProfileId);

        if (this._gameData == null && initializeDayaIfNull)
        {
            NewGame();
        }
        
        if (this._gameData == null)
        {
            Debug.Log("No data was found Initializing data to defaults.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
        
        Debug.Log("Load all data");
    }

    public void SaveGame()
    {
        if (disbleDataPersistence)
        {
            return;
        }
        
        if (this._gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(_gameData);
        }
        
        Debug.Log("Save all data");
        _gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        _dataHandler.Save(_gameData,selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }

    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _dataHandler.LoadAllProfiles();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("Auto Saved Game");
        }
    }
}
