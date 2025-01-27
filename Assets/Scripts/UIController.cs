using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadMusicVolume();
        }
        else
        {
            MusicVolume();
        }
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            LoadSFXVolume();
        }
        else
        {
            SFXVolume();
        }
    }
    public void MusicVolume()
    {
        VolumeManager.Instance.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
    }

    private void LoadMusicVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        MusicVolume();
    }

    public void SFXVolume()
    {
        VolumeManager.Instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("sfxVolume", _sfxSlider.value);
    }
    private void LoadSFXVolume()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SFXVolume();
    }
}
