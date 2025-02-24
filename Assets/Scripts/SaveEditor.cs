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
    List<float> noteListx = new List<float>();
    List<float> noteListy = new List<float>();
    List<int> noteListID = new List<int>();
    string fileName = "testFile";
    [SerializeField]
    private NoteScript finalObject;

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
        fileName = SavesScript.Instance.CurrentSave.GetName();
        LoadEditor();
    }
    public void AddToList(NoteScript note)
    {
        if (noteListID.Count > 0)
        {
            note.noteID = noteListID[noteListID.Count - 1] + 1;
            noteListID.Add(note.noteID);
        }
        else
        {
            note.noteID = 0;
            noteListID.Add(0);
        }
        noteListx.Add(note.transform.position.x);
        noteListy.Add(note.transform.position.y + VolumeManager.Instance.musicSource.time * 500);
        AutoSave();
    }
    public void RemoveFromList(GameObject note)
    {
        for (int i = 0; i < noteListID.Count; i++)
        {
            if (noteListID[i] == note.GetComponent<NoteScript>().noteID)
            {
                Debug.Log("Deleted");
                noteListID.RemoveAt(i);
                noteListy.RemoveAt(i);
                noteListx.RemoveAt(i);
                AutoSave();
                i = noteListID.Count;
            }
        }
    }

    public void AutoSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/" + fileName +".dat"))
        {
            file = new FileStream(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/" + fileName + ".dat");
        }
        EditorSave data = new EditorSave();
        foreach (float notePos in noteListx)
        {
            data.NotePositionsx.Add(notePos);
        }
        foreach (float notePos in noteListy)
        {
            data.NotePositionsy.Add(notePos);
        }
        foreach (int noteID in noteListID)
        {
            data.NoteID.Add(noteID);
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
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            EditorSave data = (EditorSave)bf.Deserialize(file);
            //data access goes here
            for (int i = 0; i < data.NotePositionsx.Count; i++)
            {
                noteListx.Add(data.NotePositionsx[i]);
                noteListy.Add(data.NotePositionsy[i]);
                noteListID.Add(data.NoteID[i]);
                NoteScript newNote = Instantiate(finalObject, new Vector3(data.NotePositionsx[i], data.NotePositionsy[i], -1f), Quaternion.identity, GameObject.FindGameObjectWithTag("NoteContainer").transform);
                newNote.noteID = data.NoteID[i];
            }
            file.Close();
            Debug.Log("Editor Loaded");
        }

    }
}

[Serializable]

public class EditorSave
{
    public List<float> NotePositionsx;
    public List<float> NotePositionsy;
    public List<int> NoteID;

    public EditorSave()
    {
        NotePositionsx = new List<float>();
        NotePositionsy = new List<float>();
        NoteID = new List<int>();
    }
}
