using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 音乐和音效管理
/// </summary>
public class AudioManager : Level<AudioManager>
{
    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private AudioMixerGroup music;
    [SerializeField]
    private AudioMixerGroup sound;
    [Header("声音上下限")]
    public float maxMusicVolume = 10;
    public float minMusicVolume = -40;
    public float maxSoundVolume = 10;
    public float minSoundVolume = -40;


    //玩家音效
    [SerializeField]
    private AudioSource playerSource;
    //背景音效
    [SerializeField]
    private AudioSource musicSource;
    //交互音效
    [SerializeField]
    private AudioSource interactSource;
    //UI音效
    [SerializeField]
    private AudioSource uiSource;

    [Header("玩家音效")]
    public AudioClip playerMoveClip;
    public AudioClip playerpushClip;

    [Header("背景音效")]
    public AudioClip gameClip;

    [Header("交互音效")]
    public AudioClip computClip;

    [Header("UI音效")]
    public AudioClip clickclip;

    //不销毁音效
    private static AudioManager instance;

    private void Awake()
    {
        //保障只有一个audioManager
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioManager");

        if (objs.Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        playerSource = gameObject.AddComponent<AudioSource>();
        playerSource.outputAudioMixerGroup = sound;
        playerSource.volume = 0.6f;

        uiSource = gameObject.AddComponent<AudioSource>();
        uiSource.outputAudioMixerGroup = sound;
        uiSource.volume = 0.7f;

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.outputAudioMixerGroup = music;
        musicSource.volume = 0.4f;

        interactSource = gameObject.AddComponent<AudioSource>();
        interactSource.outputAudioMixerGroup = music;
        interactSource.volume = 0.5f;
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < float.MinValue)//声音最小时进行改为最小-80，防止仍然有声音
        {
            volume = -80;
        }
        else
        {
            volume = volume * (maxMusicVolume - minMusicVolume) + minMusicVolume;
        }

        mixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume)
    {
        if (volume < float.MinValue)//声音最小时进行改为最小-80，防止仍然有声音
        {
            volume = -80;
        }
        else
        {
            volume = volume * (maxSoundVolume - minSoundVolume) + minSoundVolume;
        }
        mixer.SetFloat("SoundVolume", volume);
    }

    /// <summary>
    /// 玩家移动音效
    /// </summary>
    public void PlayPlayerMoveClip()
    {
        playerSource.clip = playerMoveClip;
        playerSource.Play();
    }

}
