using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private AnimatorController _animatorController;

    public event Action Attacked;

    private void OnEnable()
    {
        _animatorController.AttackEnded += Attack;
    }

    private void OnDisable()
    {
        _animatorController.AttackEnded -= Attack;
    }

    public void StartAttackAnimation()
    {
        _animatorController.StartAttackAnimation();
    }

    public void Attack()
    {
        Attacked?.Invoke();
    }
}
