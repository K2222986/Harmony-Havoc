using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileFinder : MonoBehaviour
{
    public static FileFinder Instance;
    string midiFolder = "Assets/MIDI";
    string songFolder = "Assets/Resources/Songs";
    public List<string> m_DropOptions = new List<string> { };
    public List<string> m_DropOptions2 = new List<string> { };
    public TMP_Dropdown m_Dropdown;
    public TMP_Dropdown m_Dropdown2;

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
        m_Dropdown.ClearOptions();
        m_Dropdown2.ClearOptions();
        CheckFolder();
    }
    public void CheckFolder()
    {
        if (Directory.Exists(midiFolder))
        {
            DirectoryInfo directory = new DirectoryInfo(midiFolder);
            m_DropOptions.Clear();
            foreach (var file in directory.GetFiles("*.mid"))
            {
                m_DropOptions.Add(file.Name);
            }
            foreach (var file in directory.GetFiles("*.midi"))
            {
                m_DropOptions.Add(file.Name);
            }
            m_Dropdown.AddOptions(m_DropOptions);
        }
        else
        {
            Directory.CreateDirectory(midiFolder);
            Debug.Log("Created folder");
        }
        if (Directory.Exists(songFolder))
        {
            DirectoryInfo directory = new DirectoryInfo(songFolder);
            VolumeManager.Instance.musicSounds.Clear();
            foreach (var file in directory.GetFiles("*.mp3"))
            {
                m_DropOptions2.Add(file.Name.Replace(".mp3",""));
                Sound newSong = new Sound();
                newSong.name = file.Name.Replace(".mp3", "");
                newSong.clip = Resources.Load<AudioClip>("Songs/" + file.Name.Replace(".mp3", ""));
                VolumeManager.Instance.musicSounds.Add(newSong);
            }
            m_Dropdown2.AddOptions(m_DropOptions2);
        }
        else
        {
            Directory.CreateDirectory(songFolder);
        }
    }
    public void UpdateMidiFile()
    {
        MidiScript.Instance.fileName = "Assets/MIDI/" + m_DropOptions[m_Dropdown.value];
    }

    public void UpdateMp3File()
    {
    }
}
