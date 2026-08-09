using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private CoinPack[] children;

    private void Awake() {
        children = GetComponentsInChildren<CoinPack>(true);
    }

 
    public void EnableRandomCoinPart() {
        int randomIndex = Random.Range(0, children.Length);
        CoinPack randomChild = children[randomIndex];
        randomChild.gameObject.SetActive(true);
    }
}
