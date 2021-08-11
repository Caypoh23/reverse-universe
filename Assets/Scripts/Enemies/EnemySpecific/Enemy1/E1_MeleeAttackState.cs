using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.EnemySpecific.Enemy1
{
    public class E1_MeleeAttackState : MeleeAttackState
    {
        private readonly Enemy1 _enemy;

        public E1_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
            Transform attackPosition, D_MeleeAttack stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName,
            attackPosition,
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

            if (IsAnimationFinished)
            {
                if (IsPlayerMinAgroRange)
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

        public override void TriggerAttack()
        {
            base.TriggerAttack();
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}