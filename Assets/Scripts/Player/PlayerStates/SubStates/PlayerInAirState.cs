using Player.Data;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace Player.PlayerStates.SubStates
{
    public class PlayerInAirState : PlayerState
    {
        // Input
        private int _xInput;
        private bool _jumpInput;
        private bool _jumpInputStop;
        private bool _dashInput;
        private bool _timeDilationInput;

        // Checks
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _coyoteTime;
        private bool _isJumping;

        private readonly int _yVelocity = Animator.StringToHash("yVelocity");
        private readonly int _xVelocity = Animator.StringToHash("xVelocity");

        public PlayerInAirState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName) :
            base(
                playerBase,
                stateMachine,
                playerData,
                animBoolName)
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

            CheckCoyoteTime();

            _xInput = PlayerBase.InputHandler.NormalizedInputX;
            _jumpInput = PlayerBase.InputHandler.CanJumpInput;
            _jumpInputStop = PlayerBase.InputHandler.CanJumpInputStop;
            _dashInput = PlayerBase.InputHandler.CanDashInput;
            _timeDilationInput = PlayerBase.InputHandler.CanDelayTimeInput;

            CheckJumpMultiplier();

            if (PlayerBase.InputHandler.AttackInputs[(int) CombatInputs.Primary])
            {
                StateMachine.ChangeState(PlayerBase.PrimaryAttackState);
            }
            else if (PlayerBase.InputHandler.AttackInputs[(int) CombatInputs.Secondary])
            {
                StateMachine.ChangeState(PlayerBase.SecondaryAttackState);
            }
            else if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(PlayerBase.LandState);
            }
            else if (_jumpInput && PlayerBase.WallSlideState.IsWallSliding)
            {
                PlayerBase.InputHandler.UseJumpInput();
                _isTouchingWall = Core.CollisionSenses.IsCheckingWall;
                PlayerBase.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                StateMachine.ChangeState(PlayerBase.WallJumpState);
            }
            else if (_jumpInput && PlayerBase.JumpState.CanJump())
            {
                PlayerBase.InputHandler.UseJumpInput();
                StateMachine.ChangeState(PlayerBase.JumpState);
            }
            else if (_isTouchingWall && _xInput == Core.Movement.FacingDirection && PlayerBase.Rb.velocity.y < 0)
            {
                StateMachine.ChangeState(PlayerBase.WallSlideState);
            }
            else if (_dashInput && PlayerBase.DashState.CheckIfCanDash() && !_isTouchingWall)
            {
                StateMachine.ChangeState(PlayerBase.DashState);
            }
            else
            {
                Core.Movement.CheckIfShouldFlip(_xInput);
                Core.Movement.SetVelocityX(PlayerData.movementVelocity * _xInput);

                PlayerBase.Anim.SetFloat(_yVelocity, Core.Movement.CurrentVelocity.y);
                PlayerBase.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));
            }
        }

        private void CheckJumpMultiplier()
        {
            if (_isJumping)
            {
                if (_jumpInputStop)
                {
                    Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.jumpHeightMultiplier);
                    _isJumping = false;
                }
                else if (Core.Movement.CurrentVelocity.y <= 0f)
                {
                    _isJumping = false;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public void CheckCoyoteTime()
        {
            if (_coyoteTime && Time.time > StartTime + PlayerData.coyoteTime)
            {
                _coyoteTime = false;
                PlayerBase.JumpState.DecreaseAmountOfJumpsLeft();
            }
        }

        public void StartCoyoteTime() => _coyoteTime = true;

        public void SetIsJumping() => _isJumping = true;


        public override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.IsGrounded;
            _isTouchingWall = Core.CollisionSenses.IsCheckingWall;
        }
    }
}