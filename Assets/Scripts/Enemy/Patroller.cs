using System;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private float _checkDistance;
    [SerializeField] private List<TargetPoint> _path;
    [SerializeField] private float _moveSpeed = 2.5f;

    private int _currentPointIndex = 0;
    private Vector2 _playerPosition;

    public Vector2 CurrentVelocity { get; private set; }
    public bool PlayerDetected { get; private set; }

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

    private void Update()
    {
        MoveToTarget();
        CheckPlayer();
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

    private void MoveToTarget()
    {
        Vector2 targetPoint;

        if (PlayerDetected)
        {
            targetPoint = _playerPosition;
        }
        else
        {
            targetPoint = _path[_currentPointIndex].transform.position;
        }

        var vector3 = transform.position;
        var newXPosition = Mathf.MoveTowards(transform.position.x, targetPoint.x, _moveSpeed * Time.deltaTime);
        var vector2 = CurrentVelocity;
        vector2.x = (newXPosition - transform.position.x) / Time.deltaTime;
        CurrentVelocity = vector2;
        transform.position = new Vector3(newXPosition, vector3.y, vector3.z);
    }

    private void CheckPlayer()
    {
        var hitted = Physics2D.RaycastAll(transform.position, CurrentVelocity.x < 0 ? Vector2.left : Vector3.right,
            _checkDistance);

        foreach (var hit in hitted)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                PlayerDetected = true;
                _playerPosition = player.transform.position;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (CurrentVelocity.x < 0)
        {
            Gizmos.DrawRay(transform.position, Vector3.left * _checkDistance);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector3.right * _checkDistance);
        }
    }
}