using UnityEngine;

[CreateAssetMenu()]
public class SoundDataSO : ScriptableObject
{
    public SoundType type;
    public AudioClip[] clips;
}
