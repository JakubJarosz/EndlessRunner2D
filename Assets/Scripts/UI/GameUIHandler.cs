using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject preGameLayer;

    private void Start() {
        GameManager.instance.gameInput.StartRunPressed += GameInput_StartRunPressed;
    }

    private void GameInput_StartRunPressed() {
        preGameLayer.SetActive(false);
        GameManager.instance.gameInput.EnableGameplay();
        GameManager.instance.UpdateGameState(GameManager.GameState.Gameplay);
    }
}
