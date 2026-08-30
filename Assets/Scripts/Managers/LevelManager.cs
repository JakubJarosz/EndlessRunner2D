using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject initialSpawnPrefab;
    [SerializeField] private ListOfLevelPartsSO levelListSO;
    [SerializeField] private Transform parentOfLevelParts;

    private Queue<GameObject> activeParts = new Queue<GameObject>();
    private GameObject lastSpawned;

    private int maxParts = 5;

    private bool boosterWasSpawned;
    private int spawnCounterAfterBoost;

    private void Awake() {
        InitialSpawnParts();
        GameManager.instance.levelManager = this;
    }

    private void Start() {
        GameManager.instance.stateHasChanged += Instance_stateHasChanged;
    }

    private void Instance_stateHasChanged(GameManager.GameState obj) {
        if (obj == GameManager.GameState.Death) {
            DestroyAllParts();

            // if statment belove is to be triggered after player dies and presses retry, not initial run
        } else if (obj == GameManager.GameState.PreStart && activeParts.Count == 0) {
            InitialSpawnParts();
        }
    }

    private void DestroyAllParts() {
        foreach (var part in activeParts) {
            Destroy(part);
        }
        activeParts.Clear();
    }

    private void InitialSpawnParts() {
        GameObject first = Instantiate(initialSpawnPrefab, new Vector3(0, -4.4f, 0), Quaternion.identity, parentOfLevelParts);
        activeParts.Enqueue(first);
        lastSpawned = first;
        // spawn another 4 more parts, coz first one is initialSpawnPrefab
        for (int i = 0; i < maxParts - 1; i++) {
            SpawnNext();
        }
    }

    public void SpawnNext() {
        PartSpawner spawnPoint = lastSpawned.GetComponentInChildren<PartSpawner>();

        GameObject prefab = levelListSO.GetRandomPart(lastSpawned);
        if (prefab == null) {
            Debug.Log("Null");
        }
        GameObject newPart = spawnPoint.SpawnNewPart(prefab);
        if (boosterWasSpawned) {
            spawnCounterAfterBoost++;
        }

        // Coin Spawner
        CoinSpawner coinHandler = newPart.GetComponentInChildren<CoinSpawner>();
        coinHandler.EnableRandomCoinPart();

        // Booster Spawner
        if (spawnCounterAfterBoost == 0 || spawnCounterAfterBoost >= Random.Range(2,4)) {
            BoostersSpawner boostSpawner = newPart.GetComponentInChildren<BoostersSpawner>();
            boostSpawner.SpawnBooster();
            spawnCounterAfterBoost = 0;
            boosterWasSpawned = true;
        }
  
        activeParts.Enqueue(newPart);
        lastSpawned = newPart;
    }

    public void DestroyPart() {
        if (activeParts.Count > maxParts) {
            GameObject old = activeParts.Dequeue();
            Destroy(old);
        }
    }

    public void ZeroBoosterVariables() {
        spawnCounterAfterBoost = 0;
        boosterWasSpawned = false;
    }
}
