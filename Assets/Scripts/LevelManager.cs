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

    private void SpawnNext() {
        EndPointSpawner spawnPoint = lastSpawned.GetComponentInChildren<EndPointSpawner>();

        GameObject prefab = levelListSO.GetRandomPart(lastSpawned);
        GameObject newPart = spawnPoint.SpawnNewPart(prefab);

        activeParts.Enqueue(newPart);
        lastSpawned = newPart;

        if (activeParts.Count > maxParts) {
            GameObject old = activeParts.Dequeue();
            Destroy(old);
        }
    }
}
