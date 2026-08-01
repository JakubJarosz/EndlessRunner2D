using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    private BoxCollider2D col;

    [Header("Slide collider size")]
    [SerializeField] private Vector2 slideSize;
    [SerializeField] private Vector2 slideOffset;

    [Header("Slide Timers")]
    [SerializeField] private float minSlideDuration;
    [SerializeField] private float slideCooldown;

    private Vector2 runSize;
    private Vector2 runOffset;

    private bool isSlidingPressed;

    private float slideTimer;
    private float cooldownTimer;

    public bool IsSliding {  get; private set; }

    private void Awake() {
        col = GetComponent<BoxCollider2D>();
        runSize = col.size;
        runOffset = col.offset;
    }

    private void Start() {
        GameManager.instance.gameInput.IsSlidePressed += pressed => isSlidingPressed = pressed;
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (!IsSliding) {
            TryToStartSlide();
        } else {
            HandleSlide();
        }
    }

    private void OnDrawGizmos() {
        if (col == null) {
            col = GetComponent<BoxCollider2D>(); 
        }

        Vector3 pos = transform.position;

        // current
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos + (Vector3)col.offset, col.size);

        //// running
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(pos + (Vector3)runOffset, runSize);

        //// sliding
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireCube(pos + (Vector3)slideOffset, slideSize);
    }

    private void TryToStartSlide() {
        if (isSlidingPressed && cooldownTimer >= slideCooldown) {
            cooldownTimer = 0f;
            slideTimer = 0f;

            IsSliding = true;
            col.size = slideSize;
            col.offset = slideOffset;
        }
    }

    private void HandleSlide() {
        slideTimer += Time.deltaTime;

        if (slideTimer < minSlideDuration) return;

        if (!isSlidingPressed) {
            IsSliding = false;
            col.size = runSize;
            col.offset = runOffset;
        }
    }
}
