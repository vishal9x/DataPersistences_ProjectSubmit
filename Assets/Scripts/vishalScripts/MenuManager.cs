using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public string PlayerName;
    public int HighestScore;

    public string currentPlayerName;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGameData();
    }


    [Serializable]
    public struct SaveData
    {
        public string PlayerName;
        public int HighestScore;
    }

    public void SaveGameData()
    {
        SaveData data = new SaveData();
        data.PlayerName = PlayerName;
        data.HighestScore = HighestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            PlayerName = data.PlayerName;
            HighestScore = data.HighestScore;
        }

    }

}
