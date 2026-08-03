using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public bool IsDashing {  get; private set; }

    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldownTime;

    private float dashTimeCount;
    private float dashCooldownCount;

    private void Start() {
        GameManager.instance.gameInput.DashPressed += GameInput_DashPressed;
    }

    private void Update() {
        if (IsDashing) {
            dashTimeCount += Time.deltaTime;
            EndDash();
        } else {
            dashCooldownCount += Time.deltaTime;
        }
    }

    private void GameInput_DashPressed() {
        if (dashCooldownCount >= dashCooldownTime) {
            IsDashing = true;
            dashCooldownCount = 0f;
        }
    }

    private void EndDash() { 
        if (dashTimeCount >= dashTime) {
            IsDashing = false;
            dashTimeCount = 0f;
        }
   }
}
