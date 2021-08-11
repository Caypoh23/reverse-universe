﻿using Player.Data;
using Player.PlayerFiniteStateMachine;

namespace Player.SuperStates
{
    public class PlayerTouchingWallState : PlayerState
    {
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        protected bool JumpInput;
        protected int XInput;

        public PlayerTouchingWallState(PlayerBase playerBase, PlayerStateMachine stateMachine,
            PlayerData playerData, string animBoolName) : base(playerBase, stateMachine, playerData, animBoolName)
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

            XInput = PlayerBase.InputHandler.NormalizedInputX;
            JumpInput = PlayerBase.InputHandler.JumpInput;

            if (JumpInput)
            {
                PlayerBase.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
                StateMachine.ChangeState(PlayerBase.WallJumpState);
            }
            else if (IsGrounded)
            {
                StateMachine.ChangeState(PlayerBase.IdleState);
            }
            else if (!IsTouchingWall || XInput != Core.Movement.FacingDirection && Core.Movement.CurrentVelocity.y <= 0)
            {
                StateMachine.ChangeState(PlayerBase.InAirState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsGrounded = Core.CollisionSenses.Ground;
            IsTouchingWall = Core.CollisionSenses.WallFront;
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();
        }
    }
}