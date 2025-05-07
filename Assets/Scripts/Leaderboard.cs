using System.Collections.Generic;
using System;
using UnityEngine;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardUI;
    public GameObject playScreen;
    public TextMeshProUGUI leaderboardText;
    string fileName;
    List<int> leaderboardScore = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    // Update is called once per frame
    void Update()
    {
        if (!VolumeManager.Instance.musicSource.isPlaying && VolumeManager.Instance.musicSource.time == 0)
        {
            leaderboardUI.SetActive(true);
            LoadLeaderboard();
        }
    }
    public void SaveToLeaderboard()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;

        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            file = new FileStream(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/" + fileName + ".dat");
        }
        LeaderboardSave data = new LeaderboardSave();
        foreach (int num in leaderboardScore)
        {
            data.Score.Add(num);
        }
        bf.Serialize(file, data);
        file.Close();
        foreach (var num in leaderboardScore)
        {
            TextMeshProUGUI scoreText = Instantiate(leaderboardText, new Vector3(0, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("LeaderboardScoreContainer").transform);
            scoreText.text = num.ToString();
        }
        playScreen.SetActive(false);
    }
    public void LoadLeaderboard()
    {
        fileName = FileFinder.Instance.m_DropOptions[FileFinder.Instance.m_Dropdown.value];
        if (File.Exists(Application.persistentDataPath + "/" + fileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName + ".dat", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            LeaderboardSave data = (LeaderboardSave)bf.Deserialize(file);
            //data access goes here
            for (int i = 0; i < data.Score.Count; i++)
            {
                leaderboardScore[i] = (data.Score[i]);
            }
            file.Close();
            for (int i = 0; i < 10; i++)
            {
                if (leaderboardScore[i] < ScoreScript.instance.score)
                {
                    leaderboardScore[i] = ScoreScript.instance.score;
                    for (int j = i; j < 9; j++)
                    {
                        leaderboardScore[j + 1] = data.Score[j];
                    }
                    SaveToLeaderboard();
                    break;
                }
            }
        }
        else
        {
            leaderboardScore[0] = ScoreScript.instance.score;
            SaveToLeaderboard();
        }
    }
}

[Serializable]

public class LeaderboardSave
{
    public List<int> Score;

    public LeaderboardSave()
    {
        Score = new List<int>();
    }
}
