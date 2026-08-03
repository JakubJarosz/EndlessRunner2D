using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameInputs gameInput;
    public LevelManager levelManager;
    public DeathTrigger deathTrigger;

    private void Awake() {
        instance = this;
    }
}
