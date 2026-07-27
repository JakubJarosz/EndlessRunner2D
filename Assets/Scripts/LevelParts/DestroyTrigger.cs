using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start() {
        levelManager = GameManager.instance.levelManager;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        levelManager.DestroyPart();
    }
}
