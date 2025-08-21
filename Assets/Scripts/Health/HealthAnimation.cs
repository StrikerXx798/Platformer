using UnityEngine;

public class HealthAnimation : MonoBehaviour
{
    [SerializeField] Health _health;
    [SerializeField] private HealthAnimator _healthAnimator;

    private void OnEnable()
    {
        _health.HealthChanged += OnHurt;
    }
    
    private void OnDisable()
    {
        _health.HealthChanged += OnHurt;
    }

    private void OnHurt(float currentHealth)
    {
        if (currentHealth <= 0)
        {
            _healthAnimator.TriggerDie();
            return;
        }
        
        _healthAnimator.TriggerHurt();
    }
}