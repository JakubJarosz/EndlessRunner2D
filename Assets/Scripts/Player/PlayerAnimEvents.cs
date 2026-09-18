using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public void PlayStep() {
        SoundManager.PlaySound(SoundType.Step, 0.1f);
    }
}
