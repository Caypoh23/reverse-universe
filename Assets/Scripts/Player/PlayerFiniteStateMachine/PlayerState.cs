using Player.Data;
using UnityEngine;

// make abstract
namespace Player.PlayerFiniteStateMachine
{
    public abstract class PlayerState
    {
        protected readonly Core Core;

        // Name should be changed
        // _private
        protected readonly PlayerBase PlayerBase; // animator
        protected readonly PlayerStateMachine StateMachine;
        protected readonly PlayerData PlayerData;
        protected bool IsAnimationFinished;
        protected bool IsExitingState;

        protected float StartTime;

        private readonly int _animBoolId;

        public PlayerState(PlayerBase playerBase, PlayerStateMachine stateMachine,
            PlayerData playerData, int animBoolId)
        {
            PlayerBase = playerBase;
            StateMachine = stateMachine;
            PlayerData = playerData;
            _animBoolId = animBoolId;
            Core = playerBase.Core;
        }

        public virtual void Enter()
        {
            DoChecks();
            PlayerBase.Anim.SetBool(_animBoolId, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }

        public virtual void Exit()
        {
            PlayerBase.Anim.SetBool(_animBoolId, false);
            IsExitingState = true;
        }

        // Update
        public virtual void LogicUpdate()
        {
        }

        // Fixed Update
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        // Check for wall, ground, ledge etc.
        // Tick
        // OnUpdate
        // Rename
        public virtual void DoChecks()
        {
        }

        public virtual void AnimationTrigger()
        {
        }

        public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
    }
}