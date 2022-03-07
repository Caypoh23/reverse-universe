using System;
using Cores.CoreComponents;
using Interfaces;
using JetBrains.Annotations;
using ReverseTime;
using ReverseTime.Commands;
using UnityEngine;

namespace Cores.CoreComponents
{
    public class Movement : CoreComponent
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] private RewindTime rewindTime;

        public Rigidbody2D Rb => rb;

        public int FacingDirection { get; private set; }

        public Vector2 CurrentVelocity { get; private set; }
        public bool CanSetVelocity { get; set; }

        private Vector2 _workspace;

        public bool IsRewinding => rewindTime.IsRewindingTime;

        private readonly CommandStack _commandStack = new CommandStack();

        private readonly int DirectionParameterName = Animator.StringToHash("Direction");

        protected override void Awake()
        {
            base.Awake();

            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CurrentVelocity = rb.velocity;

            CheckIfRewindingTime();

            ResetAnimationDirection();

            ResetFacingDirection();
        }

        private void CheckIfRewindingTime()
        {

            rewindTime.ReverseTime(_commandStack);
            rewindTime.StopRevisingTime();
        }
        
        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        private void ResetFacingDirection()
        {
            if (rb.transform.localRotation.eulerAngles.y >= 180f)
            {
                FacingDirection = -1;
            }
            else
            {
                FacingDirection = 1;
            }
        }

        private void ResetAnimationDirection()
        {
            // changing animation speed 
            if(rewindTime.IsRewindingTime)
            {
                animator.SetFloat(DirectionParameterName, -1);
            }
            else
            {
                animator.SetFloat(DirectionParameterName, 1);
            }
        }


        #region Set Functions

        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
            SetFinalVelocity();
        }

        public void SetVelocity(float velocity, Vector2 direction)
        {
            _workspace = direction * velocity;
            SetFinalVelocity();
        }

        public void SetVelocityX(float velocity)
        {
            _workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity)
        {
            _workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if (CanSetVelocity)
            {
                rb.velocity = _workspace;
                CurrentVelocity = _workspace;
                _commandStack.ExecuteCommand(new MoveCommand(rb.transform, animator, Core.Stats));
                //_commandStack.StackCount();
                
            }
        }

        #endregion
    }
}
