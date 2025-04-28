using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorSongScript : MonoBehaviour
{
    public static EditorSongScript Instance;
    public GameObject Play;
    public GameObject Pause;
    public Slider timeSlider;

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

    public void Update()
    {
        try
        {
            timeSlider.maxValue = VolumeManager.Instance.musicSource.clip.length;
            timeSlider.value = VolumeManager.Instance.musicSource.time;
        }
        catch
        {
            timeSlider.value = 0;
        }
    }

    public void Press()
    {
        if (Play.activeSelf)
        {
            PlaySong();
        }
        else
        {
            PauseSong();
        }
    }
    public void PlaySong()
    {
        VolumeManager.Instance.PlayMusic(FileFinder.Instance.m_DropOptions2[FileFinder.Instance.m_Dropdown2.value]);
        Play.SetActive(false);
        Pause.SetActive(true);

    }
    public void PauseSong()
    {
        VolumeManager.Instance.musicSource.Pause();
        Play.SetActive(true);
        Pause.SetActive(false);
        foreach (Transform child in GameObject.FindGameObjectWithTag("NoteContainer").transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void ChangeTime()
    {
        VolumeManager.Instance.musicSource.time = timeSlider.value;
    }
}
