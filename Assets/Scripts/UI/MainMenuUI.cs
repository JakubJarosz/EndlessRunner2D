using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void PlayButton() {
        SceneLoader.Instance.LoadScene(SceneLoader.Scenes.GameScene);
    }
}
