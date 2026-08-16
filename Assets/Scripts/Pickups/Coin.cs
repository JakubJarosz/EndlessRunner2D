using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Movement values")]
    [SerializeField] private float amplitude;
    [SerializeField] private float speed;
    [SerializeField] private float pickUpSpeed;

    private float startY;
    private bool playerPickedUpCoin;
    private Vector3 target;

    private void Start() {
        startY = transform.position.y;
        // Setting targetUI as world position
        Vector2 pos = UIManager.Instance.coinCounterUI.position;
        float distanceFromCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        target = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, distanceFromCamera));
    }

    private void Update() {
        if (playerPickedUpCoin) {
            PickUp();
        } else {
            Movement();
        }
    }

    private void Movement() {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
   
        Vector3 pos = transform.position;
        pos.y = startY + yOffset;

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        playerPickedUpCoin = true;
    }

    private void PickUp() {
        transform.position = Vector3.MoveTowards(transform.position, target, pickUpSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 3f) {
            GameManager.instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
