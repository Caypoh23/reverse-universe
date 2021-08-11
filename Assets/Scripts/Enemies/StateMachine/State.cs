using UnityEngine;

namespace Enemies.StateMachine
{
    public abstract class State
    {
        protected readonly FiniteStateMachine StateMachine;
        protected readonly Entity Entity;
        protected readonly Core Core;

        protected float StartTime;

        private readonly string _animBoolName;

        public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
        {
            Entity = entity;
            StateMachine = stateMachine;
            _animBoolName = animBoolName;
            Core = Entity.Core;
        }

        #region States

        public virtual void Enter()
        {
            StartTime = Time.time;
            Entity.Anim.SetBool(_animBoolName, true);
            DoChecks();
        }

        public virtual void Exit()
        {
            Entity.Anim.SetBool(_animBoolName, false);
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        public virtual void DoChecks()
        {
        }

        #endregion
        
    }
}