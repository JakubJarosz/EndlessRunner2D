using UnityEngine;

public class PartSpawner : MonoBehaviour
{
    public GameObject SpawnNewPart(GameObject part) {
        Transform parent = GetComponentInParent<WorldMovement>().transform;
        Vector3 spawnPoint = transform.position + Vector3.right * 100f;
        return Instantiate(part, spawnPoint, Quaternion.identity, parent);
    }
}
