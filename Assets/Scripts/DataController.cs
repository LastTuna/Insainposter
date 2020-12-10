using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour {

    //TG data controller
    //credits: pastor
    private string gameDataFileName = "savedata.json";
    public GameData LoadedData;
    
    void Start()
    {
        // Check existence of a previous DataController
        if (FindObjectsOfType<DataController>().Length > 1)
        {
            // Return to menu from game scene & kill ourselves
            Destroy(this);
        }
        else
        {
            // First game load, initialize
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
    }




    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LoadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            
            SaveGameData();
        }
        else
        {
            Debug.LogError("Cannot load game data! Creating new gamedata file.");
            LoadedData = new GameData();
            LoadedData.FOV = 60;
            LoadedData.MouseSensitivity = 2;
            LoadedData.difficulty = 0;
            LoadedData.levelRating = new int[5];

            SaveGameData();
        }
    }

    public void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(LoadedData);

        string filePath = Path.Combine(Application.dataPath, gameDataFileName);
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log("Saved");
    }
}

[Serializable]
public class GameData
{
    //settings
    public int FOV;
    public float MouseSensitivity;
    //dunno if it will be done but added here anyw
    public int difficulty;
    //not sure if time to implement but added here anyw
    public float musicVolume;
    public float effectsVolume;

    //store the best score you get in this int array. translate int
    //to the character value (SABCDF) in somewhere else
    public int[] levelRating;
}