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
        controller.PerformSlide += Controller_PerformSlide;
        controller.PerformDash += Controller_PerformDash;
        GameManager.instance.deathTrigger.PlayerDeath += Trigger_PlayerDeath;
    }

    private void Trigger_PlayerDeath() {
        anim.SetTrigger("death");
    }

    private void Controller_PerformDash(bool obj) {
        if (obj) {
            anim.SetTrigger("dashStart");
        } else {
            anim.SetTrigger("dashEnd");
        }
    }

    private void Controller_PerformSlide(bool obj) {
        if (obj) {
            anim.SetTrigger("slideStart");
        } else {
            anim.SetTrigger("slideEnd");
        }
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
        anim.SetBool("isGrounded", controller.currentState == PlayerController.PlayerState.Running);
    }


    private void Controller_PerformJump() {
        anim.SetTrigger("jump");
    }
}
