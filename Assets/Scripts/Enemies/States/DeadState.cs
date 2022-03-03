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
            DeadStateData stateData
        ) : base(entity, stateMachine, animBoolId)
        {
            StateData = stateData;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.DeactivateBoxCollider();
        }

        public override void Exit()
        {
            base.Exit();

            Entity.ActivateBoxCollider();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            Core.Movement.SetVelocityX(0);
        }
    }
}
