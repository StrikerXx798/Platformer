using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health Health => _health;

    private void OnEnable()
    {
        _health.HealthChanged += ChangeView;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ChangeView;
    }

    protected virtual void ChangeView(float currentHealth)
    {
    }
}