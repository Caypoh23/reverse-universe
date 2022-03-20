using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_IdleState : IdleState
    {
        private readonly Enemy1 _enemy;

        public E1_IdleState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            IdleStateData stateData,
            Enemy1 enemy
        ) : base(entity, stateMachine, animBoolId, stateData)
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
            
            if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (IsIdleTimeOver)
            {
                StateMachine.ChangeState(_enemy.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
