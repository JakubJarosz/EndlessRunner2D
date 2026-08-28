using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameInputs gameInput;
    public LevelManager levelManager;

    public event Action<GameState> stateHasChanged;

    public enum GameState {
        PreStart,
        Gameplay,
        Death,
    }

    public int meterCounter {  get; private set; }
    public int coinCounter { get; private set; }
    public int speedCounter { get; private set; }

    public GameState state { get; private set; }


    private void Awake() {
        instance = this;
    }

    private void Start() {
        UpdateGameState(GameState.PreStart);
    }

    public void UpdateGameState (GameState gameState) {
        state = gameState;
        stateHasChanged?.Invoke(gameState);
    }

    public void AddCoin() {
        coinCounter ++;
    }

    public void SetDistance(float distance) {
        meterCounter = Mathf.FloorToInt(distance);
    }

    public void SetSpeed(float speed) {
        speedCounter = Mathf.FloorToInt(speed); ;
    }
}
