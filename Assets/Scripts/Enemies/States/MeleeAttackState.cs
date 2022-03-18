using Enemies.StateMachine;
using Enemies.States.Data;
using Interfaces;
using UnityEngine;

namespace Enemies.States
{
    public class MeleeAttackState : AttackState
    {
        protected readonly MeleeAttackData StateData;

        protected readonly MinotaurStompStateData MinotaurStateData;

        public MeleeAttackState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            Transform attackPosition,
            MeleeAttackData stateData
        ) : base(entity, stateMachine, animBoolId, attackPosition)
        {
            StateData = stateData;
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

            var detectedObjects = Physics2D.OverlapCircleAll(
                AttackPosition.position,
                StateData.attackRadius,
                StateData.whatIsPlayer
            );

            foreach (var collider in detectedObjects)
            {
                var hitInfo = collider.GetComponent<ITakeDamage>();
                var knockbackable = collider.GetComponent<IKnockbackable>();

                hitInfo?.TakeDamage(StateData.attackDamage);

                knockbackable?.Knockback(
                    StateData.knockbackAngle,
                    StateData.knockbackStrength,
                    Core.Movement.FacingDirection
                );
            }
        }

        public override void FinishAttack()
        {
            base.FinishAttack();
        }
    }
}
