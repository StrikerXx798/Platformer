using System;
using UnityEngine;

[RequireComponent(typeof(HealthAnimator))]
public class HealthAnimation : MonoBehaviour
{
    [SerializeField] Health _health;
    private HealthAnimator _healthAnimator;

    private void Awake()
    {
        _healthAnimator = GetComponent<HealthAnimator>();
    }

    private void OnEnable()
    {
        _health.DamageTaken += OnHurt;
    }
    
    private void OnDisable()
    {
        _health.DamageTaken += OnHurt;
    }

    private void OnHurt()
    {
        if (_health.CurrentHealth <= 0)
        {
            _healthAnimator.TriggerDie();
            return;
        }
        
        _healthAnimator.TriggerHurt();
    }
}