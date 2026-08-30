using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public List<int> scores = new List<int>();

    public event Action listUpdated;

    private void Awake() {
        GameManager.instance.scoreboardManager = this;

        // Load save
        scores = ScoreSaveSystem.Load();
    }

    private void Start() {
        GameManager.instance.stateHasChanged += Instance_stateHasChanged;
    }

    private void Instance_stateHasChanged(GameManager.GameState obj) {
        if (obj == GameManager.GameState.Death) {
            AddNewScore();
        }
    }

    private void AddNewScore() {
        int coins = GameManager.instance.coinCounter == 0 ? 1 : GameManager.instance.coinCounter;
        int score = GameManager.instance.meterCounter * coins;
        scores.Add(score);

        // update save
        ScoreSaveSystem.Save(scores);
        listUpdated?.Invoke();
    }

  
}
