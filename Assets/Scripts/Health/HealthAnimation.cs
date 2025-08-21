using UnityEngine;

public class HealthAnimation : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private HealthAnimator _healthAnimator;

    private void OnEnable()
    {
        _health.Damaged += OnHurt;
    }
    
    private void OnDisable()
    {
        _health.Damaged -= OnHurt;
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