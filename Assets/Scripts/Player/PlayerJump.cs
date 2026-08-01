using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private GameInputs inputs;
    private PlayerDetection detection;

    [Header("HandleJump Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float howLongCanHoldJump;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float coyoteJumpTime;

    [Header("Gravity Settings")]
    [SerializeField] private float baseGravity = 1f;
    [SerializeField] private float jumpHoldGravity = 0.6f;
    [SerializeField] private float jumpCutGravity = 2.5f;
    [SerializeField] private float fallGravity = 3.5f;

    private bool isJumpPressed;
    private bool wasJumpPressed;
    private bool JumpPressedThisFrame;
    private float jumpTime;

    private float jumpBufferCounter;
    private float coyoteJumpCounter;

    public bool IsHoldingJump => isJumpPressed && jumpTime < howLongCanHoldJump;
    public bool HasBufferJump => jumpBufferCounter > 0f;
    public bool HasCoyote => coyoteJumpCounter > 0f;

    private void Awake() {
        detection = GetComponentInChildren<PlayerDetection>();
    }

    private void Start() {
        inputs = GameManager.instance.gameInput;
        inputs.IsJumpPressed += Inputs_IsJumpPressed;
    }

    private void Update() {
        HandleCoyote();

        JumpPressedThisFrame = isJumpPressed && !wasJumpPressed;

        if (JumpPressedThisFrame) {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }

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

    private void HandleCoyote() {
        bool isGrounded = detection.IsGrounded();

        if (isGrounded) {
            coyoteJumpCounter = coyoteJumpTime;
        } else {
            coyoteJumpCounter -= Time.deltaTime;
        }
    }

    public void ClearJumpBuffer() {
        jumpBufferCounter = 0f;
    }

    public void ClearCoyote() {
        coyoteJumpCounter = 0f;
    }

    public float GetJumpForce() {
        return jumpForce;
    }

    public float BaseGravity => baseGravity;
    public float JumpHoldGravity => jumpHoldGravity;
    public float JumpCutGravity => jumpCutGravity;
    public float FallGravity => fallGravity;
}
