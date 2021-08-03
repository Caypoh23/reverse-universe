using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int _wallJumpDirection;
    private readonly int _yVelocity = Animator.StringToHash("yVelocity");
    private readonly int _xVelocity = Animator.StringToHash("xVelocity");

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.InputHandler.UseJumpInput();
        Player.JumpState.ResetAmountOfJumpsLeft();
        Core.Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
        Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
        Player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Player.Anim.SetFloat(_yVelocity, Core.Movement.CurrentVelocity.y);
        Player.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

        if (Time.time >= StartTime + PlayerData.wallJumpTime)
        {
            IsAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -Core.Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = Core.Movement.FacingDirection;
        }
    }
}