using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private GameInputs inputs;

    private void Start() {
        inputs = GameManager.instance.gameInput;
        inputs.IsJumpPressed += Inputs_IsJumpPressed;
    }

    private void Inputs_IsJumpPressed(bool obj) {
        throw new System.NotImplementedException();
    }
}
