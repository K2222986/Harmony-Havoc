using UnityEngine;
using UnityEngine.UI;

public class EditorSongScript : MonoBehaviour
{
    public GameObject Play;
    public GameObject Pause;
    public Slider timeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        VolumeManager.Instance.PlayMusic("Cartoon");
        Play.SetActive(false);
        Pause.SetActive(true);

    }
    public void PauseSong()
    {
        VolumeManager.Instance.musicSource.Pause();
        Play.SetActive(true);
        Pause.SetActive(false);
    }
    public void ChangeTime()
    {
        VolumeManager.Instance.musicSource.time = timeSlider.value;
    }
}
