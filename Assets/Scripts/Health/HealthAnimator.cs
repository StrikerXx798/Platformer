using UnityEngine;

public class HealthAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static class Params
    {
        internal static readonly int IsHurt = Animator.StringToHash(nameof(IsHurt));
        internal static readonly int IsDie = Animator.StringToHash(nameof(IsDie));
    }

    public void TriggerDie()
    {
        _animator.SetTrigger(Params.IsDie);
    }

    public void TriggerHurt()
    {
        _animator.SetTrigger(Params.IsHurt);
    }

    private void DestroyAfterDie()
    {
        Destroy(transform.parent.gameObject);
    }
}