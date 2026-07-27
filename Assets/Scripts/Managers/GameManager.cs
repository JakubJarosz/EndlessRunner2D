using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameInputs gameInput;
    public LevelManager levelManager;

    private void Awake() {
        instance = this;
    }
}
