using UnityEngine;

public class BoostersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject slowerPrefab;

    private WorldMovement worldMov;
    private BoosterPos[] boosterPos;

    private void Awake() {
        worldMov = GetComponentInParent<WorldMovement>();
        boosterPos = GetComponentsInChildren<BoosterPos>(true);
    }

    public void SpawnBooster() {
        if (!worldMov.CanSpawnBooster()) return;

        int randomIndex = Random.Range(0, boosterPos.Length);
        BoosterPos randomPos = boosterPos[randomIndex];
        randomPos.gameObject.SetActive(true);
        Instantiate(slowerPrefab, randomPos.transform);
    }
}
