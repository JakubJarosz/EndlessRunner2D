using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerDetection detection;
    private PlayerJump jump;

    public enum PlayerState {
        Running,
        Jump,
        Fall
    }

    public PlayerState state {  get; private set; }

    public event Action PerformJump;

    // Roll landing variables
    private float lastYVelocity;
    private bool wasGrounded;
    public bool ShouldRoll { get; private set; }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        detection = GetComponentInChildren<PlayerDetection>();
        jump = GetComponent<PlayerJump>();
    }

    private void Update() {
        HandleState();
        Jump();
        HandleGravity();
        HandleRollLanding();

        switch (state) {
            case PlayerState.Running:
                break;
            case PlayerState.Jump:
                break;
            case PlayerState.Fall:
                break;
        }
    }

    private void HandleState() {
        if (detection.IsGrounded()) {
            state = PlayerState.Running;
        } else {
            state = rb.linearVelocity.y > 0 ? PlayerState.Jump : PlayerState.Fall;
        }
    }

    private void Jump() {
        if (jump.JumpPressedThisFrame && detection.IsGrounded()) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump.GetJumpForce());
            PerformJump?.Invoke();
        } 
    }

    private void HandleGravity() {
        if (rb.linearVelocity.y > 0) {
            rb.gravityScale = jump.IsHoldingJump ? jump.JumpHoldGravity : jump.JumpCutGravity;
        } else if (rb.linearVelocity.y < 0) {
            rb.gravityScale = jump.FallGravity;
        } else {
            rb.gravityScale = jump.BaseGravity;
        }
    }

    private void HandleRollLanding() {
        bool isGrounded = detection.IsGrounded();

        if (!wasGrounded && isGrounded) {
            ShouldRoll = lastYVelocity <= -32f;
        }

        wasGrounded = isGrounded;
        lastYVelocity = rb.linearVelocity.y;
    }

    // Return functions
    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }

}
