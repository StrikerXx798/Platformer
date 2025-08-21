using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private static class Params
    {
        internal static readonly int XVelocity = Animator.StringToHash(nameof(XVelocity));
        internal static readonly int YVelocity = Animator.StringToHash(nameof(YVelocity));
        internal static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
        internal static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
    }
    public void SetXVelocity(float value)
    {
        _animator.SetFloat(Params.XVelocity, value);
    }

    public void SetYVelocity(float value)
    {
        _animator.SetFloat(Params.YVelocity, value);
    }

    public void SetIsGrounded(bool value)
    {
        _animator.SetBool(Params.IsGrounded, value);
    }

    public void TriggerAttack()
    {
        _animator.SetTrigger(Params.IsAttack);
    }
}