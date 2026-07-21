using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator anim;

    private PlayerController controller;
    [SerializeField] private PlayerDetection detection;

    private void Awake() {
        anim = GetComponent<Animator>();

        controller = GetComponentInParent<PlayerController>();
    }

    private void Start() {
        controller.PerformJump += Controller_PerformJump;
    }

    private void Update() {
        anim.SetBool("isGrounded", detection.IsGrounded());
        anim.SetBool("shouldRoll", controller.ShouldRoll);
        anim.SetFloat("yVelocity", controller.GetYVelocity());
    }


    private void Controller_PerformJump() {
        anim.SetTrigger("jump");
    }
}
