using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerDetection detection;
    private PlayerJump jump;
    private PlayerSlide slide;

    [SerializeField] private float performRollAtVelocity;

    public enum PlayerState {
        Running,
        Jump,
        Fall,
        Slide,
        RollLanding
    }

    public PlayerState currentState {  get; private set; }
    private PlayerState previousState;

    public event Action PerformJump;
    public event Action<bool> PerformLanding;
    public event Action<bool> PerformSlide;

    // Roll landing variables
    private float lastYVelocity;
    private bool wasGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        detection = GetComponentInChildren<PlayerDetection>();
        jump = GetComponent<PlayerJump>();
        slide = GetComponent<PlayerSlide>();
    }

    private void Start() {
        wasGrounded = detection.IsGrounded();
    }

    private void Update() {
        HandleState();
        HandleLanding();
        HandleGravity();

        if (CanPerformActions()) {
            HandleJump();
        }

   
        switch (currentState) {
            case PlayerState.Running:
                break;
            case PlayerState.Jump:
                break;
            case PlayerState.Fall:
                break;
        }
    }

    private void HandleState() {
        previousState = currentState;

        if (currentState == PlayerState.RollLanding) {
            return;
        }

        if (detection.IsGrounded()) {

            if (slide.IsSliding) {
                currentState = PlayerState.Slide;
            } else {
                currentState = PlayerState.Running;
            }
   
        } else {
            currentState = rb.linearVelocity.y > 0 ? PlayerState.Jump : PlayerState.Fall;
        }

        HandleStateChange();
    }

    private void HandleStateChange() {
        if (currentState == previousState) return;

        // Enter Slide
        if (currentState == PlayerState.Slide) {
            PerformSlide?.Invoke(true);
        }

        // Exit Slide
        if (previousState == PlayerState.Slide) {
            PerformSlide?.Invoke(false);
        }

        // Jump Trigger
        if (currentState == PlayerState.Jump) {
            PerformJump?.Invoke();
        }
    }

    private bool CanPerformActions() {
        return currentState != PlayerState.RollLanding;
    }

    private void HandleJump() {
        if (jump.HasBufferJump && jump.HasCoyote) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jump.GetJumpForce());
            jump.ClearJumpBuffer();
            jump.ClearCoyote();
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
                currentState = PlayerState.RollLanding;
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
        currentState = PlayerState.Running;
    }

    // Return functions
    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }

}
