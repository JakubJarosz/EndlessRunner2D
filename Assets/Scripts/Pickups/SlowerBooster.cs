using UnityEngine;

public class SlowerBooster : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision) {
        WorldMovement worldMov = GetComponentInParent<WorldMovement>();
        if (worldMov == null) {
            Debug.Log("SlowerBooster variable is null");
        }
        worldMov.SlowDownSpeed();
        Destroy(gameObject);
    }
}
