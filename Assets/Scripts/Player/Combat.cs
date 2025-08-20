using UnityEngine;

public class Combat : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private void OnEnable()
    {
        if (_inputReader != null)
        {
            _inputReader.PrimaryActionPerformed += Attack;
        }
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.PrimaryActionPerformed -= Attack;
        }
    }

    private void Attack()
    {
        _playerAnimator.TriggerAttack();
        var hitted = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);

        foreach (var hit in hitted)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Health health))
                {
                    health.TakeDamage(_attackDamage);
                }
            }
        }
    }
}