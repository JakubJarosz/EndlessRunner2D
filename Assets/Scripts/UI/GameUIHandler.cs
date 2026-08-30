using UnityEngine;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject preGameLayer;
    [SerializeField] private GameObject deathLayer;

    private void Start() {
        GameManager.instance.gameInput.StartRunPressed += GameInput_StartRunPressed;
        GameManager.instance.stateHasChanged += Instance_stateHasChanged;
    }

    private void Instance_stateHasChanged(GameManager.GameState obj) {
        if (obj == GameManager.GameState.Death) {

            GameManager.instance.gameInput.EnableUI();
            deathLayer.SetActive(true);
        } else if (obj == GameManager.GameState.PreStart) {

            GameManager.instance.gameInput.EnableUI();
            deathLayer.SetActive(false);
            preGameLayer.SetActive(true);
        } else if (obj == GameManager.GameState.Gameplay) {

            preGameLayer.SetActive(false);
            GameManager.instance.gameInput.EnableGameplay();
        }
    }

    private void GameInput_StartRunPressed() {
        GameManager.instance.UpdateGameState(GameManager.GameState.Gameplay);
    }

    public void Retry() {
        //GameManager.instance.UpdateGameState(GameManager.GameState.PreStart);
        SceneLoader.Instance.LoadScene(SceneLoader.Scenes.GameScene);
    }

    public void MainMenu() {
        SceneLoader.Instance.LoadScene(SceneLoader.Scenes.MainMenuScene);
    }
}
