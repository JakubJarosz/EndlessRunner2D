using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private GameObject scorePrefab;

    private void Start() {
        GameManager.instance.scoreboardManager.listUpdated += ScoreboardManager_listUpdated;
        UpdateUI();
    }

    private void ScoreboardManager_listUpdated() {
        UpdateUI();
    }

    private void UpdateUI() {
        List<int> list = GameManager.instance.scoreboardManager.scores;
        for (int i = 0; i < list.Count; i++) {
            SpawnNewScore(i + 1, list[i]);
        }
    }

    private void SpawnNewScore(int pos, int score) {
        GameObject newScore = Instantiate(scorePrefab, transform);
        ScoreEntryUI entry = newScore.GetComponent<ScoreEntryUI>();
        entry.SetUp(pos, score);
    }
}
