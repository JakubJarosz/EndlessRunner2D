using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private string targetScene;

    public enum Scenes {
        MainMenuScene,
        LoadingScene,
        GameScene
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void LoadScene(Scenes scene) {
        targetScene = scene.ToString();
        SceneManager.LoadScene(Scenes.LoadingScene.ToString());
    }

    public void LoadTargetScene() {
        SceneManager.LoadScene(targetScene);
    }
}
