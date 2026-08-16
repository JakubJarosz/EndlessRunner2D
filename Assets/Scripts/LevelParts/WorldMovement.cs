using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncreaseRate;
    [SerializeField] private float speedWhenTheSlowBoostCanSpawn;

    private float baseSpeed;
    private float currentSpeed;
    private bool stopMovement;

    private float meterCounter;

    private void Start() {
        baseSpeed = initialSpeed;
        GameManager.instance.deathTrigger.PlayerDeath += DeathTrigger_PlayerDeath;
    }

    private void Update() {
        if (stopMovement) return;

        SpeedIncreaseOverTime();
        CalculateFinalSpeed();
        Move();
    }

    private void Move() {
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    private void SpeedIncreaseOverTime() {
        baseSpeed += speedIncreaseRate * Time.deltaTime;
    }

    private void CalculateFinalSpeed() {
        float multiplier = 1f;

        // Player dashes
        if (playerController.currentState == PlayerController.PlayerState.Dash) {
            multiplier *= 1.4f;
        }

        currentSpeed = baseSpeed * multiplier;
    }


    private void DeathTrigger_PlayerDeath() {
        stopMovement = true;
    }

}
