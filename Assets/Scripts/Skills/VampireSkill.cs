using UnityEngine;
using System;
using System.Collections.Generic;

public class VampireSkill : MonoBehaviour
{
    private const float MinTime = 0f;

    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;
    [SerializeField] private float _drainRadius = 5f;
    [SerializeField] private float _drainDamage = 10f;
    [SerializeField] private Health _playerHealth;

    public event Action<float> OnSkillActivated;
    public event Action OnSkillDeactivated;
    public event Action<float> OnSkillCooldownStarted;
    public event Action OnSkillReady;

    private bool _isActive = false;
    private bool _isCoolingDown = false;
    private float _activeTimeRemaining = 0f;
    private float _cooldownTimeRemaining = 0f;
    private List<Health> _affectedEnemies = new List<Health>();

    private void Update()
    {
        if (_isActive)
        {
            _activeTimeRemaining -= Time.deltaTime;
            DrainHealthFromAffectedEnemies();

            if (_activeTimeRemaining <= MinTime)
            {
                DeactivateAbility();
            }
        }
        else if (_isCoolingDown)
        {
            _cooldownTimeRemaining -= Time.deltaTime;

            if (_cooldownTimeRemaining <= MinTime)
            {
                _isCoolingDown = false;
                OnSkillReady?.Invoke();
            }
        }
    }

    private void DeactivateAbility()
    {
        _isActive = false;
        _isCoolingDown = true;
        _cooldownTimeRemaining = _cooldownDuration;
        OnSkillDeactivated?.Invoke();
        OnSkillCooldownStarted?.Invoke(_cooldownDuration);
        _affectedEnemies.Clear();
    }

    private void DrainHealthFromAffectedEnemies()
    {
        foreach (var enemyHealth in _affectedEnemies)
        {
            if (enemyHealth is not null)
            {
                enemyHealth.DealDamage(_drainDamage * Time.deltaTime);
                _playerHealth.Heal(_drainDamage * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_isActive)
            Gizmos.color = Color.red;
        else if (_isCoolingDown)
            Gizmos.color = Color.blue;
        else
            Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, _drainRadius);
    }

    public void ActivateAbility()
    {
        if (_isActive || _isCoolingDown) return;

        _playerAnimator.TriggerSkill();
        _isActive = true;
        _activeTimeRemaining = _abilityDuration;
        OnSkillActivated?.Invoke(_abilityDuration);

        var hitEnemies = Physics2D.OverlapCircleAll(transform.position, _drainRadius);
        _affectedEnemies.Clear();

        foreach (var enemyCollider in hitEnemies)
        {
            if (enemyCollider.TryGetComponent(out Enemy enemy))
            {
                if (enemy.TryGetComponent(out Health enemyHealth))
                {
                    _affectedEnemies.Add(enemyHealth);
                }
            }
        }
    }
}