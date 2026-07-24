using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed;

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position += Vector3.left * initialSpeed * Time.deltaTime;
    }
}
