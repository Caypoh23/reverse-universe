using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class ChargeState : State
    {
        protected readonly ChargeStateData StateData;

        protected bool IsPlayerInMinAgroRange;
        protected bool IsDetectingLedge;
        protected bool IsDetectingWall;
        protected bool IsChargeTimeOver;
        protected bool PerformCloseRangeAction;

        protected bool IsInTouchingRange;


        public ChargeState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            ChargeStateData stateData) :
            base(
                entity,
                stateMachine,
                animBoolId)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            IsChargeTimeOver = false;
            Core.Movement.SetVelocityX(StateData.chargeSpeed * Core.Movement.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(StateData.chargeSpeed * Core.Movement.FacingDirection);

            if (Time.time >= StartTime + StateData.chargeTime)
            {
                IsChargeTimeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();

            IsDetectingLedge = Core.CollisionSenses.IsCheckingLedge;
            IsDetectingWall = Core.CollisionSenses.IsCheckingWall;

            PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();

            IsInTouchingRange = Entity.CheckPlayerInTouchingRangeAction();
            
        }
    }
}