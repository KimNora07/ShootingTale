//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Volumes
{
    public string name;
    public float volume;
    public float tempVolume;
}

public class Settings
{
    public static Profiles profile;
}

[CreateAssetMenu(menuName = "Data/Create Profile")]
public class Profiles : ScriptableObject
{
    public bool saveInPlayerPrefs = true;
    public string prefPrefix = "Settings_";

    public AudioMixer audioMixer;
    public Volumes[] volumeControl;

    public void SetProfile(Profiles profile)
    {
        Settings.profile = profile;
    }

    public float GetAudioLevels(string name)
    {
        float volume = 0.5f;

        if (!audioMixer)
        {
            Debug.LogWarning("프로파일 파일안에 오디오믹서를 찾지 못했습니다!");
            return volume;
        }

        for(int i = 0; i < volumeControl.Length; i++)
        {
            if (volumeControl[i].name != name)
            {
                continue;
            }
            else
            {
                if(saveInPlayerPrefs)
                {
                    if(PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                    {
                        volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                    }
                }

                volumeControl[i].tempVolume = volumeControl[i].volume;

                if (audioMixer)
                    audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20);

                volume = volumeControl[i].volume;
                break;
            }
        }
        return volume;
    }

    public void GetAudioLevels()
    {
        if (!audioMixer)
        {
            Debug.LogWarning("프로파일 파일안에 오디오믹서를 찾지 못했습니다!");
            return;
        }

        for (int i = 0; i < volumeControl.Length; i++)
        {
            if (saveInPlayerPrefs)
            {
                if (PlayerPrefs.HasKey(prefPrefix + volumeControl[i].name))
                {
                    volumeControl[i].volume = PlayerPrefs.GetFloat(prefPrefix + volumeControl[i].name);
                }
            }

            volumeControl[i].tempVolume = volumeControl[i].volume;

            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volumeControl[i].volume) * 20);
        }
    }

    public void SetAudioLevels(string name, float volume)
    {
        if (!audioMixer)
        {
            Debug.LogWarning("프로파일 파일안에 오디오믹서를 찾지 못했습니다!");
            return;
        }

        for(int i = 0; i < volumeControl.Length; i++)
        {
            if (volumeControl[i].name != name)
            {
                continue;
            }
            else
            {
                audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20);
                volumeControl[i].tempVolume = volume;
                break;
            }
        }
    }

    public void SaveAudioLevels()
    {
        if (!audioMixer)
        {
            Debug.LogWarning("프로파일 파일안에 오디오믹서를 찾지 못했습니다!");
            return;
        }

        float volume;

        for(int i = 0; i < volumeControl.Length; i++)
        {
            volume = volumeControl[i].tempVolume;
            if(saveInPlayerPrefs)
            {
                PlayerPrefs.SetFloat(prefPrefix + volumeControl[i].name, volume);
            }
            audioMixer.SetFloat(volumeControl[i].name, Mathf.Log(volume) * 20);
            volumeControl[i].volume = volume;
        }
    }
}
