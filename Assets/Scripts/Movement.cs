using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private const string MoveVariable = "xVelocity";
    private const string JumpVariable = "yVelocity";
    private const string IsGrounded = "isGrounded";

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 7.5f;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private LayerMask _groundMask;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    private bool _isGrounded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        HandleCollision();
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
        _spriteRenderer.flipX = _rigidbody2D.linearVelocity.x < 0;
    }

    private void Jump()
    {
        var jumpPosition = new Vector2(_rigidbody2D.linearVelocity.x, _jumpForce);
        _rigidbody2D.linearVelocity = jumpPosition;
    }

    private void HandleCollision()
    {
        _isGrounded = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            _groundCheckDistance,
            _groundMask
        );
        _animator.SetBool(IsGrounded, _isGrounded);
    }

    private void HandleAnimations()
    {
        _animator.SetFloat(MoveVariable, _rigidbody2D.linearVelocity.x);
        _animator.SetFloat(JumpVariable, _rigidbody2D.linearVelocity.y);
    }
}