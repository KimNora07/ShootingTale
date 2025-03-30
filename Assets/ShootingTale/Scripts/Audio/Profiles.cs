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

public static class Settings
{
    public static Profiles Profile;
}

[CreateAssetMenu(menuName = "Data/Create Profile")]
public class Profiles : ScriptableObject
{
    public bool saveInPlayerPrefs = true;
    public string prefPrefix = "Settings_";

    public AudioMixer audioMixer;
    public Volumes[] volumeControl;

    public static void SetProfile(Profiles profile)
    {
        Settings.Profile = profile;
    }

    public float GetAudioLevels(string name)
    {
        var volume = 0.5f;

        if (!audioMixer) return volume;

        foreach (var t in volumeControl)
        {
            if (t.name != name) continue;
            else
            {
                if(saveInPlayerPrefs)
                {
                    if(PlayerPrefs.HasKey(prefPrefix + t.name))
                    {
                        t.volume = PlayerPrefs.GetFloat(prefPrefix + t.name);
                    }
                }

                t.tempVolume = t.volume;

                if (audioMixer)
                    audioMixer.SetFloat(t.name, Mathf.Log(t.volume) * 20);

                volume = t.volume;
                break;
            }
        }
        return volume;
    }

    public void GetAudioLevels()
    {
        if (!audioMixer) return;

        foreach (var t in volumeControl)
        {
            if (saveInPlayerPrefs)
            {
                if (PlayerPrefs.HasKey(prefPrefix + t.name))
                {
                    t.volume = PlayerPrefs.GetFloat(prefPrefix + t.name);
                }
            }

            t.tempVolume = t.volume;

            audioMixer.SetFloat(t.name, Mathf.Log(t.volume) * 20);
        }
    }

    public void SetAudioLevels(string name, float volume)
    {
        if (!audioMixer) return;

        foreach (var t in volumeControl)
        {
            if (t.name != name) continue;
            else
            {
                audioMixer.SetFloat(t.name, Mathf.Log(volume) * 20);
                t.tempVolume = volume;
                break;
            }
        }
    }

    public void SaveAudioLevels()
    {
        if (!audioMixer) return;

        foreach (var t in volumeControl)
        {
            float volume = t.tempVolume;
            if(saveInPlayerPrefs)
            {
                PlayerPrefs.SetFloat(prefPrefix + t.name, volume);
            }
            audioMixer.SetFloat(t.name, Mathf.Log(volume) * 20);
            t.volume = volume;
        }
    }
}
