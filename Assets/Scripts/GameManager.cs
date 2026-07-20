using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameInputs gameInput;

    private void Awake() {
        instance = this;
    }
}
