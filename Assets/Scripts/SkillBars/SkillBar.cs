using UnityEngine;

public abstract class SkillBar : MonoBehaviour
{
    protected abstract void ChangeView(float currentValue, float maxValue);
}