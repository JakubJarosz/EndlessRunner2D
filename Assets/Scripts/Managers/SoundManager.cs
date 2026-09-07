using UnityEngine;

public enum SoundType {
    Coin,
    PowerUp,
    Jump,
    Land,
    Death,
    Dash,
    Slide
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    public static SoundManager instance;
    private AudioSource audioSource;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Parameters 1.SoundType 2.Volumn
    public static void PlaySound(SoundType sound, float volumn = 1) {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volumn);
    }
}
