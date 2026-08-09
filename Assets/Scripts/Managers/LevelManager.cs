using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject initialSpawn;
    [SerializeField] private ListOfLevelPartsSO levelListSO;

    private Queue<GameObject> activeParts = new Queue<GameObject>();
    private GameObject lastSpawned;

    private int maxParts = 5;

    private void Awake() {
        activeParts.Enqueue(initialSpawn);
        lastSpawned = initialSpawn;

        // spawn another 4 more parts, coz first one is initialSpawn
        for (int i = 0; i < maxParts - 1; i++) {
            SpawnNext();
        }
    }

    public void SpawnNext() {
        PartSpawner spawnPoint = lastSpawned.GetComponentInChildren<PartSpawner>();

        GameObject prefab = levelListSO.GetRandomPart(lastSpawned);
        GameObject newPart = spawnPoint.SpawnNewPart(prefab);

        CoinSpawner coinHandler = newPart.GetComponentInChildren<CoinSpawner>();
        coinHandler.EnableRandomCoinPart();

        activeParts.Enqueue(newPart);
        lastSpawned = newPart;
    }

    public void DestroyPart() {
        if (activeParts.Count > maxParts) {
            GameObject old = activeParts.Dequeue();
            Destroy(old);
        }
    }
}
