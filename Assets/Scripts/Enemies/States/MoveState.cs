using Enemies.StateMachine;
using Enemies.States.Data;

namespace Enemies.States
{
    public class MoveState : State
    {
        protected readonly MoveStateData StateData;

        protected bool IsDetectingWall;
        protected bool IsDetectingLedge;
        protected bool IsPlayerInMinAgroRange;
        protected bool IsInTouchingRange;

        public MoveState(
            Entity entity,
            FiniteStateMachine stateMachine,
            int animBoolId,
            MoveStateData stateData
        ) : base(entity, stateMachine, animBoolId)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            if (!Core.Movement.IsRewinding)
                Core.Movement.SetVelocityX(StateData.movementSpeed * Core.Movement.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if(!Core.Movement.IsRewinding)
                Core.Movement.SetVelocityX(StateData.movementSpeed * Core.Movement.FacingDirection);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsDetectingLedge = Core.CollisionSenses.IsCheckingLedge;
            IsDetectingWall = Core.CollisionSenses.IsCheckingWall;

            IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();

            IsInTouchingRange = Entity.CheckPlayerInTouchingRangeAction();
        }
    }
}
