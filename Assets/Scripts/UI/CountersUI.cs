using TMPro;
using UnityEngine;

public class CountersUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceUI;
    [SerializeField] private TextMeshProUGUI coinUI;

    private void Update() {
        distanceUI.text = GameManager.instance.meterCounter.ToString() + " meters";
        coinUI.text = GameManager.instance.coinCounter.ToString();
    }
}
