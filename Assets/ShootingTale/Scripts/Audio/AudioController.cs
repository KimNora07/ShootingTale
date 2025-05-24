//System

using TMPro;
using UnityEngine;
using UnityEngine.Audio;
//UnityEngine
using Scene.Menu;

public class AudioController : MonoBehaviour
{
    public AudioMixer audioMixer;
    //private const string BgmParameter = "BGMVolume";
    //private const string SfxParameter = "SFXVolume";

    public TMP_Text masterText;
    //public TMP_Text bgmText;
    //public TMP_Text sfxText;

    public float volumeStep = 0.05f;

    public float currentMasterVolume;
    //public float currentBgmVolume = 0.0f;
    //public float currentSFXVolume = 0.0f;

    public MenuModel model;

    private void Awake()
    {
        model = new MenuModel();
        if (masterText) ResetVolumeText();
    }

    private void Update()
    {
        if (!masterText) return;
        if (model.CurrentMenuType.ToString() != model.MenuItems[MenuType.Setting][1] || !masterText) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            UpdateVolumeText(-volumeStep);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) UpdateVolumeText(volumeStep);
    }

    private void UpdateVolumeText(float value)
    {
        float newVolume = Mathf.Clamp01(currentMasterVolume + value);
        currentMasterVolume = newVolume;

        switch (currentMasterVolume)
        {
            case >= 1.001f:
                currentMasterVolume = 1;
                break;
            case <= 0:
                currentMasterVolume = 0.001f;
                break;
        }

        if (masterText) masterText.text = "Volume: " + Mathf.Round(currentMasterVolume * 100).ToString("F0") + "%";

        if (Settings.Profile) Settings.Profile.SetAudioLevels(ConstantsManager.MasterParameter, currentMasterVolume);
    }

    public void ResetVolumeText()
    {
        if (!Settings.Profile) return;

        float volume = Settings.Profile.GetAudioLevels(ConstantsManager.MasterParameter);
        currentMasterVolume = volume;
        UpdateVolumeText(0);
    }
}