using System;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimator), typeof(Rotator), typeof(Patroller))]
public class Enemy : MonoBehaviour
{
    private EnemyAnimator _enemyAnimator;
    private Patroller _patroller;
    private Rotator _rotator;

    private void Start()
    {
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _patroller = GetComponent<Patroller>();
        _rotator = GetComponent<Rotator>();
    }

    private void Update()
    {
        _enemyAnimator.SetXVelocity(_patroller.CurrentVelocity.x);
        _rotator.FlipSprite(_patroller.CurrentVelocity.x < 0);
    }
}