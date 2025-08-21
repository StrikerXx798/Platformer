using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundMask;

    private bool _isGrounded;
    private PlayerAnimator _playerAnimator;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        _isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            _groundCheckDistance,
            _groundMask
        );
        _playerAnimator.SetIsGrounded(_isGrounded);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(transform.position, Vector3.down * _groundCheckDistance);
    }
}