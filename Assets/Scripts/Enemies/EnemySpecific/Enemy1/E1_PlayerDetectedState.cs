using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_PlayerDetectedState : PlayerDetectedState
    {
        private readonly Enemy1 _enemy;

        public E1_PlayerDetectedState(
            Entity entity,
            FiniteStateMachine stateMachine,
            string animBoolName,
            PlayerDetectedData stateData,
            Enemy1 enemy) :
            base(
                entity,
                stateMachine,
                animBoolName,
                stateData)
        {
            _enemy = enemy;
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

            if (PerformCloseRangeAction && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (PerformLongRangeAction && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.ChargeState);
            }
            else if (!IsPlayerInMaxAgroRange && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
            else if (!IsDetectingLedge && !Core.Movement.IsRewinding)
            {
                Core.Movement.Flip();
                StateMachine.ChangeState(_enemy.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}