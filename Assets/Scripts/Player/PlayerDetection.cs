using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask platformLayer;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }

    public bool IsGrounded() {
        return Physics2D.OverlapCircle(transform.position, groundCheckRadius, platformLayer);
    }
}
