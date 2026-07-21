using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private GameInputs inputs;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float howLongCanHoldJump;

    [Header("Gravity Settings")]
    [SerializeField] private float baseGravity = 1f;
    [SerializeField] private float jumpHoldGravity = 0.6f;
    [SerializeField] private float jumpCutGravity = 2.5f;
    [SerializeField] private float fallGravity = 3.5f;

    private bool isJumpPressed;
    private bool wasJumpPressed;
    private float jumpTime;

    public bool IsHoldingJump => isJumpPressed && jumpTime < howLongCanHoldJump;
    public bool JumpPressedThisFrame {  get; private set; }

    private void Start() {
        inputs = GameManager.instance.gameInput;
        inputs.IsJumpPressed += Inputs_IsJumpPressed;
    }

    private void Update() {
        JumpPressedThisFrame = isJumpPressed && !wasJumpPressed;

        HandleJump();

        wasJumpPressed = isJumpPressed;
    }

    private void Inputs_IsJumpPressed(bool obj) {
        isJumpPressed = obj;
    }

    private void HandleJump() {
        if (isJumpPressed) {
            jumpTime += Time.deltaTime;
        } else {
            jumpTime = 0f;
        }
    }

    public float GetJumpForce() {
        return jumpForce;
    }

    public float BaseGravity => baseGravity;
    public float JumpHoldGravity => jumpHoldGravity;
    public float JumpCutGravity => jumpCutGravity;
    public float FallGravity => fallGravity;
}
