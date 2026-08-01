using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private void Start() {
        GameManager.instance.gameInput.DashPressed += GameInput_DashPressed;
    }

    private void GameInput_DashPressed() {
        
    }
}
