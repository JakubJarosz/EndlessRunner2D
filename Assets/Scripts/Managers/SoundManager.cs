using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType {
    OneShot,
    Loop,
    Music
}

public enum SoundType {
    Coin,
    PowerUp,
    Jump,
    Land,
    Death,
    Dash,
    Slide,
    Step
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource loopSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Sound Database")]
    [SerializeField] private SoundDataSO[] soundDatabase;

    private Dictionary<SoundType, SoundDataSO> soundMap;

    private void Awake() {
        instance = this;

        soundMap = new Dictionary<SoundType, SoundDataSO>();
        foreach (var sound in soundDatabase) {
            soundMap[sound.type] = sound;
        }
    }

    // ------------------- SFX -------------------

    public void PlaySFX(SoundType type, float volumn = 1f) {
        AudioClip clip = GetAClip(type);
        sfxSource.PlayOneShot(clip, volumn);
    }

    // ------------------- SFX -------------------

    public void PlayLoop(SoundType type, float volumn = 1f) {
        AudioClip clip = GetAClip(type);

        loopSource.clip = clip;
        loopSource.loop = true;
        loopSource.volume = volumn;
        loopSource.Play();
    }

    public void StopLoop() {
        loopSource.Stop();
        loopSource.clip = null;
    }

    // ------------------- SFX -------------------

    public void PlayMusic(SoundType type, float volumn = 1f) {
        AudioClip clip = GetAClip(type);

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = volumn;  
        musicSource.Play();
    }

    public void StopMusic() {
        musicSource.Stop();
        musicSource = null;
    }
    // ------------------- Helper functions -------------------

    private AudioClip GetAClip(SoundType type) {
        AudioClip[] clips = soundMap[type].clips;

        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}


