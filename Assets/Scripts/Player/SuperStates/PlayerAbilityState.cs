using Player.Data;
using Player.PlayerFiniteStateMachine;

namespace Player.SuperStates
{
    public class PlayerAbilityState : PlayerState
    {
        protected bool IsAbilityDone;
        private bool _isGrounded;

        public PlayerAbilityState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            int animBoolId
        ) : base(playerBase, stateMachine, playerData, animBoolId) { }

        public override void Enter()
        {
            base.Enter();

            IsAbilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Core.Movement.IsRewinding)
            {
                return;
            }

            if (Core.Movement.RewindingTimeIsFinished)
            {
                IsAbilityDone = true;
            }

            if (IsAbilityDone)
            {
                if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
                {
                    StateMachine.ChangeState(PlayerBase.IdleState);
                }
                else
                {
                    StateMachine.ChangeState(PlayerBase.InAirState);
                }
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
