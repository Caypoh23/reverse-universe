using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class DodgeState : State
    {
        protected DodgeStateData StateData;
        
        protected bool PerformCloseRangeAction;
        protected bool IsPlayerInMaxAgroRange;
        protected bool IsGrounded;
        protected bool IsDodgeOver;
    
        public DodgeState(
            Entity entity, 
            FiniteStateMachine stateMachine, 
            string animBoolName,
            DodgeStateData stateData) :
            base(
                entity, 
                stateMachine, 
                animBoolName)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();

            IsDodgeOver = false;
            
            Core.Movement.SetVelocity(StateData.dodgeSpeed, StateData.dodgeAngle, -Core.Movement.FacingDirection);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time >= StartTime + StateData.dodgeTime && IsGrounded && !Core.Movement.IsRewinding)
            {
                IsDodgeOver = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
            IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
            IsGrounded = Core.CollisionSenses.IsGrounded;

        }
    }
}
