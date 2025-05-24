//System

using System.Collections.Generic;
using UnityEngine;
//UnityEngine

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Profiles m_profiles;

    [SerializeField] private List<AudioController> m_VolumeText = new();

    [Header("Audio Source")] public AudioSource musicSource;

    public AudioSource sfxSource;

    [Header("Audio Clip")] public AudioClip buttonClick;

    public AudioClip buttonMove;

    [Header("BGM")] public AudioClip menuBGM;

    public AudioClip battleBGM;
    public AudioClip endingBGM;

    [Header("SFX")] public AudioClip shot;

    public AudioClip wave;

    public GameObject musicAudioSourcePrefab;
    public GameObject sfxAudioSourcePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var musicAudioSource = Instantiate(musicAudioSourcePrefab, gameObject.transform);
        var sfxAudioSource = Instantiate(sfxAudioSourcePrefab, gameObject.transform);

        musicSource = musicAudioSource.GetComponent<AudioSource>();
        sfxSource = sfxAudioSource.GetComponent<AudioSource>();

        if (m_profiles != null)
            Profiles.SetProfile(m_profiles);
    }

    private void Start()
    {
        if (Settings.Profile && Settings.Profile.audioMixer != null)
            Settings.Profile.GetAudioLevels();
    }


    public void ApplyChanges()
    {
        if (Settings.Profile && Settings.Profile.audioMixer)
            Settings.Profile.SaveAudioLevels();
    }

    public void CancelChanges()
    {
        if (Settings.Profile)
            Settings.Profile.GetAudioLevels();

        foreach (var t in m_VolumeText) t.ResetVolumeText();
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBGM(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}