using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public void PlayStep() {
        SoundManager.instance.PlaySFX(SoundType.Step, 0.1f);
    }
}
