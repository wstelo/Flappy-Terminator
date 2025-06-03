using UnityEngine;

[RequireComponent (typeof(Jumper), typeof(Attacker))]

public class Player : Character
{
    [SerializeField] private InputHandler _inputHandler;
    
    private Jumper _jumper;
    private Attacker _attacker;

    public override void Awake()
    {
        base.Awake();
        _jumper = GetComponent<Jumper>();
        _attacker = GetComponent<Attacker>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        _inputHandler.JumpButtonClicked += Jump;
        _inputHandler.AttackButtonClicked += Attack;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _inputHandler.JumpButtonClicked -= Jump;
        _inputHandler.AttackButtonClicked -= Attack;
    }

    private void Attack()
    {
        _attacker.StartAttackAnimation();
    }

    private void Jump()
    {
        _jumper.Jump();
    }
}
