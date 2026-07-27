using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed;
    [SerializeField] private float speedIncreaseRate;

    private float currentSpeed;

    private void Start() {
        currentSpeed = initialSpeed;    
    }

    private void Update() {
        SetCurrentSpeed();
        Move();
    }

    private void Move() {
        transform.position += Vector3.left * currentSpeed * Time.deltaTime;
    }

    private void SetCurrentSpeed() {
        currentSpeed += speedIncreaseRate * Time.deltaTime;
    }
}
