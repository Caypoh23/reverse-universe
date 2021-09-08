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

            IsAbilityDone = false;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

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