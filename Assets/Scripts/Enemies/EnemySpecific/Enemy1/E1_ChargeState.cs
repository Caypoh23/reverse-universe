using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_ChargeState : ChargeState
    {
        private readonly Enemy1 _enemy;

        public E1_ChargeState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            ChargeStateData stateData,
            Enemy1 enemy) :
            base(
                entity,
                stateMachine,
                animBoolId,
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

            if(Core.Movement.IsRewinding) return;

            if (PerformCloseRangeAction)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (!IsDetectingLedge || IsDetectingWall)
            {
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
            else if (IsChargeTimeOver)
            {
                if (IsPlayerInMinAgroRange)
                {
                    StateMachine.ChangeState(_enemy.PlayerDetectedState);
                }
                else
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