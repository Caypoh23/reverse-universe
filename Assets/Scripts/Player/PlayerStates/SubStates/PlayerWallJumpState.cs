using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;
using UnityEngine;

namespace Player.PlayerStates.SubStates
{
    public class PlayerWallJumpState : PlayerAbilityState
    {
        private int _wallJumpDirection;
        private readonly int _yVelocity = Animator.StringToHash("yVelocity");
        private readonly int _xVelocity = Animator.StringToHash("xVelocity");

        public PlayerWallJumpState(PlayerBase playerBase, PlayerStateMachine stateMachine,
            PlayerData playerData, string animBoolName) : base(playerBase, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerBase.InputHandler.UseJumpInput();
            PlayerBase.JumpState.ResetAmountOfJumpsLeft();
            Core.Movement.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
            Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
            PlayerBase.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            PlayerBase.Anim.SetFloat(_yVelocity, Core.Movement.CurrentVelocity.y);
            PlayerBase.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

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
}