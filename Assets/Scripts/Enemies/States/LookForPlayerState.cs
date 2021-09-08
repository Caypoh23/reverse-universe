using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class LookForPlayerState : State
    {
        protected readonly LookForPlayerData StateData;

        protected bool IsPlayerIsInMinAgroRange;
        protected bool IsAllTurnsDone;
        protected bool IsAllTurnsTimeDone;
        protected bool TurnImmediately;

        protected float LastTurnTime;

        protected int AmountOfTurnsDone;

        public LookForPlayerState(
            Entity entity, 
            FiniteStateMachine stateMachine, 
            string animBoolName,
            LookForPlayerData stateData) : 
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

            IsAllTurnsDone = false;
            IsAllTurnsTimeDone = false;

            LastTurnTime = StartTime;
            AmountOfTurnsDone = 0;

            Core.Movement.SetVelocityX(0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(0f);

            if (TurnImmediately)
            {
                Core.Movement.Flip();
                LastTurnTime = Time.time;
                AmountOfTurnsDone++;
                TurnImmediately = false;
            }
            else if (Time.time >= LastTurnTime + StateData.timeBetweenTurns && !IsAllTurnsDone)
            {
                Core.Movement.Flip();
                LastTurnTime = Time.time;
                AmountOfTurnsDone++;
            }

            if (AmountOfTurnsDone >= StateData.amountOfTurns)
            {
                IsAllTurnsDone = true;
            }

            if (Time.time >= LastTurnTime + StateData.timeBetweenTurns && IsAllTurnsDone)
            {
                IsAllTurnsTimeDone = true;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();

            IsPlayerIsInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        }

        public void SetTurnImmediately(bool flip)
        {
            TurnImmediately = flip;
        }
    }
}