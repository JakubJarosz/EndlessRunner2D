using TMPro;
using UnityEngine;

public class CountersUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceUI;
    [SerializeField] private TextMeshProUGUI coinUI;
    [SerializeField] private TextMeshProUGUI speedUI;

    private void Update() {
        distanceUI.text = GameManager.instance.meterCounter.ToString() + " meters";
        coinUI.text = GameManager.instance.coinCounter.ToString();
        speedUI.text = GameManager.instance.speedCounter.ToString() + " m/s";
    }
}
