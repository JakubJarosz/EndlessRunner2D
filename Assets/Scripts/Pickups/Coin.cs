using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private float amplitude;
    [SerializeField] private float speed;

    private float startY;

    private void Start() {
        startY = transform.position.y;
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
   
        Vector3 pos = transform.position;
        pos.y = startY + yOffset;

        transform.position = pos;
    }
}
