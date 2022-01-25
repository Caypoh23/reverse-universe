using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.States
{
    public class DeadState : State
    {
        protected readonly DeadStateData StateData;

        public DeadState(
            Entity entity, 
            FiniteStateMachine stateMachine,
            int animBoolId, 
            DeadStateData stateData) : 
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

            var position = Entity.transform.position;
            var rotation = StateData.deathBloodParticle.transform.rotation;

            Object.Instantiate(StateData.deathBloodParticle, position,
                rotation);
            Object.Instantiate(StateData.deathBloodParticle, position,
                rotation);

            Entity.gameObject.SetActive(false);
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
    }
}