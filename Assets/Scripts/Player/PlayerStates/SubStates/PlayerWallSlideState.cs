using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public bool IsWallSliding;
    
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            IsWallSliding = true;
            Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);
        }
    }

    public override void Exit()
    {
        base.Exit();

        IsWallSliding = false;
    }
}