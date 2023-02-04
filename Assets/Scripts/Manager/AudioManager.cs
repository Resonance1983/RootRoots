using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音乐和音效管理
/// </summary>
public class AudioManager : Level<AudioManager>
{
    //玩家音效
    [SerializeField]
    private AudioSource playerSource;
    //背景音效
    [SerializeField]
    private AudioSource backgroundSource;
    //交互音效
    [SerializeField]
    private AudioSource interactSource;

    [Header("玩家音效")]
    public AudioClip playerMoveClip;
    public AudioClip playerpushClip;

    [Header("背景音效")]
    public AudioClip gameClip;

    [Header("交互音效")]
    public AudioClip computClip;

}
