using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon _weapon;

    private int _xInput;

    private float _velocityToSet;
    private bool _setVelocity;
    private bool _shouldCheckFlip;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _setVelocity = false;

        _weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        _weapon.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _xInput = Player.InputHandler.NormalizedInputX;

        if (_shouldCheckFlip)
        {
            Player.CheckIfShouldFlip(_xInput);
        }

        if (_setVelocity)
        {
            Player.SetVelocityX(_velocityToSet * Player.FacingDirection);
        }
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;

        weapon.InitializeWeapon(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        Player.SetVelocityX(velocity * Player.FacingDirection);

        _velocityToSet = velocity;
        _setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        _shouldCheckFlip = value;
    }


    #region Animation Triggers

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        IsAbilityDone = true;
    }

    #endregion
}