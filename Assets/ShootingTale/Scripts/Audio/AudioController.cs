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

    private readonly string _masterParameter = "MasterVolume";
    //private readonly string bgmParameter = "BGMVolume";
    //private readonly string sfxParameter = "SFXVolume";

    public TMP_Text masterText;
    //public TMP_Text bgmText;
    //public TMP_Text sfxText;

    public float volumeStep = 0.05f; // �ּ� 0, �ִ� 1, 0.05f = 5%

    public float currentMasterVolume = 0.0f;
    //public float currentBgmVolume = 0.0f;
    //public float currentSFXVolume = 0.0f;

    public MenuMain menuMain;

    private void Start()
    {
        if (masterText && menuMain)
            ResetVolumeText();
    }

    void Update()
    {
        if (masterText&& menuMain)
        {
            if (menuMain.menuType == MenuType.Audio && masterText && menuMain)
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
    }

    private void UpdateVolumeText(float value)
    {
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

        if (masterText)
        {
            masterText.text = "Volume: " + Mathf.Round(currentMasterVolume * 100).ToString("F0") + "%";
        }

        if (Settings.Profile)
        {
            Settings.Profile.SetAudioLevels(_masterParameter, currentMasterVolume);
        }
    }

    public void ResetVolumeText()
    {
        if(Settings.Profile)
        {
            float volume = Settings.Profile.GetAudioLevels(_masterParameter);
            currentMasterVolume = volume;
            UpdateVolumeText(0);
        }
    }
}
