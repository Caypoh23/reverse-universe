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

        protected override void Awake()
        {
            base.Awake();

            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = rb.velocity;

            rewindTime.ReverseTime(_commandStack);
            rewindTime.StopRevisingTime();

            ResetFacingDirection();
        }

        #region Set Functions

        /*public void SetVelocityZero()
    {
        _workspace = Vector2.zero;
        SetFinalVelocity();
    }*/

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
                _commandStack.ExecuteCommand(new MoveCommand(rb.transform, animator));
            }
        }

        // private void RewindTime()
        // {
        //     if (Input.GetKey(KeyCode.R))
        //     {
        //         _isRewinding = true;
        //         _commandStack.UndoLastCommand();
        //     }
        //     else
        //     {
        //         _isRewinding = false;
        //     }
        // }

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

        #endregion
    }
}
