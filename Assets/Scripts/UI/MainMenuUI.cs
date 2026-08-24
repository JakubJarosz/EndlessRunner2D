using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton() {
        SceneLoader.Instance.LoadScene(SceneLoader.Scenes.GameScene);
    }

    public void OptionsButton() {
        Debug.Log("Options");
    }

    public void ExitButton() {
        Application.Quit();
    }
}
