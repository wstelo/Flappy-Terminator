using System;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;

    public event Action AttackEnded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartFlyAnimation()
    {
        _animator.SetBool(AnimationData.IsFly, true);
    }

    public void StopFlyAnimation()
    {
        _animator.SetBool(AnimationData.IsFly, false);
    }

    public void StartAttackAnimation()
    {
        _animator.SetBool(AnimationData.IsAttack, true);
    }

    public void StopAttackAnimation()
    {
        AttackEnded?.Invoke();
        _animator.SetBool(AnimationData.IsAttack, false);
    }
}
