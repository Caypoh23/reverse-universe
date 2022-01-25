using Enemies.StateMachine;
using UnityEngine;

namespace Enemies.States
{
    public class AttackState : State
    {
        protected readonly Transform AttackPosition;

        protected bool IsAnimationFinished;
        protected bool IsPlayerMinAgroRange;

        public AttackState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            Transform attackPosition
        ) : base(entity, stateMachine, animBoolId)
        {
            AttackPosition = attackPosition;
        }

        public override void Enter()
        {
            base.Enter();

            if (!Core.Movement.IsRewinding)
            {
                Entity.AnimationToState.AttackState = this;
                IsAnimationFinished = false;
                Core.Movement.SetVelocityX(0f);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!Core.Movement.IsRewinding)
            {
                Core.Movement.SetVelocityX(0f);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsPlayerMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        }

        public virtual void TriggerAttack() { }

        public virtual void FinishAttack()
        {
            IsAnimationFinished = true;
        }
    }
}
