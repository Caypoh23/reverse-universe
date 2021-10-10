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
        public Rigidbody2D Rb { get; private set; }

        public int FacingDirection { get; private set; }

        public Vector2 CurrentVelocity { get; private set; }
        public bool CanSetVelocity { get; set; }

        private Vector2 _workspace;

        private readonly CommandStack _commandStack = new CommandStack();

        protected override void Awake()
        {
            base.Awake();

            Rb = GetComponentInParent<Rigidbody2D>();

            FacingDirection = 1;
            CanSetVelocity = true;
        }

        public override void LogicUpdate()
        {
            CurrentVelocity = Rb.velocity;
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
                Rb.velocity = _workspace;
                CurrentVelocity = _workspace;
            }
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
            Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
        }

        #endregion
    }
}