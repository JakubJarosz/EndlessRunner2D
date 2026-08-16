using UnityEngine;

public class CoinPack : MonoBehaviour
{
    private BoosterPos[] spawnPoints;

    private void Awake() {
        spawnPoints = GetComponentsInChildren<BoosterPos>(true);
    }
}
