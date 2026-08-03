using System;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public event Action PlayerDeath;

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null ) {
            PlayerDeath?.Invoke();
        }
    }
}
