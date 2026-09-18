using System;
using UnityEngine;

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

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // IF IN UNITY EDITOR
    private void OnEnable() {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++) {
            soundList[i].name = names[i];
        }
    }

    // Parameters 1.SoundType 2.Volumn
    public static void PlaySound(SoundType sound, float volumn = 1) {

        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volumn);
    }
}

[Serializable]
public struct SoundList {
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}

