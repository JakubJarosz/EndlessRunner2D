using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator anim;

    private PlayerController controller;

    private void Awake() {
        anim = GetComponent<Animator>();

        controller = GetComponentInParent<PlayerController>();
    }

    private void Start() {
        controller.PerformJump += Controller_PerformJump;
        controller.PerformLanding += Controller_PerformLanding;
    }

    private void Controller_PerformLanding(bool obj) {
        if (obj) {
            anim.SetTrigger("rollLanding");
        } else {
            anim.SetTrigger("normalLanding");
        }
    }

    private void Update() {
        anim.SetFloat("yVelocity", controller.GetYVelocity());
    }


    private void Controller_PerformJump() {
        anim.SetTrigger("jump");
    }
}
