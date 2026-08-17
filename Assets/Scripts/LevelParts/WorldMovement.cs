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
    private float speedMultiplier = 1f;
    private float speedOffset = 0f;

    private void Start() {
        baseSpeed = initialSpeed;
        GameManager.instance.deathTrigger.PlayerDeath += DeathTrigger_PlayerDeath;
    }

    private void Update() {
        if (stopMovement) return;

        float distanceThisFrame = Time.deltaTime * currentSpeed;
        meterCounter += distanceThisFrame;
        GameManager.instance.SetDistance(meterCounter);
        GameManager.instance.SetSpeed(currentSpeed);

        SpeedIncreaseOverTime();
        CalculateFinalSpeed();
        Move();
    }

    private void Move() {
        transform.position += currentSpeed * Time.deltaTime * Vector3.left;
    }

    private void SpeedIncreaseOverTime() {
        baseSpeed += speedIncreaseRate * Time.deltaTime;
    }

    private void CalculateFinalSpeed() {
        // Player dashes
        if (playerController.currentState == PlayerController.PlayerState.Dash) {
            speedMultiplier = 1.4f;
        } else {
            speedMultiplier = 1f;
        }

            currentSpeed = (baseSpeed + speedOffset) * speedMultiplier;
    }

    private void DeathTrigger_PlayerDeath() {
        stopMovement = true;
    }

    public bool CanSpawnBooster() {
        return currentSpeed >= speedWhenTheSlowBoostCanSpawn && speedMultiplier == 1f;
    }

    public void SlowDownSpeed() {
        speedOffset -= 6f;
        GameManager.instance.levelManager.ZeroBoosterVariables();
    }
}
