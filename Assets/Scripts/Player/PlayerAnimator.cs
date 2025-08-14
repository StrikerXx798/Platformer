using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private static class Params
    {
        public static readonly int XVelocity = Animator.StringToHash(nameof(XVelocity));
        public static readonly int YVelocity = Animator.StringToHash(nameof(YVelocity));
        public static readonly int IsGrounded = Animator.StringToHash(nameof(IsGrounded));
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
}