using System;
using UnityEngine;

public class GameInputs : MonoBehaviour
{
    private InputActions inputActions;

    // Player events
    public event Action<bool> IsJumpPressed;
    public event Action<bool> IsSlidePressed;
    public event Action DashPressed;

    // UI events
    public event Action EnterPressed;
    public event Action EscPressed;
    public event Action StartRunPressed;

    private void Awake() {
        inputActions = new InputActions();

        inputActions.Player.Disable();
        inputActions.UI.Enable();

        // Player Inputs
        inputActions.Player.Jump.started += ctx => IsJumpPressed?.Invoke(true);
        inputActions.Player.Jump.canceled += ctx => IsJumpPressed?.Invoke(false);

        inputActions.Player.Slide.started += ctx => IsSlidePressed?.Invoke(true);
        inputActions.Player.Slide.canceled += ctx => IsSlidePressed?.Invoke(false);

        inputActions.Player.Dash.performed += ctx => DashPressed?.Invoke();

        // UI inputs
        inputActions.UI.Accept.performed += ctx => EnterPressed?.Invoke();
        inputActions.UI.Cancel.performed += ctx => EscPressed?.Invoke();
        inputActions.UI.StartRun.performed += ctx => StartRunPressed?.Invoke();
    }

    public void EnableGameplay() {
        inputActions.Player.Enable();
        inputActions.UI.Disable();
    }

    public void EnableUI() {
        inputActions.Player.Disable();
        inputActions.UI.Enable();
    }
}
