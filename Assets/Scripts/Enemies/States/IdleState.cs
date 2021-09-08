using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class IdleState : State
    {
        protected readonly IdleStateData StateData;

        protected bool FlipAfterIdle;
        protected bool IsIdleTimeOver;
        protected bool IsPlayerInMinAgroRange;

        protected float IdleTime;

        public IdleState(
            Entity entity, 
            FiniteStateMachine stateMachine, 
            string animBoolName,
            IdleStateData stateData) : 
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

            Core.Movement.SetVelocityX(0f);
            IsIdleTimeOver = false;

            SetRandomIdleTime();
        }

        public override void Exit()
        {
            base.Exit();

            if (FlipAfterIdle)
            {
                Core.Movement.Flip();
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(0f);

            if (Time.time >= StartTime + IdleTime)
            {
                IsIdleTimeOver = true;
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
        }

        public void SetFlipAfterIdle(bool flip)
        {
            FlipAfterIdle = flip;
        }

        public void SetRandomIdleTime()
        {
            IdleTime = UnityEngine.Random.Range(StateData.minIdleTime, StateData.maxIdleTime);
        }
    }
}