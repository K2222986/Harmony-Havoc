using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SavesScript : MonoBehaviour
{
    public SaveFileScript[] saves;
    public SaveFileScript CurrentSave;
    public static SavesScript Instance;
    public GameObject SaveContainer;
    public GameObject InputContainer;
    public TMP_InputField inputName;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadSaves();
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetupSave(SaveFileScript activeSave)
    {
        CurrentSave = activeSave;
        if (CurrentSave.GetName() == "Empty")
        {
            SaveContainer.SetActive(false);
            InputContainer.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
    public void CreateNewSave()
    {
        
        CurrentSave.SetName(inputName.text);
        CurrentSave.LoadName();
        SaveContainer.SetActive(true);
        InputContainer.SetActive(false);
        SaveSaves();
    }
    public void SaveSaves(string Name, int TimeElapsed)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/saveNames.dat"))
        {
            file = File.Open(Application.persistentDataPath + "/saveNames.dat", FileMode.Open);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/saveNames.dat");
        }
        SaveData data = new SaveData();
        foreach (SaveFileScript s in saves)
        {
            data.SaveFileName.Add(Name);
            data.TimeElapsed.Add(TimeElapsed);
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game Saved");
    }
    public void SaveSaves()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/saveNames.dat"))
        {
            file = File.Open(Application.persistentDataPath + "/saveNames.dat", FileMode.Open);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/saveNames.dat");
        }
        SaveData data = new SaveData();
        foreach (SaveFileScript s in saves)
        {
            data.SaveFileName.Add(s.Name);
            data.TimeElapsed.Add(s.TimeElapsed);
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game Saved");
    }
    public void LoadSaves()
    {
        if (File.Exists(Application.persistentDataPath + "/saveNames.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveNames.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            //data access goes here
            for (int i = 0; i < data.SaveFileName.Count; i++)
            {
                if (data.SaveFileName[i] != "Empty")
                {
                    saves[i].SetName(data.SaveFileName[i]);
                    saves[i].TimeElapsed = data.TimeElapsed[i];
                    saves[i].LoadName();
                }
            }
            file.Close();
            Debug.Log("Game Loaded");
        }

    }
}



[Serializable]

public class SaveData
{
    public List<string> SaveFileName;
    public List<int> TimeElapsed;

    public SaveData()
    {
        SaveFileName = new List<string>();
        TimeElapsed = new List<int>();
    }
}
