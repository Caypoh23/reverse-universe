using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class StunState : State
    {
        protected readonly D_StunState StateData;

        protected bool IsStunTimeOver;
        protected bool IsGrounded;
        protected bool IsMovementStopped;
        protected bool PerformCloseRangeAction;
        protected bool IsPLayerInMinAgroRange;

        public StunState(Entity entity, FiniteStateMachine stateMachine,
            string animBoolName, D_StunState stateData) : base(entity, stateMachine,
            animBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsStunTimeOver = false;
            IsMovementStopped = false;
            Core.Movement.SetVelocity(StateData.stunKnockbackSpeed, StateData.stunKnockbackAngle,
                Entity.LastDamageDirection);
        }

        public override void Exit()
        {
            base.Exit();

            Entity.ResetStunResistance();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData.stunTime)
            {
                IsStunTimeOver = true;
            }

            if (IsGrounded && Time.time >= StartTime + StateData.stunKnockbackTime && !IsMovementStopped)
            {
                IsMovementStopped = true;
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

            IsGrounded = Core.CollisionSenses.Ground;

            PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
            IsPLayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        }
    }
}