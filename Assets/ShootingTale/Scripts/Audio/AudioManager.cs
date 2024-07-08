//System
using System.Collections;
using System.Collections.Generic;

//UnityEngine
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private Profiles m_profiles;

    [SerializeField]
    private List<AudioController> m_VolumeText = new List<AudioController>();

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip buttonClick;
    public AudioClip buttonMove;

    [Header("BGM")]
    public AudioClip menuBGM;
    public AudioClip battleBGM;
    public AudioClip endingBGM;

    [Header("SFX")]
    public AudioClip shot;
    public AudioClip wave;

    public GameObject musicAudioSourcePrefab;
    public GameObject sfxAudioSourcePrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        GameObject musicAudioSource = Instantiate(musicAudioSourcePrefab, this.gameObject.transform);
        GameObject sfxAudioSource = Instantiate(sfxAudioSourcePrefab, this.gameObject.transform);

        musicSource = musicAudioSource.GetComponent<AudioSource>();
        sfxSource = sfxAudioSource.GetComponent<AudioSource>();

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

    public void PlayBGM(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
