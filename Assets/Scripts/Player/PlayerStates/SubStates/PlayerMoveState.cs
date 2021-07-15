using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player playerScript, PlayerStateMachine stateMachine, PlayerData playerData,
        string animBoolName) : base(playerScript, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        PlayerScript.CheckIfShouldFlip(XInput);
        PlayerScript.SetVelocityX(PlayerData.movementVelocity * XInput);

        if (XInput == 0)
        {
            StateMachine.ChangeState(PlayerScript.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}