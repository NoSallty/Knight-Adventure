﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    [SerializeField] private string fileName;
    [SerializeField] private bool encryptData;

    public GameData gameData;
    [SerializeField] private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,encryptData);
        dataHandler.Delete();
    }
    private void Awake()
    {
        if(instance!=null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
        saveManagers=FindAllSaveManagers();
        StartCoroutine(LoadGameForDelay(0.15f));
    }
    IEnumerator LoadGameForDelay(float time)
    {
        yield return new WaitForSeconds(time);
        LoadGame();
    }
    public void NewGame()
    {
        gameData=new GameData();
    }
    public void LoadGame()
    {
        gameData = dataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No saved data found!");
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }

        List<KeyValuePair<string, bool>> enemiesToUpdate = gameData.enemiesStatus.ToList();

        foreach (var enemyStatus in enemiesToUpdate)
        {
            GameObject enemy = GameObject.Find(enemyStatus.Key);
            if (enemy != null)
            {
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null && enemyStatus.Value)
                {
                    enemy.SetActive(false); 
                }
            }
        }
    }
    public void SaveGame()
    {

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
            
        }
        dataHandler.Save(gameData);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }
    public bool HasSavedData()
    {
        if (dataHandler.Load() != null)
        {
           return true;
        }
   
        return false;
    }
}
