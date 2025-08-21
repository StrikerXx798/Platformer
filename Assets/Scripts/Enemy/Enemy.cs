using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator),  typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rotator _rotator;
    private EnemyAnimator _enemyAnimator;
    private Patroller _patroller;

    private void Start()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _patroller = GetComponent<Patroller>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _enemyAnimator.SetXVelocity(_patroller.CurrentVelocity.x);
        _rotator.FlipSprite(_patroller.CurrentVelocity.x < 0);
    }
}