using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Enumeration;

public class SaveEditor : MonoBehaviour
{
    public static SaveEditor Instance;
    List<Vector2> noteList = new List<Vector2>();
    string fileName = "testFile";
    [SerializeField]
    private GameObject finalObject;

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
    private void Start()
    {
        LoadEditor();
    }
    public void AddToList(GameObject note)
    {
        noteList.Add(note.transform.position);
        AutoSave();
    }

    public void AutoSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/" + fileName +".dat"))
        {
            file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/" + fileName + ".dat");
        }
        EditorSave data = new EditorSave();
        foreach (Vector2 notePos in noteList)
        {
            data.NotePositions.Add(notePos);
        }
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Editor Saved");
    }

    public void LoadEditor()
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open);
            EditorSave data = (EditorSave)bf.Deserialize(file);
            //data access goes here
            for (int i = 0; i < data.NotePositions.Count; i++)
            {
                noteList.Add(data.NotePositions[i]);
                Instantiate(finalObject, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
            }
            file.Close();
            Debug.Log("Editor Loaded");
        }

    }
}

[Serializable]

public class EditorSave
{
    public List<Vector2> NotePositions;

    public EditorSave()
    {
        NotePositions = new List<Vector2>();
    }
}
