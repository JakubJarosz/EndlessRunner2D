using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    public event Action<bool> IsJumpPressed;
    public event Action<bool> IsSlidePressed;
    public event Action DashPressed;

    private void Awake() {
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.Jump.started += ctx => IsJumpPressed?.Invoke(true);
        inputActions.Player.Jump.canceled += ctx => IsJumpPressed?.Invoke(false);

        inputActions.Player.Slide.started += ctx => IsSlidePressed?.Invoke(true);
        inputActions.Player.Slide.canceled += ctx => IsSlidePressed?.Invoke(false);

        inputActions.Player.Dash.performed += ctx => DashPressed?.Invoke();
    }
}
