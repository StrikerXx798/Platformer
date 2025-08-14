using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    public static readonly int XVelocity = Animator.StringToHash(nameof(XVelocity));

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetXVelocity(float value)
    {
        _animator.SetFloat(XVelocity, value);
    }
}