using UnityEngine;

[RequireComponent(typeof(PlayerAnimator), typeof(Rotator), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundMask;

    private PlayerAnimator _playerAnimator;
    private Rigidbody2D _rigidbody2D;
    private Rotator _rotator;
    private Vector2 _moveDirection;

    private bool _isGrounded;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    private void Update()
    {
        Move();
        HandleCollision();
    }


    private void OnEnable()
    {
        if (_inputReader != null)
        {
            _inputReader.MovementInputReceived += OnMovementInputReceived;
            _inputReader.JumpActionPerformed += Jump;
        }
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.MovementInputReceived -= OnMovementInputReceived;
            _inputReader.JumpActionPerformed -= Jump;
        }
    }

    private void OnMovementInputReceived(Vector2 movementInput)
    {
        _moveDirection = movementInput;
    }

    private void Move()
    {
        _rigidbody2D.linearVelocity = new Vector2(_moveDirection.x * _speed, _rigidbody2D.linearVelocity.y);
        _rotator.FlipSprite(_rigidbody2D.linearVelocity.x < 0);
        _playerAnimator.SetXVelocity(_rigidbody2D.linearVelocity.x);
    }

    private void Jump()
    {
        var jumpPosition = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        _rigidbody2D.linearVelocity = jumpPosition;
        _playerAnimator.SetYVelocity(_rigidbody2D.linearVelocity.y);
    }

    private void HandleCollision()
    {
        _isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            _groundCheckDistance,
            _groundMask
        );
        _playerAnimator.SetIsGrounded(_isGrounded);
    }
}