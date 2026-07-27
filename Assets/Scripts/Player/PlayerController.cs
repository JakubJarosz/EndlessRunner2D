using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerDetection detection;
    private PlayerJump jump;

    [SerializeField] private float performRollAtVelocity;

    public enum PlayerState {
        Running,
        Jump,
        Fall,
        RollLanding
    }

    public PlayerState state {  get; private set; }

    public event Action PerformJump;
    public event Action<bool> PerformLanding;

    // Roll landing variables
    private float lastYVelocity;
    private bool wasGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        detection = GetComponentInChildren<PlayerDetection>();
        jump = GetComponent<PlayerJump>();
    }

    private void Start() {
        wasGrounded = detection.IsGrounded();
    }

    private void Update() {
        HandleState();
        HandleLanding();
        HandleGravity();

        if (CanPerformActions()) {
            Jump();
        }

   
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
        if (state == PlayerState.RollLanding) {
            return;
        }

        if (detection.IsGrounded()) {
            state = PlayerState.Running;
        } else {
            state = rb.linearVelocity.y > 0 ? PlayerState.Jump : PlayerState.Fall;
        }
    }

    private bool CanPerformActions() {
        return state != PlayerState.RollLanding;
    }

    private void Jump() {
        if (jump.HasBufferJump && jump.HasCoyote) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump.GetJumpForce());
            jump.ClearJumpBuffer();
            jump.ClearCoyote();
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

    private void HandleLanding() {
        bool isGrounded = detection.IsGrounded();

        if (!wasGrounded && isGrounded) {
            // if true roll, else normal landing
            bool shouldRoll = lastYVelocity <= -performRollAtVelocity;
            PerformLanding?.Invoke(shouldRoll);

            // block movement for a while when roll landing
            if (shouldRoll) {
                state = PlayerState.RollLanding;
                // zero buffer so Player wont jump
                jump.ClearJumpBuffer();
                StartCoroutine(EndRoll());
            }
        }

        wasGrounded = isGrounded;
        lastYVelocity = rb.linearVelocity.y;
    }

    private IEnumerator EndRoll() {
        yield return new WaitForSeconds(0.3f);
        state = PlayerState.Running;
    }

    // Return functions
    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }

}
