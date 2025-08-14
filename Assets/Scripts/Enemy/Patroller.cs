using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private List<TargetPoint> _path;
    [SerializeField] private float _moveSpeed = 2.5f;

    private int _currentPointIndex = 0;

    public Vector2 CurrentVelocity { get; private set; }

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
        var targetPoint = _path[_currentPointIndex].transform.position;
        var vector3 = transform.position;
        var newXPosition = Mathf.MoveTowards(transform.position.x, targetPoint.x, _moveSpeed * Time.deltaTime);
        var vector2 = CurrentVelocity;
        vector2.x = (newXPosition - transform.position.x) / Time.deltaTime;
        CurrentVelocity = vector2;
        transform.position = new Vector3(newXPosition, vector3.y, vector3.z);
    }
}