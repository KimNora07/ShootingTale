//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;

    private readonly string masterParameter = "MasterVolume";
    //private readonly string bgmParameter = "BGMVolume";
    //private readonly string sfxParameter = "SFXVolume";

    public TMP_Text masterText;
    //public TMP_Text bgmText;
    //public TMP_Text sfxText;

    public float volumeStep = 0.05f; // 최소 0, 최대 1, 0.05f = 5%

    public float currentMasterVolume = 0.0f;
    //public float currentBgmVolume = 0.0f;
    //public float currentSFXVolume = 0.0f;

    public MenuMain menuMain;

    private void Start()
    {
        ResetVolumeText();
    }

    void Update()
    {
        if (menuMain.menuType == MenuType.Audio)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UpdateVolumeText(-volumeStep);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpdateVolumeText(volumeStep);
            }
        }
    }

    private void UpdateVolumeText(float value)
    {
        //audioMixer.GetFloat(volumeParameter, out currentVolume);
        float newVolume = Mathf.Clamp01(currentMasterVolume + value);
        currentMasterVolume = newVolume;

        if (currentMasterVolume >= 1.001f)
        {
            currentMasterVolume = 1;
        }
        else if (currentMasterVolume <= 0)
        {
            currentMasterVolume = 0.001f;
        }

        if (masterText != null)
        {
            masterText.text = "마스터볼륨: " + Mathf.Round(currentMasterVolume * 100).ToString("F0") + "%";
        }

        if (Settings.profile)
        {
            Settings.profile.SetAudioLevels(masterParameter, currentMasterVolume);
        }
    }

    public void ResetVolumeText()
    {
        if(Settings.profile)
        {
            float volume = Settings.profile.GetAudioLevels(masterParameter);
            currentMasterVolume = volume;
            UpdateVolumeText(0);
        }
    }
}
