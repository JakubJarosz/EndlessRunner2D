using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncreaseRate;

    private float currentSpeed;
    private bool stopMovement;

    private void Start() {
        currentSpeed = initialSpeed;
        GameManager.instance.deathTrigger.PlayerDeath += DeathTrigger_PlayerDeath;
    }

    private void Update() {
        if (!stopMovement) {
            SpeedIncreaseOverTime();
            DashIncrease();
            Move();
        }
    }

    private void Move() {
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    private void SpeedIncreaseOverTime() {
        currentSpeed += speedIncreaseRate * Time.deltaTime;
    }

    private void DashIncrease() {
        float speed = currentSpeed;
        if (playerController.currentState == PlayerController.PlayerState.Dash) {
            speed += 5f;
        }
        transform.position += Vector3.left * speed * Time.deltaTime;
    }


    private void DeathTrigger_PlayerDeath() {
        stopMovement = true;
    }

}
