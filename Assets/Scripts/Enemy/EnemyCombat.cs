using System;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private Patroller _patroller;

    private bool _isAttacking;

    private void Update()
    {
        if (_patroller.PlayerDetected)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (_isAttacking)
            return;

        _isAttacking = true;
        _enemyAnimator.TriggerAttack();
        var hitted = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRadius);

        foreach (var hit in hitted)
        {
            if (hit.TryGetComponent(out Player player))
            {
                if (player.TryGetComponent(out Health health))
                {
                    health.DealDamage(_attackDamage);
                }
            }
        }
    }

    private void StopAttacking()
    {
        _isAttacking = false;
    }
}