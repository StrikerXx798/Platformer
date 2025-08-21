using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float MinHealth = 0f;

    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    public event Action<float> HealthChanged;

    public float MaxHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        _currentHealth = _maxHealth;
    }

    private void ChangeHealth(float value, string valueName, char mathOperator)
    {
        if (value < 0)
        {
            throw new ArgumentException($"{valueName} value can't be negative");
        }

        var newHealth = mathOperator switch
        {
            '-' => _currentHealth -= Mathf.Abs(value),
            '+' => _currentHealth += value,
            _ => throw new ArgumentException($"Unknown math operator {mathOperator}")
        };

        _currentHealth = Mathf.Clamp(newHealth, MinHealth, MaxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void DealDamage(float damage)
    {
        ChangeHealth(damage, "Damage", '-');
    }

    public void Heal(float heal)
    {
        ChangeHealth(heal, "Heal", '+');
    }
}