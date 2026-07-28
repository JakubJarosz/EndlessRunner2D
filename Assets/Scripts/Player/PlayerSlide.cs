using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    private Collider2D col;

    private void Awake() {
        col = GetComponent<Collider2D>();
    }

    private void Start() {
        GameManager.instance.gameInput.OnDashPressed += GameInput_OnDashPressed;
    }

    private void GameInput_OnDashPressed() {
        
    }
}
