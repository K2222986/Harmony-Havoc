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
        timeSlider.maxValue = VolumeManager.Instance.musicSource.clip.length;
        timeSlider.value = VolumeManager.Instance.musicSource.time;
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
        VolumeManager.Instance.PlayMusic("Rush E");
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
