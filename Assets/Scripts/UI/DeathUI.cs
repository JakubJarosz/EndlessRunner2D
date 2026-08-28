using UnityEngine;

public class DeathUI : MonoBehaviour
{
    public void Retry() {

    }

    public void MainMenu() {
        SceneLoader.Instance.LoadScene(SceneLoader.Scenes.MainMenuScene);
    }
}
