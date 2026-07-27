using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    public event Action<bool> IsJumpPressed;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Jump.started += ctx => IsJumpPressed?.Invoke(true);
        inputActions.Player.Jump.canceled += ctx => IsJumpPressed?.Invoke(false);
    }
}
