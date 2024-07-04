//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private Profiles m_profiles;

    [SerializeField]
    private List<AudioController> m_VolumeText = new List<AudioController>();

    [Header("Audio Source")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip buttonClick;
    public AudioClip buttonMove;

    private void Awake()
    {
        instance = this;

        if (m_profiles != null)
            m_profiles.SetProfile(m_profiles);
    }

    private void Start()
    {
        if(Settings.profile && Settings.profile.audioMixer != null)
            Settings.profile.GetAudioLevels();
    }


    public void ApplyChanges()
    {
        if (Settings.profile && Settings.profile.audioMixer != null)
            Settings.profile.SaveAudioLevels();
    }

    public void CancelChanges()
    {
        if(Settings.profile)
            Settings.profile.GetAudioLevels();

        for(int i = 0; i < m_VolumeText.Count; i++)
        {
            m_VolumeText[i].ResetVolumeText();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
