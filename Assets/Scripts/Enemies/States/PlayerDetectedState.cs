using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class PlayerDetectedState : State
    {
        protected readonly PlayerDetectedData StateData;

        protected bool IsPlayerInMinAgroRange;
        protected bool IsPlayerInMaxAgroRange;
        protected bool PerformLongRangeAction;
        protected bool PerformCloseRangeAction;
        protected bool IsDetectingLedge;

        public PlayerDetectedState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            PlayerDetectedData stateData
        ) : base(entity, stateMachine, animBoolId)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            PerformLongRangeAction = false;

            if (!Core.Movement.IsRewinding)
            {
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

                if (Time.time >= StartTime + StateData.longRangeActionTime)
                {
                    PerformLongRangeAction = true;
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

            IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
            IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
            IsDetectingLedge = Core.CollisionSenses.IsCheckingLedge;

            PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
        }
    }
}
