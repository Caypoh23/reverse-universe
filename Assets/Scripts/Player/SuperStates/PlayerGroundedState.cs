using Player.Data;
using Player.Input;
using Player.PlayerFiniteStateMachine;

namespace Player.SuperStates
{
    public class PlayerGroundedState : PlayerState
    {
        protected int XInput;

        private bool _jumpInput;
        private bool _isGrounded;
        private bool _dashInput;
        private bool _timeDilationInput;

        public PlayerGroundedState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            int animBoolId) :
            base(
                playerBase,
                stateMachine,
                playerData,
                animBoolId)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerBase.JumpState.ResetAmountOfJumpsLeft();
            PlayerBase.DashState.ResetCanDash();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            XInput = PlayerBase.InputHandler.NormalizedInputX;
            _jumpInput = PlayerBase.InputHandler.CanJumpInput;
            _dashInput = PlayerBase.InputHandler.CanDashInput;
            _timeDilationInput = PlayerBase.InputHandler.CanDelayTimeInput;
            
            if (Core.Movement.IsRewinding)
                return;

            if (PlayerBase.InputHandler.AttackInputs[(int) CombatInputs.Primary])
            {
                StateMachine.ChangeState(PlayerBase.PrimaryAttackState);
            }
            else if (_jumpInput && PlayerBase.JumpState.CanJump())
            {
                StateMachine.ChangeState(PlayerBase.JumpState);
            }
            else if (!_isGrounded)
            {
                PlayerBase.InAirState.StartCoyoteTime();
                StateMachine.ChangeState(PlayerBase.InAirState);
            }
            else if (_dashInput && PlayerBase.DashState.CheckIfCanDash())
            {
                StateMachine.ChangeState(PlayerBase.DashState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.IsGrounded;
        }
    }
}