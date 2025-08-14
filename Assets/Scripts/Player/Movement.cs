using UnityEngine;

[RequireComponent(typeof(GroundChecker))]
[RequireComponent(typeof(PlayerAnimator), typeof(Rotator), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private InputReader _inputReader;

    private PlayerAnimator _playerAnimator;
    private Rigidbody2D _rigidbody2D;
    private Rotator _rotator;
    private Vector2 _moveDirection;

    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rotator = GetComponent<Rotator>();
    }

    private void Update()
    {
        Move();
        HandleAnimations();
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
    }

    private void Jump()
    {
        var jumpPosition = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        _rigidbody2D.linearVelocity = jumpPosition;
    }

    private void HandleAnimations()
    {
        _playerAnimator.SetXVelocity(_rigidbody2D.linearVelocity.x);
        _playerAnimator.SetYVelocity(_rigidbody2D.linearVelocity.y);
    }
}