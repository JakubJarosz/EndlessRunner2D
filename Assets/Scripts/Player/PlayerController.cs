using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlayerDetection detection;
    private PlayerJump jump;
    private PlayerSlide slide;
    private PlayerDash dash;

    [Header("Roll variables")]
    [SerializeField] private float performRollAtVelocity;

    [Header("Player position variables")]
    [SerializeField] private float treshhold;
    [SerializeField] private float centerSpeed;
    private float lastPosition;
    private float unchangedTimer;

    public enum PlayerState {
        Running,
        Jump,
        Fall,
        Slide,
        Dash,
        RollLanding
    }

    public PlayerState currentState {  get; private set; }
    private PlayerState previousState;

    public event Action PerformJump;
    public event Action<bool> PerformLanding;
    public event Action<bool> PerformSlide;
    public event Action<bool> PerformDash;

    // Roll landing variables
    private float lastYVelocity;
    private bool wasGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        detection = GetComponentInChildren<PlayerDetection>();
        jump = GetComponent<PlayerJump>();
        slide = GetComponent<PlayerSlide>();
        dash = GetComponent<PlayerDash>();
    }

    private void Start() {
        GameManager.instance.deathTrigger.PlayerDeath += DeathTrigger_PlayerDeath;
        wasGrounded = detection.IsGrounded();
    }

    private void Update() {
        HandleState();
        HandleCharacterCentering();
        HandlePositionAdjustion();
        HandleLanding();
        HandleGravity();

        if (CanPerformActions()) {
            HandleJump();
        }

   
        switch (currentState) {
            case PlayerState.Dash:
                HandleDash();
                break;
        }
    }

    private void HandleState() {
        previousState = currentState;

        if (currentState == PlayerState.RollLanding) {
            return;
        }

        if (dash.IsDashing) {
            currentState = PlayerState.Dash;
        } 
        else if (detection.IsGrounded()) {

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

        // Enter Dash
        if (currentState == PlayerState.Dash) {
            PerformDash?.Invoke(true);
        }

        // Exit Dash
        if (previousState == PlayerState.Dash) {
            PerformDash?.Invoke(false);
        }

        // Jump Trigger
        if (currentState == PlayerState.Jump) {
            PerformJump?.Invoke();
        }
    }

    private bool CanPerformActions() {
        return currentState != PlayerState.RollLanding;
    }

    private void HandlePositionAdjustion() {
        if (transform.position.x != 0) {

        }
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

    private void HandleDash() {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
    }

    private void DeathTrigger_PlayerDeath() {
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

  

    private void HandleCharacterCentering() {
        float currentPosition = transform.position.x;
        float delta = currentPosition - lastPosition;
        bool isBeingPushed = delta < -0.01f;
        bool isBeingCentered = delta > 0.01f;
        bool isNotMoving = Mathf.Abs(delta) <= 0.01f;

        if (isBeingPushed) {
            unchangedTimer = 0f;
        } else if (isNotMoving) {
            unchangedTimer += Time.deltaTime;

            if (unchangedTimer >= treshhold) {
                // Start centering
                CenterCharacter();
            }
        } else if (isBeingCentered) {
            CenterCharacter();
            unchangedTimer = 0f;
        }

            lastPosition = currentPosition;
    }

    private void CenterCharacter() {
        Vector3 pos = transform.position;

        pos.x = Mathf.MoveTowards(pos.x, 0f, centerSpeed * Time.deltaTime);

        transform.position = pos;
    }

    // Return functions
    public float GetYVelocity() {
        return rb.linearVelocity.y;
    }
}
