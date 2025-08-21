using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarSmooth : HealthBar
{
    private const float DefaultElapsedTime = 0f;
    private const float MaxHealthView = 1f;

    [SerializeField] private Image _fillBar;
    [SerializeField] private float _changeSpeed;

    private Coroutine _currentChangeCoroutine;

    private void Start()
    {
        _fillBar.fillAmount = MaxHealthView;
    }

    private IEnumerator ChangeToSmooth(float targetFillAmount)
    {
        var startFillAmount = _fillBar.fillAmount;
        var elapsedTime = DefaultElapsedTime;
        var duration = Mathf.Abs(targetFillAmount - startFillAmount) / _changeSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            var t = Mathf.Clamp01(elapsedTime / duration);
            _fillBar.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);

            yield return null;
        }

        _fillBar.fillAmount = targetFillAmount;
    }

    protected override void ChangeView(float currentHealth)
    {
        var healthPercentage = currentHealth / Health.MaxHealth;

        if (_currentChangeCoroutine != null)
        {
            StopCoroutine(_currentChangeCoroutine);
        }

        _currentChangeCoroutine = StartCoroutine(ChangeToSmooth(healthPercentage));
    }
}