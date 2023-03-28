using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager manager;

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private FileHandler dataHandler;

    private GameData gameData;

    private List<IDataPersistance> DataPersistanceObjects;
    
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else 
        {
            Debug.Log("Found more than one Data Persistance Manager in the scene");
        }

        this.dataHandler = new FileHandler(Application.persistentDataPath, fileName, useEncryption);
        this.DataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    //private void Start()
    //{
        
    //}

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No Data was found! Initializing data to defaults.");
            NewGame();
        }

        // Push 
        foreach(IDataPersistance DataPersistanceObj in DataPersistanceObjects)
        {
            DataPersistanceObj.LoadData(gameData);
        }

        //Debug.Log("Loaded score = " + gameData.score);
    }

    public void SaveGame()
    {
        foreach(IDataPersistance DataPersistanceObj in DataPersistanceObjects)
        {
            DataPersistanceObj.SaveData(ref gameData);
        }
        
        dataHandler.Save(gameData);

        //Debug.Log("Saved score = " + gameData.score);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    { 
        
        IEnumerable<IDataPersistance> DataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(DataPersistanceObjects);
    }

}
