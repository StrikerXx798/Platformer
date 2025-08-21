using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillBarSmooth : SkillBar
{
    private const float MinFillAmount = 0f;
    private const float DefaultElapsedTime = 0f;
    private const float MaxSkillView = 1f;

    [SerializeField] private VampireSkill _vampireSkill;
    [SerializeField] private Image _fillBar;

    private Coroutine _currentChangeCoroutine;

    private void OnEnable()
    {
        if (_vampireSkill != null)
        {
            _vampireSkill.OnSkillActivated += OnSkillActivated;
            _vampireSkill.OnSkillDeactivated += OnSkillDeactivated;
            _vampireSkill.OnSkillCooldownStarted += OnSkillCooldownStarted;
            _vampireSkill.OnSkillReady += OnSkillReady;
        }
    }

    private void OnDisable()
    {
        if (_vampireSkill != null)
        {
            _vampireSkill.OnSkillActivated -= OnSkillActivated;
            _vampireSkill.OnSkillDeactivated -= OnSkillDeactivated;
            _vampireSkill.OnSkillCooldownStarted -= OnSkillCooldownStarted;
            _vampireSkill.OnSkillReady -= OnSkillReady;
        }
    }

    private void OnSkillActivated(float duration)
    {
        ChangeView(MinFillAmount, duration);
    }

    private void OnSkillDeactivated()
    {
        _fillBar.fillAmount = MinFillAmount;
    }

    private void OnSkillCooldownStarted(float duration)
    {
        ChangeView(MaxSkillView, duration);
    }

    private void OnSkillReady()
    {
        if (_currentChangeCoroutine != null)
        {
            StopCoroutine(_currentChangeCoroutine);
        }

        _fillBar.fillAmount = MaxSkillView;
    }

    private IEnumerator ChangeToSmooth(float targetFillAmount, float maxDuration, float startFillAmount)
    {
        var elapsedTime = DefaultElapsedTime;
        var duration = maxDuration;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            _fillBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);

            yield return null;
        }

        _fillBar.fillAmount = targetFillAmount;
    }

    protected override void ChangeView(float currentValue, float maxValue)
    {
        if (_currentChangeCoroutine != null)
        {
            StopCoroutine(_currentChangeCoroutine);
        }

        var startFill = _fillBar.fillAmount;
        var endFill = currentValue / maxValue;

        if (Mathf.Approximately(currentValue, MinFillAmount))
        {
            startFill = MaxSkillView;
            endFill = MinFillAmount;
        }
        else if (Mathf.Approximately(currentValue, MaxSkillView))
        {
            startFill = MinFillAmount;
            endFill = MaxSkillView;
        }

        _currentChangeCoroutine = StartCoroutine(ChangeToSmooth(endFill, maxValue, startFill));
    }
}