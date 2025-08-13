using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    private const string MoveVariable = "xVelocity";

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private List<TargetPoint> _path;

    private int _currentPointIndex = 0;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        if (_path is not null)
        {
            foreach (var targetPoint in _path)
            {
                targetPoint.Reached += ChangeTarget;
            }
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void OnDisable()
    {
        if (_path is not null)
        {
            foreach (var targetPoint in _path)
            {
                targetPoint.Reached -= ChangeTarget;
            }
        }
    }

    private void Move()
    {
        var targetPoint = _path[_currentPointIndex].transform.position;
        var vector3 = transform.position;
        var newXPosition = Mathf.MoveTowards(transform.position.x, targetPoint.x, _moveSpeed * Time.deltaTime);
        var currentVelocityX = (newXPosition - transform.position.x) / Time.deltaTime;
        transform.position = new Vector3(newXPosition, vector3.y, vector3.z);
        _animator.SetFloat(MoveVariable, currentVelocityX);
        _spriteRenderer.flipX = currentVelocityX < 0;
    }

    private void ChangeTarget()
    {
        var newTargetIndex = _currentPointIndex + 1;

        if (newTargetIndex >= _path.Count)
        {
            _currentPointIndex = 0;

            return;
        }

        _currentPointIndex = newTargetIndex;
    }
}