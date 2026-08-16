using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public RectTransform coinCounterUI;

    private void Awake() {
        Instance = this;
    }
}
