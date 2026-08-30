using TMPro;
using UnityEngine;

public class ScoreEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void SetUp(int pos, int score) {
        positionText.text = pos.ToString();
        scoreText.text = score.ToString();
    }
}
