using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class ChargeState : State
    {
        protected readonly D_ChargeState StateData;

        protected bool IsPlayerInMinAgroRange;
        protected bool IsDetectingLedge;
        protected bool IsDetectingWall;
        protected bool IsChargeTimeOver;
        protected bool PerformCloseRangeAction;

        public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
            D_ChargeState stateData) :
            base(entity, stateMachine,
                animBoolName)
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

            IsDetectingLedge = Core.CollisionSenses.Ledge;
            IsDetectingWall = Core.CollisionSenses.WallFront;

            PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
        }
    }
}