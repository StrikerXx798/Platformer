using System;
using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackDelay;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private Patroller _patroller;

    private Coroutine _attackCoroutine;

    private void Update()
    {
        if (_patroller.PlayerDetected)
        {
            if (PlayerInAttackRange())
            {
                if (_attackCoroutine is not null)
                    return;

                _attackCoroutine = StartCoroutine(AttackCoroutine());
            }
            else
            {
                StopAttacking();
            }
        }
        else
        {
            StopAttacking();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (PlayerInAttackRange())
        {
            _enemyAnimator.TriggerAttack();

            yield return new WaitForSeconds(_attackDelay);

            if (PlayerInAttackRange())
            {
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

            yield return null;
        }

        _attackCoroutine = null;
    }

    private bool PlayerInAttackRange()
    {
        var playerInAttackRange =
            Vector2.Distance(_attackPoint.position, _patroller.PlayerPosition) <= _attackRadius;

        return playerInAttackRange;
    }

    private void StopAttacking()
    {
        if (_attackCoroutine is null)
            return;

        StopCoroutine(_attackCoroutine);
    }
}