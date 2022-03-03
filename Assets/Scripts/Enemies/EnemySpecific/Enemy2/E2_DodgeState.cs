using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy2
{
    public class E2_DodgeState : DodgeState
    {
        private readonly Enemy2 _enemy;

        public E2_DodgeState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            DodgeStateData stateData,
            Enemy2 enemy
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

            if (IsDodgeOver && !Core.Movement.IsRewinding)
            {
                
                if (Core.Stats.CurrentHealthAmount <= 0)
                {
                    StateMachine.ChangeState(_enemy.DeadState);
                }
                else if (IsPlayerInMaxAgroRange && PerformCloseRangeAction)
                {
                    StateMachine.ChangeState(_enemy.MeleeAttackState);
                }
                else if (IsPlayerInMaxAgroRange && !PerformCloseRangeAction)
                {
                    StateMachine.ChangeState(_enemy.RangedAttackState);
                }
                else if (!IsPlayerInMaxAgroRange)
                {
                    StateMachine.ChangeState(_enemy.LookForPlayerState);
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
        }
    }
}
