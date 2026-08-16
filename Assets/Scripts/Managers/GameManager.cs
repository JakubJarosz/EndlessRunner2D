using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameInputs gameInput;
    public LevelManager levelManager;
    public DeathTrigger deathTrigger;

    public int meterCounter {  get; private set; }
    public int coinCounter { get; private set; }

    private void Awake() {
        instance = this;
    }

    public void AddCoin() {
        coinCounter ++;
    }

    public void SetDistance(float distance) {
        meterCounter = Mathf.FloorToInt(distance);
    }
}
