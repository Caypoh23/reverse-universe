﻿using System.Collections;
using System.Collections.Generic;
using Unity.XR.GoogleVr;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;

    private bool _jumpInput;
    private bool _isGrounded;
    private bool _dashInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Player.DashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = Player.InputHandler.NormalizedInputX;
        _jumpInput = Player.InputHandler.JumpInput;
        _dashInput = Player.InputHandler.DashInput;


        if (Player.InputHandler.AttackInputs[(int) CombatInputs.Primary])
        {
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }
        else if (Player.InputHandler.AttackInputs[(int) CombatInputs.Secondary])
        {
            StateMachine.ChangeState(Player.SecondaryAttackState);
        }
        else if (_jumpInput && Player.JumpState.CanJump())
        {
            StateMachine.ChangeState(Player.JumpState);
        }
        else if (!_isGrounded)
        {
            Player.InAirState.StartCoyoteTime();
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (_dashInput && Player.DashState.CheckIfCanDash())
        {
            StateMachine.ChangeState(Player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        _isGrounded = Core.CollisionSenses.Ground;
    }
}