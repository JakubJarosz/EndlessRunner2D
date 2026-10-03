using UnityEngine;

public class PlaySoundEnter : StateMachineBehaviour
{
    [SerializeField] private AudioType audioType;
    [SerializeField] private SoundType sound;
    [SerializeField, Range(0,1)] private float volumn;

     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (audioType == AudioType.OneShot) {

            SoundManager.instance.PlaySFX(sound, volumn);

        } else if (audioType == AudioType.Loop) {

            SoundManager.instance.PlayLoop(sound, volumn);
        } else {

            SoundManager.instance.PlayMusic(sound, volumn);
        }
    }
}
