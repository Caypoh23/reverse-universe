using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_LookForPlayerState : LookForPlayerState
    {
        private readonly Enemy1 _enemy;

        public E1_LookForPlayerState(
            Entity entity,
            FiniteStateMachine stateMachine,
            string animBoolName,
            LookForPlayerData stateData,
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

            if (IsPlayerIsInMinAgroRange && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (IsAllTurnsTimeDone && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.MoveState);
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