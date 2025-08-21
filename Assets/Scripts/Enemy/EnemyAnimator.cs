using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static class Params
    {
        internal static readonly int XVelocity = Animator.StringToHash(nameof(XVelocity));
        internal static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    }

    public void SetXVelocity(float value)
    {
        _animator.SetFloat(Params.XVelocity, value);
    }
    
    public void TriggerAttack()
    {
        _animator.SetTrigger(Params.IsAttack);
    }
}