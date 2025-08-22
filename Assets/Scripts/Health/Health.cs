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

    public float DealDamage(float damage)
    {
        if (damage < 0)
        {
            throw new ArgumentException("Damage value can't be negative");
        }

        var previousHealth = _currentHealth;
        _currentHealth -= damage;
        var clampedHealth = Mathf.Clamp(_currentHealth, MinHealth, MaxHealth);
        _currentHealth = clampedHealth;
        HealthChanged?.Invoke(clampedHealth);
        Damaged?.Invoke(clampedHealth);

        return previousHealth - _currentHealth;
    }

    public void Heal(float heal)
    {
        if (heal < 0)
        {
            throw new ArgumentException("Heal value can't be negative");
        }

        _currentHealth += heal;
        var clampedHealth = Mathf.Clamp(_currentHealth, MinHealth, MaxHealth);
        _currentHealth = clampedHealth;
        HealthChanged?.Invoke(clampedHealth);
    }
}