using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private Vector2 groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, groundCheckRadius);
    }

    private void Update() {
        Debug.Log(IsGrounded());
    }

    public bool IsGrounded() {
        return Physics2D.OverlapBox(transform.position, groundCheckRadius, groundLayer);
    }
}
