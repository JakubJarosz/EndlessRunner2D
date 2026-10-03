using UnityEngine;

public class PlaySoundExit : StateMachineBehaviour
{
    [SerializeField] private AudioType audioType;
    [SerializeField] private SoundType sound;
    [SerializeField, Range(0, 1)] private float volumn = 1;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (audioType == AudioType.OneShot) {

            SoundManager.instance.PlaySFX(sound, volumn);

        } else if (audioType == AudioType.Loop) {

            SoundManager.instance.StopLoop();
        } else {

            SoundManager.instance.StopMusic();
        }
    }
}
