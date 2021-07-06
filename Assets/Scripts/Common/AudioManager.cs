using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance = null;

    //音频开关 0关闭 1打开
    public int soundState = 1;

    public const int MusicUI = 0;
    public const int MusicGame0 = 1;
    public const int MusicGame1 = 2;
    public const int MusicGameWM = 3;

    public const int SoundButtonClick = 0;
    public const int SoundYF1 = 1;
    public const int SoundYF2 = 2;
    public const int SoundYF3 = 3;
    public const int SoundYF4 = 4;
    public const int SoundYF5 = 5;
    public const int SoundYF6 = 6;
	public const int SoundYF7 = 7;
    public const int SoundYF8 = 8;
    public const int SoundYF9 = 9;
    public const int SoundYF10 = 10;
    public const int SoundUnityBreak = 11;
    public const int SoundGameWinBest = 12;
    public const int SoundGameWin = 13;
    public const int SoundGameLose = 14;
    public const int SoundShoot = 15;
    public const int SoundClickBall = 16;
    public const int SoundLogo = 17;

    public const int SoundChuanBoom = 18;
    public const int SoundEnd2Boom = 19;
    public const int SoundStoneClickHit = 20;
    public const int SoundStoneBoom = 21;
    public const int SoundEndXian = 22;
    public const int SoundGetSound = 23;
    public const int SoundGetSoundMost = 24;
    public const int SoundLockBoom = 25;
    public const int SoundLockClick = 26;
    public const int SoundBigo = 27;
    public const int SoundWin150 = 28;

    public const int SoundButtonStartHandler = 29;
    public const int SoundButtonStartDowning = 30;

    public const int SoundRight = 31;
    public const int SoundRightEnd = 32;
    public const int SoundRightScendEnd = 33;

    public const int SoundBigLevel = 34;
    public const int SoundSmallLevel = 35;
    public const int SoundBtnStart = 36;
    public const int SoundTips = 37;
    public const int SoundStep = 38;
    public const int SoundUIShow = 39;

    public const int SoundStarUI = 40;
    public const int SoundClear = 41;

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    public AudioClip[] audioClipsMusic;
    public AudioClip[] audioClipsOne;
    public static AudioManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void PlayMusic(int id)
    {
        if (soundState == 0)
        {
            return;
        }
        audioSourceMusic.volume = 1;
        audioSourceMusic.clip = audioClipsMusic[id];
        audioSourceMusic.Play();
    }

    public void PlaySound(int id)
    {
        if (soundState == 0)
        {
            return;
        }        
        audioSourceSound.PlayOneShot(audioClipsOne[id]);
    }


    public void StopMusic()
    {
        if (soundState == 0)
        {
            return;
        }
        audioSourceMusic.volume = 0;
    }

    public void StopSound(int id)
    {
        if (soundState == 0)
        {
            return;
        }
        audioSourceSound.clip = audioClipsOne[id];
        audioSourceSound.Stop();
    }

    public void SetSoundState(int _state)
    {
        soundState = _state;
        if (soundState == 0)
        {
            if (audioSourceMusic.isPlaying)
                audioSourceMusic.Stop();
        }
        else
        {
            if (audioSourceMusic.isPlaying == false)
                audioSourceMusic.Play();
        }
    }

    private void Update()
    {
        //if (GameController.GetInstance().soundState == 0)
        //{
        //    if (audioSourceMusic.isPlaying)
        //        audioSourceMusic.Stop();
        //}
        //else
        //{
        //    if (audioSourceMusic.isPlaying==false)
        //        audioSourceMusic.Play();
        //}
    }
}
