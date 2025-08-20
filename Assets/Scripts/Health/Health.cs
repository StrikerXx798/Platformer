using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float _maxHealth;

    public event Action DamageTaken;

    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        DamageTaken?.Invoke();
    }

    public void Heal(float heal)
    {
        var newHealth = CurrentHealth + heal;
        CurrentHealth = newHealth >= _maxHealth ? _maxHealth : newHealth;
    }
}