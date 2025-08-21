using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float MinHealth = 0f;

    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    public event Action<float> HealthChanged;
    public event Action<float> Damaged;

    public float MaxHealth { get; private set; }

    private void Awake()
    {
        MaxHealth = _maxHealth;
        _currentHealth = _maxHealth;
    }

    private float ChangeHealth(float value, string valueName, char mathOperator)
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

        var clampedHealth = Mathf.Clamp(newHealth, MinHealth, MaxHealth);
        _currentHealth = clampedHealth;
        HealthChanged?.Invoke(clampedHealth);

        return clampedHealth;
    }

    public void DealDamage(float damage)
    {
        var currentHealth = ChangeHealth(damage, "Damage", '-');
        Damaged?.Invoke(currentHealth);
    }

    public void Heal(float heal)
    {
        ChangeHealth(heal, "Heal", '+');
    }
}