using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_MoveState : MoveState
    {
        private readonly Enemy1 _enemy;

        public E1_MoveState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            MoveStateData stateData,
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

            Entity.TimeIsRewinding();
            
            if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (IsDetectingWall || !IsDetectingLedge)
            {
                _enemy.IdleState.SetFlipAfterIdle(true);
                StateMachine.ChangeState(_enemy.IdleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
