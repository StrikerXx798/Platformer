using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSkill : MonoBehaviour
{
    private const float MinTime = 0f;

    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private float _drainRadius = 5f;
    [SerializeField] private float _drainDamage = 10f;
    [SerializeField] private Health _playerHealth;
    [SerializeField] private SkillEffect _skillEffect;

    public event Action<float> OnSkillActivated;
    public event Action OnSkillDeactivated;
    public event Action<float> OnSkillCooldownStarted;
    public event Action OnSkillReady;

    private bool _isActive = false;
    private bool _isCoolingDown = false;
    private float _activeTimeRemaining = 0f;
    private float _cooldownTimeRemaining = 0f;
    private readonly List<Health> _affectedEnemies = new List<Health>();

    private void DeactivateAbility()
    {
        _isActive = false;
        OnSkillDeactivated?.Invoke();
        _affectedEnemies.Clear();
        _skillEffect.StopEffect();
        StartCoroutine(SkillCooldownCoroutine());
    }

    private void DrainHealthFromAffectedEnemies()
    {
        foreach (var enemyHealth in _affectedEnemies)
        {
            if (enemyHealth is not null)
            {
                float actualDamageDealt = enemyHealth.DealDamage(_drainDamage * Time.deltaTime);
                _playerHealth.Heal(actualDamageDealt);
            }
        }
    }

    private IEnumerator SkillActiveCoroutine()
    {
        _isActive = true;
        _activeTimeRemaining = _abilityDuration;
        OnSkillActivated?.Invoke(_abilityDuration);

        while (_activeTimeRemaining > MinTime)
        {
            _activeTimeRemaining -= Time.deltaTime;
            DrainHealthFromAffectedEnemies();

            yield return null;
        }

        DeactivateAbility();
    }

    private IEnumerator SkillCooldownCoroutine()
    {
        _isCoolingDown = true;
        _cooldownTimeRemaining = _cooldownDuration;
        OnSkillCooldownStarted?.Invoke(_cooldownDuration);

        while (_cooldownTimeRemaining > MinTime)
        {
            _cooldownTimeRemaining -= Time.deltaTime;

            yield return null;
        }

        _isCoolingDown = false;
        OnSkillReady?.Invoke();
    }

    private Health FindNearestEnemy()
    {
        Health nearestEnemyHealth = null;
        var minDistance = float.MaxValue;

        var hitColliders = Physics2D.OverlapCircleAll(transform.position, _drainRadius);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Health enemyHealth))
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestEnemyHealth = enemyHealth;
                    }
                }
            }
        }

        return nearestEnemyHealth;
    }

    public void ActivateAbility()
    {
        if (_isActive || _isCoolingDown) return;

        _playerAnimator.TriggerSkill();
        _skillEffect.PlayEffect();
        _affectedEnemies.Clear();
        var nearestEnemyHealth = FindNearestEnemy();

        if (nearestEnemyHealth is not null)
        {
            _affectedEnemies.Add(nearestEnemyHealth);
        }

        StartCoroutine(SkillActiveCoroutine());
    }
}