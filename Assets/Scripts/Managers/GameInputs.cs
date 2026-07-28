using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    public event Action<bool> IsJumpPressed;
    public event Action OnDashPressed;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Jump.started += ctx => IsJumpPressed?.Invoke(true);
        inputActions.Player.Jump.canceled += ctx => IsJumpPressed?.Invoke(false);
        inputActions.Player.Dash.performed += ctx => OnDashPressed?.Invoke();
    }
}
